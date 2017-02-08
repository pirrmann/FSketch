#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.Shapes)

open FSketch
open FSketch.Dsl
open FSketch.Builder

let drawBehaviour pen (FSketch.Behaviours.Behaviour(f)) =
    shapes {
        let step = 0.01
        let mutable start = None
        let mutable v = 0.
        yield [
            for t in 0. .. step .. 2. do
                let context = { FSketch.Behaviours.Time = t }
                let v' = f context
                if start.IsNone then start <- Some v'
                else yield lineTo (step * 10., (v - v') * 10.)
                v <- v'
        ] |> toOpenPath |> at (0., start.Value * -10.) |> withContour pen
    }

let graphBox pen xRange yRange = shapes {
    let xMin, xMax = List.min xRange, List.max xRange
    let yMin, yMax = List.min yRange, List.max yRange
    yield [lineTo ((xMax - xMin) * 10., 0.)] |> toOpenPath |> at (xMin * 10., 0.) |> withContour pen
    yield [lineTo (0., (yMax - yMin) * 10.)] |> toOpenPath |> at (0., -yMax * 10.) |> withContour pen
    for x in xRange do
        yield [lineTo (0., 4.)] |> toOpenPath |> at (float x * 10., -2.) |> withContour pen    
    for y in yRange do
        yield [lineTo (4., 0.)] |> toOpenPath |> at (-2., float y * -10.) |> withContour pen    
}

let drawBehaviours behaviours =
    let pen = { Pens.Black with Thickness = 0.1 }
    shapes {
        yield! graphBox pen [0. .. 2.] [-2. .. 2.] |> at origin
        for b, color in behaviours do
            yield! b |> drawBehaviour { pen with Color = color } |> at origin
    }

open FSketch.Behaviours.Behaviours

let timeShift = 0.
[
//    time, Colors.Black
//    forever 1., Colors.Red
//    time >>> (fun t -> sin(2. * t * Pi)), Colors.Blue
//    time >>> (fun t -> cos(2. * t * Pi)), Colors.Green
    time >>> (fun t -> sin(2. * Pi * (t + timeShift)) * Pi / 6.), Colors.DarkGreen
    time >>> (fun t -> (max 0. (sin(2. * Pi * (t + timeShift) - Pi / 2. + Pi / 6.))) * Pi / 4.), Colors.DeepPink
    time >>> (fun t -> - Pi / 2. + (sin (2. * Pi * t)) * Pi / 36.), Colors.Blue
] |> drawBehaviours
