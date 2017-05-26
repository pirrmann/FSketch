#load "ToolsForIntro.fsx"
open FSketch
open ToolsForIntro

[|
    "Hello everyone!"
    "I'm happy to see you there"
|]

Array.singleton """@pirrmann
Blah blah blah
Computation expressions
DSLs
Blah blah blah
Type providers
(actually not this time)"""

open FSketch.Dsl
open FSketch.Builder

Array.singleton """Step 1
Low-level verbose stuff
(but composable)"""

let redCircle = RefSpace.Origin, { Shape = Ellipse(Vector(100., 100.)); DrawType = Fill Brushes.Red }

[redCircle]

let blackSquare = RefSpace.Origin, { Shape = Rectangle(Vector(100., 100.)); DrawType = Contour Pens.Black }
[blackSquare]

// And now... composition!
[redCircle;blackSquare]

Array.singleton """Step 2
Improving the language"""

// Computation expression!
shapes {
    yield redCircle
    yield blackSquare
}

let success = RefSpace.Origin, { Shape = Text({Text = "Great success!"; Size = 10.; Font = Font.Arial; VerticalAlign = Top; HorizontalAlign = Left}); DrawType = Fill Brushes.Black }
[success]

// Composition again!
shapes {
    yield redCircle
    yield blackSquare
    yield success
}

Array.singleton """Step 3
Domain Specific Language"""

shapes {
    yield circle 50. |> at origin |> withFill Brushes.Red
    yield square 100. |> at origin |> withContour Pens.Black
    yield text "Great" |> at (0., -6.) |> writtenWithFill Brushes.White
    yield text "success!" |> withSize 16. |> at (0., 6.) |> writtenWithFill Brushes.White
}

shapes {
    yield! FSharpLogo.Logo |> at (-62., -63.)
    yield text "F# rocks!" |> withSize 16. |> at (0., 65.) |> writtenWithFill Brushes.Black
}

Array.singleton """Step 4
Wire things up
and get data to draw"""

// just a list
[1..10] |> DrawingDebugUtils.AutoDraw

// just an 2D array
Array2D.init 10 10 (fun x y -> x, y) |> DrawingDebugUtils.AutoDraw

// alice in Wonderland cypher table
Array2D.init 27 27 (fun x y -> if x = 0 && y = 0 then '*' else (x + y - 1) % 26 + (int 'A') |> char) |> DrawingDebugUtils.AutoDraw

Array.singleton """Step 5
Use this to actually
draw what I wanted to
draw in the first place..."""
