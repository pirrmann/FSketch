#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

#load "FSharpLogo.fsx"

open FSketch

fsi.AddPrintTransformer(fun (shapes:Shapes) ->
                            shapes |> Winforms.WinformsDrawer.Draw |> ignore
                            null)

fsi.AddPrintTransformer(fun (strings:string array) ->
                            Array2D.init strings.Length 1 (fun i _ -> strings.[i])
                            |> DrawingDebugUtils.FromArray2D(ListDrawerOptions(bordersEnabled = false, gridCellSizes = Unconstrained))
                            |> Winforms.WinformsDrawer.Draw
                            |> ignore
                            null)
