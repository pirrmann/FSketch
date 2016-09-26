module Program

open System
open System.Drawing
open System.Windows.Forms

open FSketch
open FSketch.Dsl
open FSketch.Builder

[<EntryPoint>]
[<STAThread>]
let main argv = 
//    let test = shapes {
//        yield line (0., 0.) (10., 0.) |> withPen Pens.Black
//        yield line (0., 0.) (0., 10.) |> withPen Pens.Black
//        yield square 10. |> at (-50., -50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
//        yield square 10. |> at (-50., 50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
//        yield square 10. |> at (50., 50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
//        yield square 10. |> at (50., -50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
//        yield square 100. |> at origin |> withContour Pens.Blue
//    }
//
//    let testList = [0. .. 9.]
//    let test2 = testList |> DrawingDebugUtils.FromList (fun i -> [circle 10. |> at (10. * cos (Pi * float i / 5.), 10. * sin (Pi * float i / 5.)) |> withFill Brushes.Red])
//    let test3 = testList |> DrawingDebugUtils.FromList ((fun i -> circle (i * 10.)), ListDrawerOptions(bordersEnabled=false))
//    
//    let testArray = Array2D.init 10 20 (fun x y -> float (x + y))
//    let test4 = testArray |> DrawingDebugUtils.FromArray2D (fun i -> [circle i |> at origin |> withFill Brushes.Magenta])

    let getTestRectangle (x, y) =
        [
            match x, y with
            | 0, 1 -> yield rectangle (20., 10.) |> at origin |> withFill Brushes.Green
            | 1, 0 -> yield rectangle (10., 20.) |> at origin |> withFill Brushes.Blue
            | 1, 1 -> yield rectangle (20., 20.) |> at origin |> withFill Brushes.Red
            | _ -> ()
        ]

    let test5 =
        Array2D.init 2 2 (fun x y -> x, y)
        |> DrawingDebugUtils.FromArray2D (getTestRectangle, ListDrawerOptions(bordersEnabled = false, gridCellSizes = Unconstrained))

    Application.Run(FSketch.Winforms.WinformsDrawer.Draw test5)

    0