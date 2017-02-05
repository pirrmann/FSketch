#load "References.fsx"
References.RegisterPrinters()

module Figures =
    open FSketch
    open FSketch.Dsl
    open FSketch.Builder

    let Head =
        shapes {
            yield circle 10. |> at origin |> withContour Pens.Black }
        |> HandDrawer.RedrawByHand

    let Torso (Vector(xHips, yHips)) (Vector(xHead, yHead)) =
        shapes {
            yield [lineTo(xHead - xHips, yHead + 10. - yHips)] |> toOpenPath |> at origin |> withContour Pens.Black }
        |> HandDrawer.RedrawByHand

    let Leg = 
        shapes {
            yield [lineTo(0., 50.); lineTo(10.,0.)] |> toOpenPath |> at origin |> withContour Pens.Black }
        |> HandDrawer.RedrawByHand

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

type Stickman = {
    Head: FSketch.Vector
    Hips: FSketch.Vector
}

let stickman = {
    Head = FSketch.Vector(0., -60.)
    Hips = FSketch.Vector(0., 0.)
}

let walkingMan = shapes {
    yield! Constant.Shapes Figures.Head |> atPos (Constant.Vector stickman.Head)
    yield! Constant.Shapes (Figures.Torso stickman.Hips stickman.Head) |> atPos (Constant.Vector stickman.Hips)
    yield! Constant.Shapes Figures.Leg |> atPos (Constant.Vector stickman.Hips) |> rotatedBy (time >*> Pi)
    yield! Constant.Shapes Figures.Leg |> atPos (Constant.Vector stickman.Hips) |> rotatedBy (time >*> Pi >+> Pi)
}

let pi = FSketch.Dsl.Pi

let walkingInBox = shapes {
    yield rectangle (forever 1000., forever 300.) |> at origin |> withContour Pens.Black
    yield rectangle (forever 10., forever 200.) |> at (forever 400., forever 50.) |> withContour Pens.Black
    yield! walkingMan |> at (time >>> (fun t -> t * pi * 50. - 400.), forever 98.)
}


let scene1 = {
    Duration = 5.
    TimeTransform = id
    Shapes = walkingInBox
}
