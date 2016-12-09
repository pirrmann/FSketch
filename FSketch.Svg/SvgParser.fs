namespace FSketch.Svg

open System
open System.Xml.Linq

open FSketch

type PathCommand =
    | MoveRelative
    | LineToRelative
    | CurveToRelative
    | ClosePath

type PathState = {
    StartX: float
    StartY: float
    LastCommand: PathCommand option
    Parts: Path list }

module internal ParsingHelper =
    let stringOfChars s = s |> Seq.toArray |> String
    let skipSpacesIfAny = List.skipWhile Char.IsWhiteSpace
    let skipChar c s =
        match s with
        | c' :: s' when c = c' -> s'
        | _ -> failwithf "Cannot parse: expecting %A but was %s" c (stringOfChars s)

    let parseFloat floatString = Double.Parse(floatString, System.Globalization.CultureInfo.InvariantCulture)
    let readFloat s =
        let rec readFloat' acc s =
            match s with
            | c :: s when Char.IsDigit c -> readFloat' (c::acc) s
            | '.' :: s -> readFloat' ('.'::acc) s
            | '-' :: s -> readFloat' ('-'::acc) s
            | _ ->
                let floatString = acc |> Seq.rev |> stringOfChars
                try
                    parseFloat floatString, s
                with e -> failwithf "Cannot parse: expecting float value and was %s: %O" floatString e 
        readFloat' [] s

    let readCoordinates s =
        let x, s =  s |> skipSpacesIfAny |> readFloat
        let s = s |> skipSpacesIfAny |> skipChar ','
        let y, s = s |> skipSpacesIfAny |> readFloat
        (x, y), s

    let tryReadCommand s =
        match s |> skipSpacesIfAny with
        | 'm' :: s -> Some MoveRelative, s
        | 'l' :: s -> Some LineToRelative, s
        | 'c' :: s -> Some CurveToRelative, s
        | 'z' :: s -> Some ClosePath, s
        | _ -> None, s

    let rec parseNextCommand state s =
        match tryReadCommand s, state.LastCommand with
        | (None, []), _ -> state, false
        | (Some ClosePath, []), _ -> state, true
        | (Some MoveRelative, s), _
        | (None, s), Some MoveRelative ->
            if state.Parts <> [] then
                failwithf "Cannot move when the path has already started: %s" (stringOfChars s)
            let (x, y), s = s |> readCoordinates
            let state = { state with LastCommand = Some LineToRelative; StartX = state.StartX + x; StartY = state.StartY + y }
            s |> parseNextCommand state
        | (Some LineToRelative, s), _
        | (None, s), Some LineToRelative ->
            let (x, y), s = s |> readCoordinates
            let part = Line(Vector(x, y))
            let state = { state with LastCommand = Some LineToRelative; Parts = part :: state.Parts }
            s |> parseNextCommand state
        | (Some CurveToRelative, s), _
        | (None, s), Some CurveToRelative ->
            let (dx1, dy1), s = s |> readCoordinates
            let (dx2, dy2), s = s |> readCoordinates
            let (x, y), s = s |> readCoordinates
            let part = Bezier(Vector(x, y), Vector(dx1, dy1), Vector(dx2 - x, dy2 - y))
            let state = { state with LastCommand = Some CurveToRelative; Parts = part :: state.Parts }
            s |> parseNextCommand state
        | _ -> failwithf "Cannot parse '%s'" (stringOfChars s)

    let parsePath (pathDescription:string) =
        let initialState = { LastCommand = None; StartX = 0.; StartY = 0.; Parts = [] }
        let state, closedPath = pathDescription |> Seq.toList |> parseNextCommand initialState
        let parts =
            if closedPath then
                let startPoint = Vector(initialState.StartX, initialState.StartY)
                let endPoint = state.Parts |> Seq.sumBy (fun p -> p.End)
                if startPoint <> endPoint then
                    Line(startPoint - endPoint) :: state.Parts
                else
                    state.Parts
            else
                state.Parts
        let path = parts |> List.rev |> CompositePath
        RefSpace.At(state.StartX, state.StartY), path, closedPath

    let getCssProperties (s:string) =
        s.Split([|';'|], System.StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun s ->
            match s.Trim().Split([|':'|]) with
            | [|key; value|] -> key, value
            | _ -> failwithf "Cannot parse style %s" s)
        |> Map.ofSeq

    let parseColor (s:string) =
        // TODO
        match s with
        | "none" -> Colors.Transparent
        | "#fff"
        | "#ffffff"
        | "#FFF"
        | "#FFFFFF"
        | "white" -> Colors.White
        | _ -> Colors.Black

    let parseSize (s:string) =
        if s.EndsWith("pt") then
            let size = parseFloat (s.Substring(0, s.Length - 2))
            size * 2.54 / 72.
        else
            parseFloat s

    let parsePenAndBrush s =
        let properties = getCssProperties s
        try
            let fillColor = properties.TryFind("fill") |> Option.map parseColor
            let strokeColor = properties.TryFind("stroke") |> Option.map parseColor
            let strokeWidth = properties.TryFind("stroke-width") |> Option.map parseSize

            let fillColor =
                match fillColor with
                | Some c when c = Colors.Transparent -> None
                | Some c -> Some c
                | None -> strokeColor

            let pen = strokeColor |> Option.map (fun c -> {Color = c; Thickness = defaultArg strokeWidth 1.})
            let brush = fillColor |> Option.map (fun c -> {Color = c})
            pen, brush
        with _ -> failwithf "Cannot parse properties %A" properties

    let inline xName name = XName.Get name

    let parseSvgElement (e:XElement) =
        match e.Name.LocalName with
        | "path" ->
            let origin, path, closed = e.Attribute(xName "d").Value |> parsePath
            let style = e.Attribute(xName "style").Value
            match parsePenAndBrush style with
            | Some pen, Some brush ->
                origin, ClosedShape(ClosedPath(path), ContourAndFill(pen, brush))
            | None, Some brush ->
                origin, ClosedShape(ClosedPath(path), Fill(brush))
            | Some pen, None ->
                if closed then
                    origin, ClosedShape(ClosedPath(path), Contour(pen))
                else
                    origin, Path(path, pen)
            | _ -> failwithf "Cannot parse path style %s" style
        | name -> failwithf "Cannot parse element %s" name

module SvgParser =
    let FromFile path =
        use stream = System.IO.File.OpenRead path
        (XDocument.Load stream).Elements().Elements()
        |> Seq.map ParsingHelper.parseSvgElement
        |> Seq.toList
