#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.RenderedScene)

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

let bouncingCircle = shapes {
    yield square (forever 10.) |> at origin |> withContour Pens.Black
    let y = time >>> (fun t -> abs(cos(t * System.Math.PI)) * -10.)
    yield circle (forever 2.) |> at (forever 0., y) |> withFill Brushes.Red
}

let scene1 = {
    Duration = 1.
    TimeTransform = id
    Shapes = bouncingCircle
    Viewport = None
}

let scene2 = {
    Duration = 1.
    TimeTransform = id
    Shapes = bouncingCircle
    Viewport = Some {
        Center = Vector(forever 0., forever -3.5)
        ViewSize = Vector(forever 18., forever 18.)
    }
}

[scene1;scene2] |> Camera.record 24
