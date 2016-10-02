#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Behaviours.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

fsi.AddPrintTransformer(fun (shapes: FSketch.Shapes) ->
                            shapes |> FSketch.Winforms.WinformsDrawer.Draw |> ignore
                            null)

let test = shapes {
    yield circle (forever 2.) |> at (forever 0., time >*> Pi >>> sin >>> ((*)-10.)) |> withFill Brushes.Red
}

open FSketch

[0. .. 0.1 .. 1.0]
|> DrawingDebugUtils.FromList (fun t -> test |> Snapshot.atTime t)
