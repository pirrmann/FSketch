(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Tutorial
========================

I'll write that some time later...

*)
#r "FSketch.dll"
open FSketch
open FSketch.Builder
open FSketch.Dsl

let simpleShapes = shapes {
  yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
  yield ellipse (140., 40.) |> at origin |> withContourAndFill (Pens.Blue, Brushes.LightBlue)
  yield rectangle (150., 50.) |> at (200., 0.) |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (200., 0.) |> withContourAndFill (Pens.Red, Brushes.LightPink)
}