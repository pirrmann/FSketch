#load "References.fsx"
References.RegisterPrinters()

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

type LegSize = {
    Thigh: FSketch.Numeric
    Calf: FSketch.Numeric
    Foot: FSketch.Numeric
}

type LegAngles = {
    Hip: Numeric
    Knee: Numeric
    Ankle: Numeric
}

type Leg = {
    Size: LegSize
    Angles: LegAngles
}

let toLegShapes leg = shapes {
        let knee = (forever leg.Size.Thigh, forever 0.)
        let ankle = (forever leg.Size.Calf, forever 0.)
        let foot = (forever leg.Size.Foot, forever 0.)
        yield! shapes {
            yield [ lineTo knee ] |> toOpenPath |> at origin |> withContour Pens.Black
            yield! shapes {
                yield [ lineTo ankle ] |> toOpenPath |> at origin |> withContour Pens.Black
                yield [ lineTo foot ] |> toOpenPath |> at origin |> rotatedBy leg.Angles.Ankle |> translatedBy ankle |> withContour Pens.Black
            } |> at origin |> rotatedBy leg.Angles.Knee |> translatedBy knee
        } |> at origin |> rotatedBy leg.Angles.Hip
    }

let pi = System.Math.PI

let leg timeShift = {
    Size =
        {
            Thigh = 6.5
            Calf = 7.0
            Foot = 3.0
        }
    Angles =
        {
            Hip = time >>> (fun t -> sin(2. * pi * (t + timeShift)) * pi / 6.)
            Knee = time >>> (fun t -> (max 0. (sin(2. * pi * (t + timeShift) - pi / 2. + pi / 6.))) * pi / 4.)
            Ankle = forever (-System.Math.PI / 2.)
        }
}

let walkingInBox = shapes {
    yield rectangle (forever 40., forever 40.) |> at origin |> withContour Pens.Black
    yield! leg 0.0 |> toLegShapes |> at origin |> rotatedBy (Pi >/> forever 2.)
    yield! leg 0.5 |> toLegShapes |> at origin |> rotatedBy (Pi >/> forever 2.)
}

let scene1 = {
    Duration = 1.
    TimeTransform = (fun t -> t / 2.)
    Shapes = walkingInBox
}
