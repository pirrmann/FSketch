namespace FSketch

module Pathetizer =

    let private closeSubPath (pathParts: PathPart list) =
        let pathEnd = pathParts |> List.map (fun p -> p.End) |> List.sum
        match pathEnd with
        | Vector(x, y) when abs x > 0.1 || abs y > 0.1 ->
            pathParts @ [Line(Vector.Zero - pathEnd)]
        | _ ->
            pathParts

    let private convertToSubPath (pathPoints:(System.Drawing.PointF * byte) array, isClosedPath) =
        let mutable started = false
        let mutable startPosition = 0., 0.
        let pathParts =
            [
                let mutable lastPoint = new System.Drawing.PointF()
                let mutable currentIndex = 0
                while currentIndex < pathPoints.Length do
                    let point, pathType = pathPoints.[currentIndex]
                    match pathType &&& 0x7uy with
                    | 0uy ->
                        started <- true
                        startPosition <- float point.X, float point.Y
                        lastPoint <- point
                    | 1uy ->
                        yield Line(Vector(float(point.X - lastPoint.X), float(point.Y - lastPoint.Y)))
                        lastPoint <- point
                    | 3uy ->
                        let cp1 = point
                        let cp2 = fst(pathPoints.[currentIndex + 1])
                        let endPoint = fst(pathPoints.[currentIndex + 2])
                        currentIndex <- currentIndex + 2
                        yield Bezier(Vector(float(endPoint.X - lastPoint.X), float(endPoint.Y - lastPoint.Y)),
                                     Vector(float(cp1.X - lastPoint.X), float(cp1.Y - lastPoint.Y)),
                                     Vector(float(cp2.X - lastPoint.X), float(cp2.Y - lastPoint.Y)))
                        lastPoint <- endPoint
                    | _ -> ()
                    currentIndex <- currentIndex + 1
            ]

        let pathParts' = if isClosedPath then closeSubPath pathParts else pathParts

        { Start = Vector(startPosition); Parts = pathParts'; Closed = isClosedPath }

    let ConvertToPath shape =
        match shape with
        | Rectangle(Vector(width,height)) ->
            let pathParts =
                [
                    Line(Vector(width,0.))
                    Line(Vector(0.,height))
                    Line(Vector(-width,0.))
                    Line(Vector(0.,-height))
                ]
            { SubPaths = [ { Start = Vector(-width/2.,-height/2.); Parts = pathParts; Closed = true } ] }
        | Ellipse(Vector(width,height)) ->
            let x = width / 2.0
            let y = height / 2.0
            let kappa = 0.5522848
            let ox = x * kappa  // control point offset horizontal
            let oy = y * kappa // control point offset vertical
            let pathParts =
                [
                    Bezier (Vector(x, -y), Vector(0., -oy), Vector(x-ox, -y))
                    Bezier (Vector(x, y), Vector(ox, 0.), Vector(x, y-oy))
                    Bezier (Vector(-x, y), Vector(0., oy), Vector(-x+ox, y))
                    Bezier (Vector(-x, -y), Vector(-ox, 0.), Vector(-x, oy-y))
                ]
            { SubPaths = [ { Start = Vector(-x, 0.); Parts = pathParts; Closed = true } ] }
        | Path path ->
            let subPaths' =
                path.SubPaths
                |> List.map (fun s -> if s.Closed then { s with Parts = closeSubPath s.Parts } else s )
            { path with SubPaths = subPaths' }
        | Text text ->
            let i = new System.Drawing.Bitmap(1, 1)
            let g = System.Drawing.Graphics.FromImage(i)
            g.TextRenderingHint <- System.Drawing.Text.TextRenderingHint.AntiAlias
            use font = new System.Drawing.Font(text.Font.FontName, single text.Size)
            let gp = new System.Drawing.Drawing2D.GraphicsPath()
            let fontFamily = new System.Drawing.FontFamily(text.Font.FontName)
            let fontStyle = int System.Drawing.FontStyle.Regular
            let origin = new System.Drawing.PointF(0.f, 0.f)
            gp.AddString(text.Text, fontFamily, fontStyle, single text.Size, origin, System.Drawing.StringFormat.GenericTypographic)

            let isUnclosedSinglePathFont = text.Font.IsUnclosedSinglePath
            let subPathsPoints =
                 [
                    use iterator = new System.Drawing.Drawing2D.GraphicsPathIterator(gp)
                    let subPathCount = iterator.SubpathCount
                    for subPathNumber in 1 .. subPathCount do
                        let _, startIndex, endIndex, isClosed = iterator.NextSubpath()
                        let points = gp.PathPoints.[startIndex..endIndex]
                        let pathTypes = gp.PathTypes.[startIndex..endIndex]
                        yield Array.zip points pathTypes, (isClosed && not isUnclosedSinglePathFont)
                ]

            { SubPaths = subPathsPoints |> List.map convertToSubPath }
