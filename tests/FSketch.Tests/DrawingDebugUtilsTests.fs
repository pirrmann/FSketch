module DrawingDebugTests

open NUnit.Framework

open FSketch
open FSketch.Dsl

let [<Test>] ``A single element array yields the shape``() =
    let array = Array2D.zeroCreate 1 1
    let shapes = array |> DrawingDebugUtils.FromArray2D((fun _ -> square 10.), ListDrawerOptions(bordersEnabled = false))

    let expectedShapes =
        [
            square 10. |> at origin |> withFill Brushes.Blue
        ]

    Assert.AreEqual(expectedShapes, shapes)

let [<Test>] ``A single element array yields the shape and border``() =
    let array = Array2D.zeroCreate 1 1
    let shapes = array |> DrawingDebugUtils.FromArray2D(fun _ -> square 10.)

    let expectedShapes =
        [
            square 10. |> at origin |> withContour Pens.Black
            square 10. |> at origin |> withFill Brushes.Blue
        ]

    Assert.AreEqual(expectedShapes, shapes)

let [<Test>] ``A single element array yields the centered shape and border``() =
    let array = Array2D.zeroCreate 1 1
    let shapes = array |> DrawingDebugUtils.FromArray2D(fun _ -> [square 10. |> at (1., 1.) |> withFill Brushes.Blue])

    let expectedShapes =
        [
            square 10. |> at origin |> withContour Pens.Black
            square 10. |> at origin |> withFill Brushes.Blue
        ]

    Assert.AreEqual(expectedShapes, shapes)

let [<Test>] ``A single element array yields shape centered on origin and border``() =
    let array = Array2D.zeroCreate 1 1
    let shapes = array |> DrawingDebugUtils.FromArray2D((fun _ -> [square 10. |> at (1., 1.) |> withFill Brushes.Blue]), ListDrawerOptions(alwaysCenterOnOrigin = true))

    let expectedShapes =
        [
            square 12. |> at origin |> withContour Pens.Black
            square 10. |> at (1., 1.) |> withFill Brushes.Blue
        ]

    Assert.AreEqual(expectedShapes, shapes)

let [<Test>] ``A 2 elements array yields shapes with the right shift when grid is unconstrained``() =
    let array = [1.; 2.]
    let shapes = array |> DrawingDebugUtils.FromList((fun w -> [rectangle (w, 1.) |> at origin |> withFill Brushes.Blue]), ListDrawerOptions(bordersEnabled = false, gridCellSizes = Unconstrained))

    let expectedShapes =
        [
            rectangle (1., 1.) |> at origin |> withFill Brushes.Blue
            rectangle (2., 1.) |> at (1.5, 0.) |> withFill Brushes.Blue
        ]

    Assert.AreEqual(expectedShapes, shapes)
