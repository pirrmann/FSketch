#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.Shapes)

open FSketch
open FSketch.Dsl
open FSketch.Builder

let texts = shapes {
        yield text "top-left" |> withSize 48. |> aligned (Top, Left) |> at origin |> writtenWithContour Pens.Red
        yield text "bottom-left" |> withSize 48. |> aligned (Bottom, Left) |> at origin |> writtenWithContour Pens.Green
        yield text "top-right" |> withSize 48. |> aligned (Top, Right) |> at origin |> writtenWithContour Pens.Blue
        yield text "bottom-right" |> withSize 48. |> aligned (Bottom, Right) |> at origin |> writtenWithContour Pens.Orange
        yield text "middle" |> withSize 48. |> aligned (Middle, Center) |> at origin |> writtenWithContour Pens.Magenta
    }

let addBoundingBox (refSpace, styledShape) =
    let (left, top, right, bottom) =
        DrawingUtils.computeShapeBoundingBox (refSpace, styledShape)
    let w, h = right - left, bottom - top
    [
        (refSpace, styledShape)
        rectangle (w, h) |> at (left + w / 2., top + h / 2.) |> withContour Pens.Red
    ]

let alignTests = shapes {
    yield rectangle (600., 200.) |> at origin |> withContour Pens.Black
    yield [
            lineTo (10., 10.)
        ] |> toOpenPath
        |> at (-5., -5.)
        |> withContour Pens.Black
    yield [
            lineTo (10., -10.)
        ] |> toOpenPath
        |> at (-5., 5.)
        |> withContour Pens.Black

    yield! texts |> List.collect addBoundingBox |> at origin
}
