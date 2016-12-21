module FSketch.Winforms.Drawer

open FSketch
open FSketch.DrawingUtils

let toSystemColor (color:Color) =
    System.Drawing.Color.FromArgb(int(color.Alpha * 255.0), int(color.R * 255.0), int(color.G * 255.0), int(color.B * 255.0))

let toSystemPen (pen:Pen) =
    new System.Drawing.Pen(pen.Color |> toSystemColor, pen.Thickness |> single)

let toSystemBrush (brush:Brush) =    
    new System.Drawing.SolidBrush(brush.Color |> toSystemColor)

let toSystemTransform (TransformMatrix((m11, m12), (m21, m22), (dx, dy))) =
    new System.Drawing.Drawing2D.Matrix(float32 m11, float32 m12, float32 m21, float32 m22, float32 dx, float32 dy)

open System.Drawing
open System.Drawing.Drawing2D

let toSystemXY (Vector(x, y)) = single x, single y
let toSystemPoint (Vector(x, y)) = new System.Drawing.PointF(single x, single y)

let toSystemPath path =
    let offset = ref Vector.Zero

    let rec getPoints path = seq {
        for segment in path do
        match segment with
        | Line v ->
            offset := !offset + v
            yield !offset, PathPointType.Line
        | Bezier (v, t1, t2) ->
            yield !offset + t1, PathPointType.Bezier
            offset := !offset + v
            yield !offset + t2, PathPointType.Bezier
            yield !offset, PathPointType.Bezier
        | CompositePath (path) ->
            yield! getPoints path
    }

    let allPoints =
        seq {
            yield !offset, System.Drawing.Drawing2D.PathPointType.Start
            yield! getPoints [path]
        } |> Seq.toArray

    let systemPoints = allPoints |> Array.map (fst >> toSystemPoint)
    let pathPointTypes = allPoints |> Array.map (snd >> byte)

    new System.Drawing.Drawing2D.GraphicsPath(systemPoints, pathPointTypes)

let drawShape (graphics:Graphics) (space:RefSpace, styledShape:StyledShape) =
    graphics.MultiplyTransform(space.transform |> toSystemTransform)
    let drawType = styledShape.DrawType
    match styledShape.Shape with
    | Rectangle(size) ->
        let width, height = size.X, size.Y
        let graphicsPath = new System.Drawing.Drawing2D.GraphicsPath()
        graphicsPath.AddRectangle(new RectangleF(- width/2.0 |> float32, - height/2.0 |> float32, width |> float32, height |> float32))
        drawType.Brush |> Option.iter (fun brush ->
            let region = new Region(graphicsPath)
            use brush = brush |> toSystemBrush
            graphics.FillRegion(brush, region))
        drawType.Pen |> Option.iter (fun pen ->
            use pen = pen |> toSystemPen
            pen.LineJoin <- LineJoin.Round
            graphics.DrawPath(pen, graphicsPath))
    | Ellipse(size) ->
        let width, height = size.X, size.Y
        let graphicsPath = new System.Drawing.Drawing2D.GraphicsPath()
        graphicsPath.AddEllipse(new RectangleF(- width/2.0 |> float32, - height/2.0 |> float32, width |> float32, height |> float32))
        drawType.Brush |> Option.iter (fun brush ->
            let region = new Region(graphicsPath)
            use brush = brush |> toSystemBrush
            graphics.FillRegion(brush, region))
        drawType.Pen |> Option.iter (fun pen ->
            use pen = pen |> toSystemPen
            pen.LineJoin <- LineJoin.Round
            graphics.DrawPath(pen, graphicsPath))
    | Path(path) ->
        let graphicsPath = path |> toSystemPath
        drawType.Brush |> Option.iter (fun brush ->
            let region = new Region(graphicsPath)
            use brush = brush |> toSystemBrush
            graphics.FillRegion(brush, region))
        drawType.Pen |> Option.iter (fun pen ->
            use pen = pen |> toSystemPen
            pen.LineJoin <- LineJoin.Round
            graphics.DrawPath(pen, graphicsPath))
    | Text(text) ->
        let w, h = measureText text
        use font = new Font("Arial", single text.Size)
        drawType.Brush |> Option.iter (fun brush ->
            use brush = brush |> toSystemBrush
            graphics.DrawString(text.Text, font, brush, new PointF(single(-w/2.), single(-h/2.))))

let Draw (graphics:Graphics) (width:int, height:int) (shapes:Shapes) =

    match computeBoundingBox false shapes with
    | None -> ()
    | Some (left, top, right, bottom) -> 

        let displayWidth, displayHeight = width - 50, height - 50
        let scaleRatio =
            if (right - left) / (bottom - top) < (double displayWidth / double displayHeight) then
                double displayHeight / (bottom - top)
            else
                double displayWidth / (right - left)

        graphics.SmoothingMode <- SmoothingMode.HighQuality
        graphics.TextRenderingHint <- Text.TextRenderingHint.AntiAlias

        for shape in shapes |> Seq.sortBy (fun (s, _) -> s.z) do
            graphics.ResetTransform()
            graphics.TranslateTransform(single width/2.f, single height/2.f)
            graphics.ScaleTransform(single scaleRatio, single scaleRatio)
            graphics.TranslateTransform(-(single left + single right)/2.f, -(single top + single bottom)/2.f)
            shape |> drawShape graphics
        graphics.ResetTransform()
