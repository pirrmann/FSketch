namespace FSketch.Svg

open System
open System.Xml.Linq

open FSketch

type PathCommand =
    | MoveRelative
    | LineToRelative
    | CurveToRelative
    | ClosePath

type SubPathState = {
    StartX: float
    StartY: float
    LastCommand: PathCommand option
    PathParts: PathPart list }

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
            if state.PathParts <> [] then
                failwithf "Cannot move when the path has already started: %s" (stringOfChars s)
            let (x, y), s = s |> readCoordinates
            let state = { state with LastCommand = Some LineToRelative; StartX = state.StartX + x; StartY = state.StartY + y }
            s |> parseNextCommand state
        | (Some LineToRelative, s), _
        | (None, s), Some LineToRelative ->
            let (x, y), s = s |> readCoordinates
            let part = Line(Vector(x, y))
            let state = { state with LastCommand = Some LineToRelative; PathParts = part :: state.PathParts }
            s |> parseNextCommand state
        | (Some CurveToRelative, s), _
        | (None, s), Some CurveToRelative ->
            let (cx1, cy1), s = s |> readCoordinates
            let (cx2, cy2), s = s |> readCoordinates
            let (x, y), s = s |> readCoordinates
            let part = Bezier(Vector(x, y), Vector(cx1, cy1), Vector(cx2, cy2))
            let state = { state with LastCommand = Some CurveToRelative; PathParts = part :: state.PathParts }
            s |> parseNextCommand state
        | _ -> failwithf "Cannot parse '%s'" (stringOfChars s)

    let parsePath (pathDescription:string) =
        let initialState = { LastCommand = None; StartX = 0.; StartY = 0.; PathParts = [] }
        let state, closedPath = pathDescription |> Seq.toList |> parseNextCommand initialState
        let pathParts = state.PathParts |> List.rev
        let path = { SubPaths = [ { Start = Vector.Zero; Parts = pathParts; Closed = closedPath } ] }
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

    let parseLineJoin (s:string) =
        match s with
        | "round" -> LineJoin.Round
        | "null" -> LineJoin.Miter
        | _ -> failwithf "Cannot parse linejoin value '%s'" s

    let buildDrawType fillColor strokeColor strokeWidth strokeLineJoin =
        let fillColor =
            match fillColor with
            | Some c when c = Colors.Transparent -> None
            | Some c -> Some c
            | None -> strokeColor

        let pen = strokeColor |> Option.map (fun c -> {Color = c; Thickness = defaultArg strokeWidth 1.; LineJoin = defaultArg strokeLineJoin LineJoin.Miter })
        let brush = fillColor |> Option.map (fun c -> SolidBrush c)

        match pen, brush with
        | Some pen, Some brush ->
            ContourAndFill(pen, brush)
        | None, Some brush ->
            Fill(brush)
        | Some pen, None ->
            Contour(pen)
        | _ -> failwith "Cannot build a draw type with no pen nor brush"

    let parseDrawType s =
        let properties = getCssProperties s
        try
            let fillColor = properties.TryFind("fill") |> Option.map parseColor
            let strokeColor = properties.TryFind("stroke") |> Option.map parseColor
            let strokeWidth = properties.TryFind("stroke-width") |> Option.map parseSize
            let strokeLineJoin = properties.TryFind("stroke-linejoin") |> Option.map parseLineJoin
            buildDrawType fillColor strokeColor strokeWidth strokeLineJoin
        with _ -> failwithf "Cannot parse properties %A" properties

    let inline xName name = XName.Get name

    let parseAttributeWith parseFunction attributeName (e:XElement) =
        let attributeValue = e.Attribute(xName attributeName).Value
        try
            parseFunction attributeValue
        with e -> failwithf "Cannot parse attribute %s with value was %s: %O" attributeName attributeValue e 

    let parseSvgElement (e:XElement) =
        match e.Name.LocalName with
        | "path" ->
            let origin, path, closed = e.Attribute(xName "d").Value |> parsePath
            let style = e.Attribute(xName "style").Value
            let drawType = parseDrawType style

            origin, { Shape = Path(path); DrawType = drawType }

        | "line" ->
            let x1 = parseAttributeWith parseFloat "x1" e
            let x2 = parseAttributeWith parseFloat "x2" e
            let y1 = parseAttributeWith parseFloat "y1" e
            let y2 = parseAttributeWith parseFloat "y2" e

            let strokeColor =
                match e.Attribute(xName "stroke").Value |> Option.ofObj with
                | Some color -> parseColor color
                | _ -> Colors.Black

            let strokeWidth = e.Attribute(xName "stroke-width").Value |> Option.ofObj |> Option.map parseSize
            let strokeLineJoin = e.Attribute(xName "stroke-linejoin").Value |> Option.ofObj |> Option.map parseLineJoin

            let drawType = buildDrawType None (Some strokeColor) strokeWidth strokeLineJoin

            let line = {
                Start = Vector(x1, y1)
                Parts = [Line(Vector(x2-x1, y2-y1))]
                Closed = false }

            RefSpace.Origin, { Shape = Path({SubPaths = [line]}); DrawType = drawType }

        | "ellipse" ->
            let cx = parseAttributeWith parseFloat "cx" e
            let cy = parseAttributeWith parseFloat "cy" e
            let rx = parseAttributeWith parseFloat "rx" e
            let ry = parseAttributeWith parseFloat "ry" e

            let fillColor = e.Attribute(xName "fill").Value |> Option.ofObj |> Option.map parseColor
            let strokeColor = e.Attribute(xName "stroke").Value |> Option.ofObj |> Option.map parseColor
            let strokeWidth = e.Attribute(xName "stroke-width").Value |> Option.ofObj |> Option.map parseSize

            let drawType = buildDrawType fillColor strokeColor strokeWidth None

            RefSpace.At(cx, cy), { Shape = Ellipse(Vector(rx * 2., ry * 2. )); DrawType = drawType }

        | name -> failwithf "Cannot parse element %s" name

module SvgParser =
    let FromFile path =
        use stream = System.IO.File.OpenRead path
        (XDocument.Load stream).Elements().Elements()
        |> Seq.map ParsingHelper.parseSvgElement
        |> Seq.toList
