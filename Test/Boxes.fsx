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

#load "HandDrawer.fs"

let simpleShapes = shapes {
    yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
    yield ellipse (140., 40.) |> at (75., 25.) |> withContour Pens.Blue
}

simpleShapes |> List.choose HandDrawer.handDrawn
