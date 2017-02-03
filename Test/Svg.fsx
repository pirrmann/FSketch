#load "References.fsx"

open FSketch
open FSketch.Dsl
open FSketch.Builder

let simpleShapes = shapes {
    yield ellipse (200., 200.) |> at origin |> withFill Brushes.Blue
    yield rectangle (150., 50.) |> at origin |> rotatedBy (Pi/3.) |> withContourAndFill (Pens.Black, Brushes.DarkGreen)
}

let svgPath = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Crossbones.svg")
let complexShapes = Svg.SvgParser.FromFile svgPath

complexShapes |> List.take 3
complexShapes
