#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Behaviours.dll"
#r "bin/debug/FSketch.Svg.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

[<RequireQualifiedAccess>]
type PrinterRegistration =
    | Shapes = 0x01
    | Frame = 0x02
    | Scene = 0x04
    | RenderedScene = 0x08
    | All = 0x0F

let RegisterPrinters (printerRegistration:PrinterRegistration) =
#if HAS_FSI_ADDHTMLPRINTER
    if printerRegistration &&& PrinterRegistration.Shapes = PrinterRegistration.Shapes then
        fsi.AddHtmlPrinter(fun (shapes:FSketch.Shapes) ->
                                let svg = FSketch.Svg.SvgDrawer.Draw shapes
                                Seq.empty, svg)
#else
    if printerRegistration &&& PrinterRegistration.Shapes = PrinterRegistration.Shapes then
        fsi.AddPrintTransformer(fun (shapes:FSketch.Shapes) ->
                                    shapes |> FSketch.Frame.FromShapes |> FSketch.Winforms.WinformsDrawer.Draw |> ignore
                                    null)

    if printerRegistration &&& PrinterRegistration.Frame = PrinterRegistration.Frame then
        fsi.AddPrintTransformer(fun (frame:FSketch.Frame) ->
                                    frame |> FSketch.Winforms.WinformsDrawer.Draw |> ignore
                                    null)

    if printerRegistration &&& PrinterRegistration.Scene = PrinterRegistration.Scene then
        fsi.AddPrintTransformer(fun (scene: FSketch.Behaviours.Scene) ->
                                    [scene] |> FSketch.Behaviours.Camera.record 24 |> FSketch.Winforms.WinformsDrawer.Play |> ignore
                                    null)

    if printerRegistration &&& PrinterRegistration.RenderedScene = PrinterRegistration.RenderedScene then
        fsi.AddPrintTransformer(fun (scene: FSketch.RenderedScene) ->
                                    scene |> FSketch.Winforms.WinformsDrawer.Play |> ignore
                                    null)
#endif
