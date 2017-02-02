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

let darkBlue = ArgbColor { Alpha = 1.000000; R = 0.215686275; G = 0.545098039; B = 0.729411765 }
let lightBlue = ArgbColor { Alpha = 1.000000; R = 0.188235294; G = 0.725490196; B = 0.858823529 }

let coolLogo = shapes {
    yield [
            lineTo (56., -56.)
            lineTo (0., 28.)
            lineTo (-28., 28.)
            lineTo (28., 28.)
            lineTo (0., 28.)
        ] |> toClosedPath
        |> at (5., 63.)
        |> withContour { Pens.Black with Color = darkBlue }

    yield [
            lineTo (20., -20.)
            lineTo (0., 40.)
        ] |> toClosedPath
        |> at (41., 63.)
        |> withContour { Pens.Black with Color = darkBlue }

    yield [
            lineTo (-58., -56.)
            lineTo (0., 28.)
            lineTo (28., 28.)
            lineTo (-28., 28.)
            lineTo (0., 28.)
        ] |> toClosedPath
        |> at (121., 63.)
        |> withContour { Pens.Black with Color = lightBlue }
}

let simpleShapes = shapes {
  yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (75., 25.) |> withContourAndFill (Pens.Blue, Brushes.LightBlue)
  yield rectangle (150., 50.) |> at (200., 0.) |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (275., 25.) |> withContourAndFill (Pens.Red, Brushes.LightPink)
}

let fSharpAdvert = shapes {
  yield! coolLogo |> at origin
  yield text "F# rocks" |> withSize 48. |> at (110., 150.) |> writtenWithContourAndFill (Pens.Black, Brush.FromColor darkBlue)
  yield text "it just worked the 1st time" |> withSize 24. |> at (120., 180.) |> writtenWithContour {Pens.Default with Color = lightBlue}
}

fSharpAdvert |> List.map HandDrawer.handDrawn