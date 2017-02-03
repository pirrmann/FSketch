#load "References.fsx"

open FSketch
open FSketch.Dsl
open FSketch.Builder

let simpleShapes = shapes {
    yield ellipse (200., 200.) |> at origin |> withFill Brushes.Blue
    yield rectangle (150., 50.) |> at origin |> rotatedBy (Pi/3.) |> withContourAndFill (Pens.Black, Brushes.DarkGreen)
}

let complexShapes = Svg.SvgParser.FromFile @"C:\Users\Pierre\Pictures\Léo\Skull_and_crossbones.svg"

complexShapes |> List.take 3
complexShapes
