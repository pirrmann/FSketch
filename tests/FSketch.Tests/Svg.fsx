#load "References.fsx"
References.RegisterPrinters(References.PrinterRegistration.Shapes)

open FSketch
open FSketch.Dsl
open FSketch.Builder

let simpleShapes = shapes {
    yield ellipse (200., 200.) |> at origin |> withFill Brushes.Blue
    yield rectangle (150., 50.) |> at origin |> rotatedBy (Pi/3.) |> withContourAndFill (Pens.Black, Brushes.DarkGreen)
}

let svgPath = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Crossbones.svg")
let complexShapes = Svg.SvgParser.FromFile svgPath

complexShapes |> List.take 3
complexShapes

let addBoundingBox (refSpace, styledShape) =
    let (left, top, right, bottom) =
        DrawingUtils.computeShapeBoundingBox (refSpace, styledShape)
    let w, h = right - left, bottom - top
    [
        (refSpace, styledShape)
        rectangle (w, h) |> at (left + w / 2., top + h / 2.) |> withContour Pens.Red
    ]

let addBoundingBox2 shapes =
    [
        yield! shapes
        match DrawingUtils.computeBoundingBox false shapes with
        | Some (left, top, right, bottom) ->
            let w, h = right - left, bottom - top
            yield rectangle (w, h) |> at (left + w / 2., top + h / 2.) |> withContour Pens.Blue
        | None -> ()
    ]

let timeline = Svg.SvgParser.FromFile @"C:\Code\FSharp\CSharpOnly\sketches\timeline.svg" |> DrawingUtils.recenter
let smallMe = Svg.SvgParser.FromFile @"C:\Code\FSharp\CSharpOnly\sketches\small-me.svg" |> DrawingUtils.recenter
let what = Svg.SvgParser.FromFile @"C:\Code\FSharp\CSharpOnly\sketches\what.svg" |> DrawingUtils.recenter
let archiClassique = Svg.SvgParser.FromFile @"C:\Code\FSharp\CSharpOnly\sketches\archi-classique.svg" |> DrawingUtils.recenter |> HandDrawer.RedrawByHand
let gartner = Svg.SvgParser.FromFile @"C:\Code\FSharp\CSharpOnly\sketches\gartner.svg" |> DrawingUtils.recenter |> HandDrawer.RedrawByHand

shapes {
    yield! timeline |> at origin
    yield text "2005" |> withSize 25. |> withFont (Font.UnclosedSinglePathFont "Machine Tool SanSerif") |> at (-210., 20.) |> writtenWithContourAndFill (Pens.Black, Brushes.White)
    yield text "2011" |> withSize 25. |> withFont (Font.UnclosedSinglePathFont "Machine Tool SanSerif") |> at (-65., 20.) |> writtenWithContourAndFill (Pens.Black, Brushes.White)
    yield text "2013" |> withSize 25. |> withFont (Font.UnclosedSinglePathFont "Machine Tool SanSerif") |> at (-5., 20.) |> writtenWithContourAndFill (Pens.Black, Brushes.White)
    yield text "2017" |> withSize 25. |> withFont (Font.UnclosedSinglePathFont "Machine Tool SanSerif") |> at (110., 20.) |> writtenWithContourAndFill (Pens.Black, Brushes.White)
    yield! smallMe |> at (-210., -40.)
} |> HandDrawer.RedrawByHand