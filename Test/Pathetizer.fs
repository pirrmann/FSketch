module Pathetizer

open FSketch
open FSketch.Dsl
open FSketch.Builder

let private closePath (pathParts: Path list) =
    let pathEnd = pathParts |> List.map (fun p -> p.End) |> List.sum
    match pathEnd with
    | Vector(x, y) when abs x > 0.1 || abs y > 0.1 ->
        pathParts @ [Line(Vector.Zero - pathEnd)]
    | _ ->
        pathParts

let ConvertToPlacedPath (refSpace, shape) =
    match shape with
    | Rectangle(Vector(width,height)) ->
        let path =
            [
                Line(Vector(width,0.))
                Line(Vector(0.,height))
                Line(Vector(-width,0.))
                Line(Vector(0.,-height))
            ] |> CompositePath
        (refSpace, path)
    | Ellipse(Vector(width,height)) ->
        let x = width / 2.0
        let y = height / 2.0
        let kappa = 0.5522848
        let ox = x * kappa  // control point offset horizontal
        let oy = y * kappa // control point offset vertical
        let path =
            [
                Bezier (Vector(x, -y), Vector(0., -oy), Vector(x-ox, -y))
                Bezier (Vector(x, y), Vector(ox, 0.), Vector(x, y-oy))
                Bezier (Vector(-x, y), Vector(0., oy), Vector(-x+ox, y))
                Bezier (Vector(-x, -y), Vector(-ox, 0.), Vector(-x, oy-y))
            ] |> CompositePath
        (refSpace, path) |> translatedBy (-x, 0.)
    | Path path -> (refSpace, path)
    | Text text ->
        let i = new System.Drawing.Bitmap(1, 1)
        let g = System.Drawing.Graphics.FromImage(i)
        g.TextRenderingHint <- System.Drawing.Text.TextRenderingHint.AntiAlias
        use font = new System.Drawing.Font("Arial", single text.Size)
        let size = g.MeasureString(text.Text, font)
        let w, h = float size.Width, float size.Height
        let gp = new System.Drawing.Drawing2D.GraphicsPath()
        let fontFamily = new System.Drawing.FontFamily("Arial")
        let fontStyle = int System.Drawing.FontStyle.Regular
        let origin = new System.Drawing.PointF(single(-w/2.), single(-h/2.))
        gp.AddString(text.Text, fontFamily, fontStyle, single text.Size, origin, System.Drawing.StringFormat.GenericTypographic)

        let mutable startPosition = (0., 0.)

        printfn "%A" (Array.zip gp.PathTypes gp.PathPoints)

        let path =
            [
                let mutable lastPoint = new System.Drawing.PointF()
                let mutable currentIndex = 0
                while currentIndex < gp.PointCount do
                    match gp.PathTypes.[currentIndex] &&& 0x7uy with
                    | 0uy ->
                        lastPoint <- gp.PathPoints.[currentIndex]
                        startPosition <- float lastPoint.X, float lastPoint.Y
                    | 1uy ->
                        let p = gp.PathPoints.[currentIndex]
                        yield Line(Vector(float(p.X - lastPoint.X), float(p.Y - lastPoint.Y)))
                        lastPoint <- p
                    | _ -> ()
                    currentIndex <- currentIndex + 1
            ]
            |> closePath
            |> CompositePath
        
        (refSpace, path) |> translatedBy startPosition
