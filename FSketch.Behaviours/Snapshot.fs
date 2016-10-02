namespace FSketch.Behaviours

module Snapshot =
    type private CS = FSketch.ClosedShape

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

        let evalClosedShape (closedShape: ClosedShape) : CS =
            match closedShape with
            | Rectangle size ->
                CS.Rectangle(evalVector size)
            | Ellipse size ->
                CS.Ellipse(evalVector size)
            | ClosedPath path ->
                CS.ClosedPath(evalPath path)

        let evalColor (color:Color) : FSketch.Color = {
            Alpha = eval color.Alpha
            R = eval color.R
            G = eval color.G
            B = eval color.B }

        let evalPen (pen:Pen) : FSketch.Pen = {
            Color = evalColor pen.Color
            Thickness = eval pen.Thickness }

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
            | ClosedShape (closedShape, drawType) ->
                FSketch.Shape.ClosedShape(evalClosedShape closedShape, evalDrawType drawType)
            | Path (path, pen) ->
                FSketch.Shape.Path(evalPath path, evalPen pen)
            | Text (text, brush) ->
                FSketch.Shape.Text(evalText text, evalBrush brush)
        
        let evalPlacedShape (refSpace:RefSpace, shape:Shape) =
            evalRefSpace refSpace, evalShape shape

        shapes |> List.map evalPlacedShape

    let atTime time (shapes:Shapes) =
        let context = { Time = time }
        let eval (Behaviour(f)) = f context
        shapes |> takeSnapshot eval
