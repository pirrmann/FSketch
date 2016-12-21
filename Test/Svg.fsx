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
    yield ellipse (200., 200.) |> at origin |> withFill Brushes.Blue
    yield rectangle (150., 50.) |> at origin |> rotatedBy (Pi/3.) |> withContourAndFill (Pens.Black, Brushes.DarkGreen)
}

let complexShapes = Svg.SvgParser.FromFile @"C:\Users\Pierre\Pictures\Léo\Skull_and_crossbones.svg"

complexShapes |> List.take 2
complexShapes
