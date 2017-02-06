#load "References.fsx"
References.RegisterPrinters()

module Background =
    open FSketch
    open FSketch.Dsl
    open FSketch.Builder

    let Wall = shapes {
        for y in 0 .. 5 do
            let offset = if y % 2 = 0 then -50 else -45
            for x in 0 .. 10 do
                yield rectangle (10., 5.) |> at (float (x * 10 + offset), float (y * 5 - 10)) |> withContour { Pens.Black with Thickness = 0.5 }
    }

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

let toLegShapes pen leg = shapes {
        let knee = (forever leg.Size.Thigh, forever 0.)
        let ankle = (forever leg.Size.Calf, forever 0.)
        let foot = (forever leg.Size.Foot, forever 0.)
        yield! shapes {
            yield [ lineTo knee ] |> toOpenPath |> at origin |> withContour pen
            yield! shapes {
                yield [ lineTo ankle ] |> toOpenPath |> at origin |> withContour pen
                yield [ lineTo foot ] |> toOpenPath |> at origin |> rotatedBy leg.Angles.Ankle |> translatedBy ankle |> withContour pen
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
            Hip = time >>> (fun t -> sin(2. * pi * (t + timeShift)) * pi / 8.)
            Knee = time >>> (fun t -> (max 0. (sin(2. * pi * (t + timeShift) - pi / 2. + pi / 6.))) * pi / 6.)
            Ankle = time >>> (fun t -> - pi / 2. - pi / 12. + (max 0. (sin (2. * pi * (t + timeShift)))) * pi / 6.)
        }
}

let pen = { Pens.Default with Color = Colors.DarkRed }
let walkingStickman = shapes {
    let wall = Constant.Shapes Background.Wall

    yield! wall |> at (time >>> (fun t -> (t / 3. % 1.) * -100.), forever -10.)
    yield! wall |> at (time >>> (fun t -> (t / 3. % 1.) * -100. + 100.), forever -10.)

    yield [lineTo (forever 0., forever -12.)] |> toClosedPath |> at origin |> withContour pen
    yield circle (forever 3.) |> at (forever 0., forever -15.) |> withContourAndFill (pen, Brushes.White)
    yield! leg 0.0 |> toLegShapes pen |> at origin |> rotatedBy (Pi >/> forever 2.)
    yield! leg 0.5 |> toLegShapes pen |> at origin |> rotatedBy (Pi >/> forever 2.)
}

let scene1 = {
    Duration = 3.
    TimeTransform = id
    Shapes = walkingStickman
    Viewport = Some {
        Center = Vector(forever 0., forever -5.8)
        ViewSize = Vector(forever 100., forever 40.)
    }
}
