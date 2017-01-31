﻿#if BEHAVIOURS
namespace FSketch.Behaviours
#else
namespace FSketch
#endif

module Dsl =

    let rectangle (width, height) = Rectangle(Vector(width, height))
    let square (width) = Rectangle(Vector(width, width))
    let ellipse (width, height) = Ellipse(Vector(width, height))
    let circle (radius) = Ellipse(Vector(multiply (radius, ofFloat 2.0), multiply(radius, ofFloat 2.0)))
    let line (x1, y1) (x2, y2) = RefSpace.At(x1, y1), Line(Vector(substract(x2, x1), substract(y2, y1)))
    let lineTo (x, y) = Line(Vector(x, y))
    let bezierTo (x, y) (cx1, cy1) (cx2, cy2) = Bezier(Vector(x, y), Vector(cx1, cy1), Vector(cx2, cy2))
    let toPath = CompositePath
    let toClosedPath = CompositePath >> Path
    let text format = Printf.ksprintf (fun s -> { Text = s; Size = ofFloat 10. }) format
    let withSize size text = { text with Size = size }

    let withContour pen (space, shape) = space, { Shape = shape; DrawType = Contour(pen) }
    let withFill brush (space, shape) = space, { Shape = shape; DrawType = Fill(brush) }
    let withContourAndFill (pen, brush) (space, shape) = space, { Shape = shape; DrawType = ContourAndFill(pen, brush) }
    let writtenWith brush (space, text) = space, { Shape = Text(text); DrawType = Fill(brush) }

    let transform matrix refSpace = RefSpace.Transform(matrix) + refSpace

    let at (x, y) element = RefSpace.At(x, y), element
    let withZ z (refSpace, element) = { refSpace with z = z }, element
    let translatedBy (x, y) (refSpace:RefSpace, element) = ({refSpace with transform = (refSpace.transform * Transforms.translate (x, y))}, element)
    let rotatedBy alpha (refSpace:RefSpace, element) = ({refSpace with transform = refSpace.transform * (Transforms.rotate alpha)}, element)
    let scaledBy ratio (refSpace:RefSpace, element) = ({refSpace with transform = refSpace.transform * (Transforms.scale ratio)}, element)
    let scaledByX ratio (refSpace:RefSpace, element) = ({refSpace with transform = refSpace.transform * (Transforms.scaleX ratio)}, element)
    let scaledByY ratio (refSpace:RefSpace, element) = ({refSpace with transform = refSpace.transform * (Transforms.scaleY ratio)}, element)
    let xFlipped (refSpace:RefSpace, element) = ({refSpace with transform = refSpace.transform * (Transforms.scaleX (negate One))}, element)
    let yFlipped (refSpace:RefSpace, element) = ({refSpace with transform = refSpace.transform * (Transforms.scaleY (negate One))}, element)
    let origin = (Zero, Zero)

    let placedMap f (r, s) = r, f(s)

    let Pi = ofFloat System.Math.PI
