#load "References.fsx"
References.RegisterPrinters()

type Camera = {
    Zoom: float
    X: float
    Y: float
    Z: float } with
    member this.convertLength z len =
        let ratio = z - this.Z
        if ratio = 0. then 0. else len / ratio * this.Zoom

    member this.map3dTo2d (x3d, y3d, z3d) =
        let convertLength'  = this.convertLength z3d
        let x2d = (convertLength' (x3d - this.X))
        let y2d = (convertLength' (y3d - this.Y))
        (x2d, y2d)

open FSketch
open FSketch.Dsl
open FSketch.Builder

let camera = {
    Zoom = 400.
    X = 20.
    Y = -20.
    Z = 40. }

let square = shapes {
    for z in 100. .. 10. .. 300. do
        let points =
            [
                -10., -10., z
                10., -10., z
                10., 10., z
                -10., 10., z
            ] |> List.map camera.map3dTo2d

        let lines =
            points
            |> Seq.pairwise
            |> Seq.map (fun ((x1, y1), (x2, y2)) -> lineTo (x2 - x1, y2 - y1))
            |> Seq.toList

        yield lines |> toClosedPath |> at (List.head points) |> withContour Pens.Black } |> HandDrawer.RedrawByHand