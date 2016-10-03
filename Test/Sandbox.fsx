#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Behaviours.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

fsi.AddPrintTransformer(fun (scene: FSketch.Behaviours.Scene) ->
                            scene |> FSketch.Winforms.WinformsDrawer.Play |> ignore
                            null)

let bouncingCircle = shapes {
    yield square (forever 10.) |> at origin |> withContour Pens.Black
    let y = time >>> (fun t -> abs(sin(t * System.Math.PI)) * -10.)
    yield circle (forever 2.) |> at (forever 0., y) |> withFill Brushes.Red
}

let scene1 = {
    Duration = 2.
    TimeTransform = id
    Shapes = bouncingCircle
}

let ratio = sqrt 2. / 2.

let rec fractal size = shapes {
    if size > 4. then
        yield square (forever size) |> at origin |> withContour Pens.Black
        yield! fractal (size * ratio) |> at origin |> rotatedBy (time >*> Pi >/> forever 2.)
}

let scene2 = {
    Duration = 3.
    TimeTransform = fun t -> t / 3.
    Shapes = fractal 100.
}
