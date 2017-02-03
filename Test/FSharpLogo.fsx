#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Svg.dll"
#r "bin/debug/FSketch.Drawing.dll"

open FSketch
open FSketch.Dsl
open FSketch.Builder

let DarkBlue = ArgbColor { Alpha = 1.000000; R = 0.215686275; G = 0.545098039; B = 0.729411765 }
let LightBlue = ArgbColor { Alpha = 1.000000; R = 0.188235294; G = 0.725490196; B = 0.858823529 }

let Logo = shapes {
    yield [
            lineTo (56., -56.)
            lineTo (0., 28.)
            lineTo (-28., 28.)
            lineTo (28., 28.)
            lineTo (0., 28.)
        ] |> toClosedPath
        |> at (5., 63.)
        |> withContour { Pens.Black with Color = DarkBlue }

    yield [
            lineTo (20., -20.)
            lineTo (0., 40.)
        ] |> toClosedPath
        |> at (41., 63.)
        |> withContour { Pens.Black with Color = DarkBlue }

    yield [
            lineTo (-58., -56.)
            lineTo (0., 28.)
            lineTo (28., 28.)
            lineTo (-28., 28.)
            lineTo (0., 28.)
        ] |> toClosedPath
        |> at (121., 63.)
        |> withContour { Pens.Black with Color = LightBlue }
}

