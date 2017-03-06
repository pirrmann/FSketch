#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.Scene)

open FSketch.Behaviours
open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

let color shift = HslaColor {
        H = time >>> (fun t -> 0.5 + 0.5 * System.Math.Sin(t * 2. * System.Math.PI + shift))
        S = time >>> (fun t -> 0.5 + 0.5 * System.Math.Sin(t * 2. * System.Math.PI))
        L = forever 0.5
        Alpha = forever 1.
    }

let coloredShapes = shapes {
    yield circle (forever 10.) |> at origin |> withFill (Brush.FromColor (color 0.))
    yield circle (forever 10.) |> at (forever 10., forever 0.) |> withFill (Brush.FromColor (color 0.25))
    yield circle (forever 10.) |> at (forever 20., forever 0.) |> withFill (Brush.FromColor (color 0.5))
    yield circle (forever 10.) |> at (forever 30., forever 0.) |> withFill (Brush.FromColor (color 0.75))
    yield circle (forever 10.) |> at (forever 40., forever 0.) |> withFill (Brush.FromColor (color 1.))
}

let scene1 =
    {
        Duration = 1.
        TimeTransform = id
        Shapes = coloredShapes
        Viewport = None
    }
