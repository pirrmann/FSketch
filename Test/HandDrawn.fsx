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

let simpleShapes = shapes {
  yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (75., 25.) |> withContourAndFill (Pens.Blue, Brushes.LightBlue)
  yield rectangle (150., 50.) |> at (200., 0.) |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (275., 25.) |> withContourAndFill (Pens.Red, Brushes.LightPink)
}

#load "FSharpLogo.fsx"

let fSharpAdvert = shapes {
  yield! FSharpLogo.Logo |> at origin
  yield text "F# rocks" |> withSize 48. |> at (110., 150.) |> writtenWithContourAndFill (Pens.Black, Brush.FromColor FSharpLogo.DarkBlue)
  yield text "it just worked the 1st time" |> withSize 24. |> at (120., 180.) |> writtenWithContour {Pens.Default with Color = FSharpLogo.LightBlue}
}

fSharpAdvert |> List.map HandDrawer.RedrawByHand