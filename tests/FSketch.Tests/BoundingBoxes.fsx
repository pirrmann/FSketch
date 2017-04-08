#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.Shapes)

open FSketch
open FSketch.Dsl
open FSketch.Builder

let curves = shapes {
    yield rectangle (600., 600.) |> at origin |> withContour Pens.Blue
    yield text "aceou" |> withSize 48. |> at origin |> writtenWithContour Pens.Black
    yield text "test\net encore un autre" |> withSize 48. |> at origin |> writtenWithContour Pens.Black
}

let addBoundingBox (refSpace, styledShape) =
    let (left, top, right, bottom) =
        DrawingUtils.computeShapeBoundingBox (refSpace, styledShape)
    let w, h = right - left, bottom - top
    [
        (refSpace, styledShape)
        rectangle (w, h) |> at (left + w / 2., top + h / 2.) |> withContour Pens.Red
    ]

let boxed = curves |> List.collect addBoundingBox
