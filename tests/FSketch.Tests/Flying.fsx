#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.RenderedScene)

open FSketch.Behaviours
open FSketch.Behaviours.NumericOps

type Camera = {
    Zoom: Numeric
    X: Numeric
    Y: Numeric
    Z: Numeric } with
    member this.map3dTo2d (x3d:Numeric, y3d:Numeric, z3d:Numeric) =
        let notZero = (Behaviours.lift max) (forever 0.0001)
        let ratio = NumericOps.substract(z3d, this.Z) |> notZero
        let x2d = NumericOps.multiply(NumericOps.divide(NumericOps.substract(x3d, this.X), ratio), this.Zoom)
        let y2d = NumericOps.multiply(NumericOps.divide(NumericOps.substract(y3d, this.Y), ratio), this.Zoom)
        x2d, y2d

open FSketch.Behaviours.Dsl
open FSketch.Behaviours.Builder

let camera = {
    Zoom = forever 40.
    X = time >>> (fun t -> System.Math.Cos(2. * System.Math.PI * (t+10.) / 6.) * 10.)
    Y = time >>> (fun t -> System.Math.Sin(2. * System.Math.PI * (t+10.) / 6.) * 10.)
    Z = forever 40. }

let zShift = time >*> forever 10.
let squares = shapes {
    for z in 50. .. 10. .. 300. do
        let centerX, centerY =
            System.Math.Cos(2. * System.Math.PI * (z-50.) / 60.) * 10.,
            System.Math.Sin(2. * System.Math.PI * (z-50.) / 60.) * 10.

        let points =
            [
                forever (centerX-10.), forever (centerY-10.), forever z >-> zShift
                forever (centerX+10.), forever (centerY-10.), forever z >-> zShift
                forever (centerX+10.), forever (centerY+10.), forever z >-> zShift
                forever (centerX-10.), forever (centerY+10.), forever z >-> zShift
            ] |> List.map camera.map3dTo2d

        let lines =
            points
            |> Seq.pairwise
            |> Seq.map (fun ((x1, y1), (x2, y2)) -> lineTo (NumericOps.substract(x2, x1), NumericOps.substract(y2, y1)))
            |> Seq.toList

        yield lines |> toClosedPath |> at (List.head points) |> withContour Pens.Black }

let scene = {
    Shapes = squares
    Viewport = Some {
        Center = Vector.Zero
        ViewSize = Vector(forever 100., forever 100.)
    }
}

let clip =
    [scene]
    |> Camera.record 24
    |> FSketch.HandDrawer.RedrawAllFrames
