namespace FSketch.Behaviours

module Camera =
    type private CS = FSketch.Shape

    let rec internal takeSnapshot eval (shapes:Shapes) =
        let evalTransformMatrix (TransformMatrix((m11, m12), (m21, m22), (mx, my))) =
            FSketch.TransformMatrix ((eval m11, eval m12), (eval m21, eval m22), (eval mx, eval my))

        let evalRefSpace (refSpace:RefSpace) : FSketch.RefSpace = {
            transform = evalTransformMatrix refSpace.transform
            z = eval refSpace.z }

        let evalVector (Vector(x, y)) = FSketch.Vector(eval x, eval y)

        let rec evalPath (path:Path) : FSketch.Path =
            match path with
            | Line v -> FSketch.Line (evalVector v)
            | Bezier (v, t1, t2) -> FSketch.Bezier (evalVector v, evalVector t1, evalVector t2)
            | CompositePath paths -> FSketch.CompositePath (paths |> List.map evalPath)

        let evalColor (color:Color) : FSketch.Color = {
            Alpha = eval color.Alpha
            R = eval color.R
            G = eval color.G
            B = eval color.B }

        let evalLineJoin (lineJoin:LineJoin) : FSketch.LineJoin =
            match lineJoin with
            | LineJoin.Miter -> FSketch.LineJoin.Miter
            | LineJoin.Round -> FSketch.LineJoin.Round

        let evalPen (pen:Pen) : FSketch.Pen = {
            Color = evalColor pen.Color
            Thickness = eval pen.Thickness
            LineJoin = evalLineJoin pen.LineJoin }

        let evalBrush (brush:Brush) : FSketch.Brush = {
            Color = evalColor brush.Color }

        let evalDrawType (drawType:DrawType) : FSketch.DrawType =
            match drawType with
            | Contour pen -> FSketch.Contour (evalPen pen)
            | Fill brush -> FSketch.Fill (evalBrush brush)
            | ContourAndFill (pen, brush) -> FSketch.ContourAndFill(evalPen pen, evalBrush brush)

        let evalText (text:Text) : FSketch.Text = {
            Text = text.Text
            Size = eval text.Size }

        let evalShape (shape:Shape) : FSketch.Shape =
            match shape with
            | Rectangle size ->
                CS.Rectangle(evalVector size)
            | Ellipse size ->
                CS.Ellipse(evalVector size)
            | Path (path) ->
                FSketch.Shape.Path(evalPath path)
            | Text (text) ->
                FSketch.Shape.Text(evalText text)

        let evalStyledShape (styledShape:StyledShape) =
            {
                FSketch.StyledShape.Shape = evalShape styledShape.Shape
                FSketch.StyledShape.DrawType = evalDrawType styledShape.DrawType
            }

        let evalPlacedShape (refSpace:RefSpace, styledShape:StyledShape) =
            evalRefSpace refSpace, evalStyledShape styledShape

        shapes |> List.map evalPlacedShape

    let atTime time (shapes:Shapes) =
        let context = { Time = time }
        let eval (Behaviour(f)) = f context
        shapes |> takeSnapshot eval

    let toFrames (frameRate:int) (scene:Scene) = seq {
        let framesCount = scene.Duration * float frameRate |> floor |> int
        for index in 0 .. framesCount - 1 do
        let time = (float index / float (framesCount - 1)) |> scene.TimeTransform
        yield scene.Shapes |> atTime time
    }
