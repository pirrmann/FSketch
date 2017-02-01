#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Svg.dll"
#r "bin/debug/FSketch.Drawing.dll"

open FSketch
open FSketch.Dsl
open FSketch.Builder

#if HAS_FSI_ADDHTMLPRINTER
fsi.AddHtmlPrinter(fun (shapes:Shapes) ->
                        let svg = FSketch.Svg.SvgDrawer.Draw shapes
                        Seq.empty, svg)
#else
#r "bin/debug/FSketch.Winforms.dll"
fsi.AddPrintTransformer(fun (shapes:Shapes) ->
                            shapes |> FSketch.Winforms.WinformsDrawer.Draw |> ignore
                            null)
#endif

#load "Pathetizer.fs"
#load "HandDrawer.fs"

let lightBlue = ArgbColor { Alpha = 1.000000; R = 0.215686275; G = 0.545098039; B = 0.729411765 }
let darkBlue = ArgbColor { Alpha = 1.000000; R = 0.188235294; G = 0.725490196; B = 0.858823529 }

let closePath (pathParts: Path list) =
    let pathEnd = pathParts |> List.map (fun p -> p.End) |> List.sum
    match pathEnd with
    | Vector(x, y) when abs x > 0.1 || abs y > 0.1 ->
        pathParts @ [Line(Vector.Zero - pathEnd)]
    | _ ->
        pathParts

let coolLogo = shapes {
    yield [
            lineTo (56., -56.)
            lineTo (0., 28.)
            lineTo (-28., 28.)
            lineTo (28., 28.)
            lineTo (0., 28.)
        ] |> closePath |> toClosedPath
        |> at (5., 63.)
        |> withContour { Pens.Black with Color = lightBlue }

    yield [
            lineTo (20., -20.)
            lineTo (0., 40.)
        ] |> closePath |> toClosedPath
        |> at (41., 63.)
        |> withContour { Pens.Black with Color = lightBlue }

    yield [
            lineTo (-58., -56.)
            lineTo (0., 28.)
            lineTo (28., 28.)
            lineTo (-28., 28.)
            lineTo (0., 28.)
        ] |> closePath |> toClosedPath
        |> at (121., 63.)
        |> withContour { Pens.Black with Color = darkBlue }
}

let simpleShapes = shapes {
  yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (75., 25.) |> withContourAndFill (Pens.Blue, Brushes.LightBlue)
  yield rectangle (150., 50.) |> at (200., 0.) |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (275., 25.) |> withContourAndFill (Pens.Red, Brushes.LightPink)
  yield! coolLogo |> at (112., -125.)
}

simpleShapes |> List.map HandDrawer.handDrawn
