open System
open System.Drawing
open System.Windows.Forms

open FSketch
open FSketch.Dsl
open FSketch.Builder

[<EntryPoint>]
[<STAThread>]
let main argv = 
    let test = shapes {
        yield line (0., 0.) (10., 0.) |> withPen Pens.Black
        yield line (0., 0.) (0., 10.) |> withPen Pens.Black
        yield square 10. |> at (-50., -50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
        yield square 10. |> at (-50., 50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
        yield square 10. |> at (50., 50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
        yield square 10. |> at (50., -50.) |> rotatedBy (Pi / 4.) |> withContour Pens.Red
        yield square 100. |> at origin |> withContour Pens.Blue
    }

    let testList = [0. .. 9.]
    let test2 = testList |> DrawingDebugUtils.FromList (fun i -> [circle 10. |> at (10. * cos (Pi * float i / 5.), 10. * sin (Pi * float i / 5.)) |> withFill Brushes.Red])
    let test3 = testList |> DrawingDebugUtils.FromList ((fun i -> circle (i * 10.)), ListDrawerOptions(bordersEnabled=false))
    
    let testArray = Array2D.init 10 20 (fun x y -> float (x + y))
    let test4 = testArray |> DrawingDebugUtils.FromArray2D (fun i -> [circle i |> at origin |> withFill Brushes.Magenta])

    Application.Run(FSketch.Winforms.WinformsDrawer.Draw test3)

    0