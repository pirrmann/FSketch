#r "../FSketch/bin/debug/FSketch.dll"
#r "../FSketch.Winforms/bin/debug/FSketch.Winforms.dll"

open FSketch

fsi.AddPrintTransformer(fun (shapes:Shapes) ->
                            shapes |> Winforms.WinformsDrawer.Draw |> ignore
                            null)

open FSketch.Dsl
open FSketch.Builder

let test = shapes {
    for x in -25. .. 25. .. 25. do
    for y in -25. .. 25. .. 25. do
    for size in 50. .. 50. .. 150. do
    yield ellipse (size, size) |> at (x-size/2., y-size/2.) |> withContour Pens.Red
    yield rectangle (size, size) |> at (x-size/2., y-size/2.) |> withContour Pens.Blue
}

let testList = [0. .. 9.]
let test2 = testList |> DrawingDebugUtils.FromList ((fun i -> [circle 10. |> at (10. * cos (Pi * float i / 5.), 10. * sin (Pi * float i / 5.)) |> withFill Brushes.Red]), new ListDrawerOptions(bordersEnabled = true, bordersPen = { Pens.Black with Thickness = 0.1 }, gridCellSizes = Unconstrained))
    
let testArray = Array2D.init 10 20 (fun x y -> x + y)
let test3 = testArray |> DrawingDebugUtils.FromArray2D (fun i -> circle (float i))

let chessBoard = Array2D.init 8 8 (fun x y -> if (x + y) % 2 = 0 then Brushes.Black else Brushes.White)
let test4 = chessBoard |> DrawingDebugUtils.FromArray2D((fun b -> [square 100. |> at origin |> withFill b]), new ListDrawerOptions(bordersEnabled = false))

let visualProof =
    let brushes = [|Brushes.Red; Brushes.Blue; Brushes.Green; Brushes.Yellow|]
    Array2D.init 4 4 (fun x y -> y+1, x+1, brushes.[max x y])
    |> DrawingDebugUtils.FromArray2D((fun (w, h, b) -> [rectangle(float w, float h) |> at origin |> withFill b]), new ListDrawerOptions(bordersEnabled = true, bordersPen = { Pens.Black with Thickness = 0.1 }, gridCellSizes = Unconstrained))
