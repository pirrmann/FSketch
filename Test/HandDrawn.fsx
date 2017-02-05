#load "References.fsx"
References.RegisterPrinters()

open FSketch
open FSketch.Dsl
open FSketch.Builder

let simpleShapes = shapes {
  yield rectangle (150., 50.) |> at origin |> withContour Pens.Black
  yield ellipse (140., 40.) |> at origin |> withContourAndFill (Pens.Blue, Brushes.LightBlue)
  yield rectangle (150., 50.) |> at (200., 0.) |> withContour Pens.Black
  yield ellipse (140., 40.) |> at (200., 0.) |> withContourAndFill (Pens.Red, Brushes.LightPink)
}

#load "FSharpLogo.fsx"

let fSharpAdvert = shapes {
  yield! FSharpLogo.Logo |> at origin
  yield text "F# rocks" |> withSize 48. |> at (110., 150.) |> writtenWithContourAndFill (Pens.Black, Brush.FromColor FSharpLogo.DarkBlue)
  yield text "it just worked the 1st time" |> withSize 24. |> at (120., 180.) |> writtenWithContour {Pens.Default with Color = FSharpLogo.LightBlue}
}

fSharpAdvert |> HandDrawer.RedrawByHand