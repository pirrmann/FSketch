#load "References.fsx"
References.RegisterPrinters()

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

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

let rotatingCircle offset =
    let x = time >>> (fun t -> cos (2. * System.Math.PI * t + offset * System.Math.PI / 6.) * 10.)
    let y = time >>> (fun t -> sin (2. * System.Math.PI * t + offset * System.Math.PI / 6.) * 10.)
    shapes {
        yield circle (forever 10.) |> at origin |> withContour { Pens.Black with Thickness = forever 0.1 }
        yield circle (forever 2.) |> at (x, y) |> withFill Brushes.Black
    }

let scene3 = {
    Duration = 3.
    TimeTransform = fun t -> t / 3.
    Shapes = shapes {
        yield rectangle (forever 210., forever 210.) |> at origin |> withContour Pens.Black
        for x in -9 .. 9 do
        for y in -9 .. 9 do
        yield! rotatingCircle (float (x + y)) |> at (forever (float x*10.), forever(float y*10.))
    }
}

let circleAt (x, y) =
    let angle = (lift atan2) x y >>> (fun a -> (a + System.Math.PI) / 2. / System.Math.PI)
    let color =
        HslaColor {
            H = angle
            S = forever 1.
            L = forever 0.5
            Alpha = forever 1. }

    shapes {
        yield circle (forever 10.) |> at (x, y) |> withFill { Brushes.Black with Color = color }
    }

let circlesBasedAt (x, y) offset backwards =
    let rotationRadius = 100. * System.Math.PI / 12.
    let timeFilter t =
        let relativeTime = (t - offset + 1.875) % 1.
        if relativeTime > 0. && relativeTime < 0.25 then
            if backwards
            then offset + 0.25 - relativeTime * 2.
            else offset + 0.25 + relativeTime * 2.
        else offset + 0.25

    let time' = time >>> timeFilter

    let x' = time' >>> (fun t -> x + rotationRadius * cos (t * 2. * System.Math.PI))
    let y' = time' >>> (fun t -> y + rotationRadius * sin (t * 2. * System.Math.PI))
    circleAt (x', y')

let scene4 = {
    Duration = 8.
    TimeTransform = fun t -> t / 8.
    Shapes = shapes {
        yield rectangle (forever 300., forever 300.) |> at origin |> withContour Pens.Black
        for step in 0 .. 23 do
        let offset = float step / 24.
        let angleOffset = offset * 2. * System.Math.PI
        let x = 100. * cos angleOffset
        let y = 100. * sin angleOffset
        let backwards = (step % 2) = 0
        yield! circlesBasedAt (x, y) offset backwards |> at origin
    }
}
