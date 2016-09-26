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
let testCosAndSinus1 = testList |> DrawingDebugUtils.FromList ((fun i -> [circle 10. |> at (10. * cos (Pi * float i / 5.), 10. * sin (Pi * float i / 5.)) |> withFill Brushes.Red]), ListDrawerOptions(bordersEnabled = true, bordersPen = { Pens.Black with Thickness = 0.1 }, gridCellSizes = Unconstrained))
let testCosAndSinus2 = testList |> DrawingDebugUtils.FromList ((fun i -> [circle 10. |> at (10. * cos (Pi * float i / 5.), 10. * sin (Pi * float i / 5.)) |> withFill Brushes.Red]), ListDrawerOptions(bordersEnabled = true, bordersPen = { Pens.Black with Thickness = 0.1 }, alwaysCenterOnOrigin = true))
    
let testArray = Array2D.init 10 20 (fun x y -> x + y)
let testCircles = testArray |> DrawingDebugUtils.FromArray2D (fun i -> circle (float i))

let chessBoard =
    Array2D.init 8 8 (fun x y -> (x + y) % 2 = 0) 
    |> DrawingDebugUtils.FromArray2D((fun b -> [square 100. |> at origin |> withFill (if b then Brushes.Black else Brushes.White)]), ListDrawerOptions(bordersEnabled = false))

let visualProof =
    let brushes = [|Brushes.Red; Brushes.Blue; Brushes.Green; Brushes.Yellow|]
    Array2D.init 4 4 (fun x y -> y+1, x+1, brushes.[max x y])
    |> DrawingDebugUtils.FromArray2D((fun (w, h, b) -> [rectangle(float w, float h) |> at origin |> withFill b]), ListDrawerOptions(bordersEnabled = true, bordersPen = { Pens.Black with Thickness = 0.1 }, gridCellSizes = Unconstrained))

let products =
    let getShapes = function
        | 0, 0 -> []
        | 0, i
        | i, 0 ->
            [
                sprintf "%X" i |> at origin |> writtenWith Brushes.Blue
            ]
        | i, j when i = j ->
            [
                sprintf "%X" (i*j) |> at origin |> writtenWith Brushes.Red
            ]
        | i, j ->
            [
                sprintf "%X" (i*j) |> at origin |> writtenWith Brushes.Black
            ]

    Array2D.init 17 17 (fun x y -> x, y)
    |> DrawingDebugUtils.FromArray2D(getShapes, ListDrawerOptions(gridCellSizes = AllSquare))

let testWithDefaultFormatter = 
    Array2D.init 8 8 (fun x y -> x * y)
    |> DrawingDebugUtils.AutoDraw

let aliceWonderlandCypherTable =
    Array2D.init 27 27 (fun x y -> if x = 0 && y = 0 then ' ' else (x + y - 1) % 26 + (int 'A') |> char)
    |> DrawingDebugUtils.AutoDraw
