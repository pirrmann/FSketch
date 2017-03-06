module FSketch.Winforms.Drawer

open FSketch
open FSketch.DrawingUtils

open System.Drawing
open System.Drawing.Drawing2D

let toSystemColor (color:FSketch.Color) =
    let argbColor =
        match color with
        | ArgbColor c -> c
        | HslaColor c -> ColorSpaces.HsalToArgb c
    Color.FromArgb(int(argbColor.Alpha * 255.0), int(argbColor.R * 255.0), int(argbColor.G * 255.0), int(argbColor.B * 255.0))

let toSystemPen (pen:FSketch.Pen) =
    let systemPen = new Pen(pen.Color |> toSystemColor, pen.Thickness |> single)
    match pen.LineJoin with
    | FSketch.LineJoin.Miter -> systemPen.LineJoin <- LineJoin.Miter
    | FSketch.LineJoin.Round -> systemPen.LineJoin <- LineJoin.Round
    systemPen

let toSystemBrush (brush:FSketch.Brush) =
    match brush with
    | SolidBrush color -> new SolidBrush(color |> toSystemColor)

let toSystemTransform (TransformMatrix((m11, m12), (m21, m22), (dx, dy))) =
    new Matrix(float32 m11, float32 m12, float32 m21, float32 m22, float32 dx, float32 dy)

let toSystemXY (Vector(x, y)) = single x, single y
let toSystemPoint (Vector(x, y)) = new PointF(single x, single y)

let toSystemPath path =
    let offset = ref Vector.Zero

    let getPathPartPoints pathPart = seq {
        match pathPart with
        | Line v ->
            offset := !offset + v
            yield !offset, PathPointType.Line
        | Bezier (v, cp1, cp2) ->
            yield !offset + cp1, PathPointType.Bezier
            yield !offset + cp2, PathPointType.Bezier
            offset := !offset + v
            yield !offset, PathPointType.Bezier }

    let getSubPathPoints subPath =
        offset := subPath.Start
        let points =
            [|
                yield !offset, PathPointType.Start
                yield! subPath.Parts |> Seq.collect getPathPartPoints
            |]

        if subPath.Closed then
            let lastIndex = points.Length - 1
            let lastPoint, lastPointType = points.[lastIndex]
            points.[lastIndex] <- lastPoint, lastPointType ||| PathPointType.CloseSubpath

        points

    let allPoints =
        path.SubPaths
        |> Seq.collect getSubPathPoints
        |> Seq.toArray

    let systemPoints = allPoints |> Array.map (fst >> toSystemPoint)
    let pathPointTypes = allPoints |> Array.map (snd >> byte)

    new GraphicsPath(systemPoints, pathPointTypes)

let drawShape (graphics:Graphics) (space:RefSpace, styledShape:StyledShape) =
    graphics.MultiplyTransform(space.transform |> toSystemTransform)
    let drawType = styledShape.DrawType
    match styledShape.Shape with
    | Rectangle(size) ->
        let width, height = size.X, size.Y
        let graphicsPath = new GraphicsPath()
        graphicsPath.AddRectangle(new RectangleF(- width/2.0 |> float32, - height/2.0 |> float32, width |> float32, height |> float32))
        drawType.Brush |> Option.iter (fun brush ->
            let region = new Region(graphicsPath)
            use brush = brush |> toSystemBrush
            graphics.FillRegion(brush, region))
        drawType.Pen |> Option.iter (fun pen ->
            use pen = pen |> toSystemPen
            graphics.DrawPath(pen, graphicsPath))
    | Ellipse(size) ->
        let width, height = size.X, size.Y
        let graphicsPath = new GraphicsPath()
        graphicsPath.AddEllipse(new RectangleF(- width/2.0 |> float32, - height/2.0 |> float32, width |> float32, height |> float32))
        drawType.Brush |> Option.iter (fun brush ->
            let region = new Region(graphicsPath)
            use brush = brush |> toSystemBrush
            graphics.FillRegion(brush, region))
        drawType.Pen |> Option.iter (fun pen ->
            use pen = pen |> toSystemPen
            graphics.DrawPath(pen, graphicsPath))
    | Path(path) ->
        let graphicsPath = path |> toSystemPath
        drawType.Brush |> Option.iter (fun brush ->
            let region = new Region(graphicsPath)
            use brush = brush |> toSystemBrush
            graphics.FillRegion(brush, region))
        drawType.Pen |> Option.iter (fun pen ->
            use pen = pen |> toSystemPen
            graphics.DrawPath(pen, graphicsPath))
    | Text(text) ->
        let w, h = measureText text
        use font = new Font("Arial", single text.Size)
        drawType.Brush |> Option.iter (fun brush ->
            use brush = brush |> toSystemBrush
            graphics.DrawString(text.Text, font, brush, new PointF(single(-w/2.), single(-h/2.))))

let Draw (graphics:Graphics) (width:int, height:int) (frame:Frame) =

    match computeBoundingBox false frame.Shapes with
    | None -> ()
    | Some (left, top, right, bottom) -> 

        let left, top, right, bottom =
            match frame.Viewport with
             | Some viewPort ->
                viewPort.Center.X - viewPort.ViewSize.X / 2.,
                viewPort.Center.Y - viewPort.ViewSize.Y / 2.,
                viewPort.Center.X + viewPort.ViewSize.X / 2.,
                viewPort.Center.Y + viewPort.ViewSize.Y / 2.
             | None -> left, top, right, bottom

        let displayWidth, displayHeight = width - 50, height - 50
        let scaleRatio =
            if (right - left) / (bottom - top) < (double displayWidth / double displayHeight) then
                double displayHeight / (bottom - top)
            else
                double displayWidth / (right - left)

        graphics.SmoothingMode <- SmoothingMode.HighQuality
        graphics.TextRenderingHint <- Text.TextRenderingHint.AntiAlias

        for shape in frame.Shapes |> Seq.sortBy (fun (s, _) -> s.z) do
            graphics.ResetTransform()
            graphics.TranslateTransform(single width/2.f, single height/2.f)
            graphics.ScaleTransform(single scaleRatio, single scaleRatio)
            graphics.TranslateTransform(-(single left + single right)/2.f, -(single top + single bottom)/2.f)
            shape |> drawShape graphics

        match frame.Viewport with
        | Some viewport ->
            graphics.ResetTransform()
            graphics.TranslateTransform(single width/2.f, single height/2.f)
            graphics.ScaleTransform(single scaleRatio, single scaleRatio)
            graphics.TranslateTransform(-(single left + single right)/2.f, -(single top + single bottom)/2.f)
            graphics.TranslateTransform(single viewport.Center.X, single viewport.Center.Y)
            let graphicsPath = new GraphicsPath()
            graphicsPath.AddRectangle(new RectangleF(-(single viewport.ViewSize.X) / 2.f, -(single viewport.ViewSize.Y) / 2.f, single viewport.ViewSize.X, single viewport.ViewSize.Y))
            let region = new Region()
            region.MakeInfinite()
            region.Exclude(graphicsPath)
            use brush = Brushes.White |> toSystemBrush
            graphics.FillRegion(brush, region)
        | None -> ()

        graphics.ResetTransform()
