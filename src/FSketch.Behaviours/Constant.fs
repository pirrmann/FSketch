namespace FSketch.Behaviours

[<RequireQualifiedAccess>]
module Constant =

    let TransformMatrix (transform:FSketch.TransformMatrix) =
        match transform with
        | FSketch.TransformMatrix ((m11, m12), (m21, m22), (mx, my)) ->
            TransformMatrix ((forever m11, forever m12), (forever m21, forever m22), (forever mx, forever my))

    let RefSpace (refSpace:FSketch.RefSpace) = {
        transform = TransformMatrix refSpace.transform
        z = forever refSpace.z }

    let Vector (v:FSketch.Vector) =
        match v with
        | FSketch.Vector(x, y) -> Vector(forever x, forever y)

    let PathPart (pathPart:FSketch.PathPart) =
        match pathPart with
        | FSketch.Line v -> Line (Vector v)
        | FSketch.Bezier (v, cp1, cp2) -> Bezier (Vector v, Vector cp1, Vector cp2)

    let SubPath (subPath:FSketch.SubPath) = {
        Start = Vector subPath.Start
        Parts = List.map PathPart subPath.Parts
        Closed = subPath.Closed }

    let Path (path:FSketch.Path) =
        { SubPaths = List.map SubPath path.SubPaths }

    let Font (font:FSketch.Font) =
        match font with
        | FSketch.Font.Arial -> Font.Arial
        | FSketch.Font.UnclosedSinglePathFont fontName -> Font.UnclosedSinglePathFont fontName

    let HorizontalAlign (align:FSketch.HorizontalAlign) =
        match align with
        | FSketch.HorizontalAlign.Left -> Left
        | FSketch.HorizontalAlign.Center -> Center
        | FSketch.HorizontalAlign.Right -> Right

    let VerticalAlign (align:FSketch.VerticalAlign) =
        match align with
        | FSketch.VerticalAlign.Top -> Top
        | FSketch.VerticalAlign.Middle -> Middle
        | FSketch.VerticalAlign.Bottom -> Bottom

    let Text (text:FSketch.Text) = {
        Text = text.Text
        Size = forever text.Size
        Font = Font text.Font
        HorizontalAlign = HorizontalAlign text.HorizontalAlign
        VerticalAlign = VerticalAlign text.VerticalAlign }

    let Shape (shape:FSketch.Shape) =
        match shape with
        | FSketch.Rectangle size -> Rectangle (Vector size)
        | FSketch.Ellipse size -> Ellipse (Vector size)
        | FSketch.Path path -> FSketch.Behaviours.Path (Path path)
        | FSketch.Text text -> FSketch.Behaviours.Text (Text text)

    let ArgbColor (color:FSketch.ArgbColor) = {
        Alpha = forever color.Alpha
        R = forever color.R
        G = forever color.G
        B = forever color.B }

    let HslaColor (color:FSketch.HslaColor) = {
        H = forever color.H
        S = forever color.S
        L = forever color.L
        Alpha = forever color.Alpha }

    let Color (color:FSketch.Color) =
        match color with
        | FSketch.ArgbColor argbColor -> FSketch.Behaviours.ArgbColor (ArgbColor argbColor)
        | FSketch.HslaColor hslaColor -> FSketch.Behaviours.HslaColor (HslaColor hslaColor)

    let Pen (pen:FSketch.Pen) = {
        Color = Color pen.Color
        Thickness = forever pen.Thickness
        LineJoin = match pen.LineJoin with
                   | FSketch.LineJoin.Miter -> LineJoin.Miter
                   | FSketch.LineJoin.Round -> LineJoin.Round }

    let Brush (brush:FSketch.Brush) =
        match brush with
        | FSketch.SolidBrush color -> SolidBrush (Color color)

    let DrawType (drawType:FSketch.DrawType) =
        match drawType with
        | FSketch.Contour pen -> Contour (Pen pen)
        | FSketch.Fill brush -> Fill (Brush brush)
        | FSketch.ContourAndFill (pen, brush) -> ContourAndFill (Pen pen, Brush brush)

    let StyledShape (styledShape:FSketch.StyledShape) = {
        Shape = Shape styledShape.Shape
        DrawType = DrawType styledShape.DrawType }

    let Shapes (shapes:FSketch.Shapes) =
        shapes |> List.map (fun (refSpace, shape) -> RefSpace refSpace, StyledShape shape)
