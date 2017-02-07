#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Behaviours.dll"
#r "bin/debug/FSketch.Svg.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

let RegisterPrinters () =
#if HAS_FSI_ADDHTMLPRINTER
    fsi.AddHtmlPrinter(fun (shapes:FSketch.Shapes) ->
                            let svg = FSketch.Svg.SvgDrawer.Draw shapes
                            Seq.empty, svg)
#else
    fsi.AddPrintTransformer(fun (shapes:FSketch.Shapes) ->
                                shapes |> FSketch.Frame.FromShapes |> FSketch.Winforms.WinformsDrawer.Draw |> ignore
                                null)

    fsi.AddPrintTransformer(fun (frame:FSketch.Frame) ->
                                frame |> FSketch.Winforms.WinformsDrawer.Draw |> ignore
                                null)

    fsi.AddPrintTransformer(fun (scene: FSketch.Behaviours.Scene) ->
                                scene |> FSketch.Winforms.WinformsDrawer.Play |> ignore
                                null)
#endif
