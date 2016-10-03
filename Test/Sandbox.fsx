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

let bouncingCircle = shapes {
    let y = time >>> (fun t -> abs(sin(t * System.Math.PI)) * -10.)
    yield circle (forever 2.) |> at (forever 0., y) |> withFill Brushes.Red
}

let rec fractal size = shapes {
    if size > 5. then
        yield square (forever size) |> at origin |> withContour Pens.Black
        yield! fractal (size * 0.72) |> at origin |> rotatedBy (time >*> Pi)
}

let scene1 = {
    Duration = 2.
    TimeTransform = id
    Shapes = bouncingCircle
}

let scene2 = {
    Duration = 1.
    TimeTransform = id
    Shapes = fractal 100.
}

open FSketch

scene2
|> Camera.toFrames 18
|> Seq.toList
|> DrawingDebugUtils.FromList(fun (shapes:Shapes) -> shapes)
