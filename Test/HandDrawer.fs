module HandDrawer

open FSketch
open FSketch.Dsl
open FSketch.Builder

let rand = new System.Random()
let moveRatio = 0.5

let rec splitLine (x, y) =
    let length = sqrt (x*x + y*y)
    if length > 10. then
        let splitDistance = 4. + rand.NextDouble() * 2.
        let thX, thY = x * splitDistance / length, y * splitDistance / length
        let x' = thX + (-1. + rand.NextDouble() * 2.) * moveRatio
        let y' = thY + (-1. + rand.NextDouble() * 2.) * moveRatio
        seq {
            yield Line(Vector(x', y'))
            yield! splitLine (x - x', y - y')
        }
    else
        Seq.singleton (Line(Vector(x, y)))

let splitBezierAt t ((xD, yD) as D, B, C) =
    let interpolateLine (x1, y1) (x2, y2) =
        x1 + (x2 - x1) * t, y1 + (y2 - y1) * t
    let ((xE, yE) as E) = interpolateLine (0., 0.) B
    let ((xF, yF) as F) = interpolateLine B C
    let ((xG, yG) as G) = interpolateLine C D
    let ((xH, yH) as H) = interpolateLine E F
    let ((xJ, yJ) as J) = interpolateLine F G
    let ((xK, yK) as K) = interpolateLine H J

    let xK' = xK + (-1. + rand.NextDouble() * 2.) * moveRatio
    let yK' = yK + (-1. + rand.NextDouble() * 2.) * moveRatio

    let D' = xD - xK', yD - yK'
    let J' = xJ - xK', yJ - yK'
    let G' = xG - xK', yG - yK'

    let K' = xK', yK'

    (K', E, H), (D', J', G')

let rec splitBezier (x, y) (cx1, cy1) (cx2, cy2) =
    let length = sqrt (x*x + y*y)
    if length > 10. then
        let splitDistance = 4. + rand.NextDouble() * 2.
        let (K, E, H), (D, J, G) =
            ((x, y), (cx1, cy1), (cx2, cy2))
            |> splitBezierAt (splitDistance / length)

        seq {
            yield Bezier(Vector(K),Vector(E),Vector(H))
            yield! splitBezier D J G
        }
    else
        Seq.singleton (Bezier(Vector(x, y),Vector(cx1, cy1),Vector(cx2, cy2)))

let rec handDrawnPath path = seq {
    match path with
    | Line(Vector(x, y)) ->
        yield! splitLine (x, y)
    | Bezier(Vector(x, y), Vector(cx1, cy1), Vector(cx2, cy2)) ->
        yield! splitBezier (x, y) (cx1, cy1) (cx2, cy2)
    | CompositePath pathParts ->
        yield! pathParts |> Seq.collect handDrawnPath }

let handDrawn (refSpace, { Shape = shape; DrawType = drawType }) =
    match shape with
    | Rectangle(Vector(width,height)) ->
        let path =
            [
                Line(Vector(width,0.))
                Line(Vector(0.,height))
                Line(Vector(-width,0.))
                Line(Vector(0.,-height))
            ] |> CompositePath
        Some (refSpace, path)
    | Ellipse(Vector(width,height)) ->
        let x = width / 2.0
        let y = height / 2.0
        let kappa = 0.5522848
        let ox = x * kappa  // control point offset horizontal
        let oy = y * kappa // control point offset vertical
        let path =
            [
                Bezier (Vector(x, -y), Vector(0., -oy), Vector(x-ox, -y))
                Bezier (Vector(x, y), Vector(ox, 0.), Vector(x, y-oy))
                Bezier (Vector(-x, y), Vector(0., oy), Vector(-x+ox, y))
                Bezier (Vector(-x, -y), Vector(-ox, 0.), Vector(-x, oy-y))
            ] |> CompositePath
        (refSpace, path) |> translatedBy (-x, 0.) |> Some
    | Path path -> Some (refSpace, path)
    | Text text -> None
    |> Option.map (fun (refSpace, path) -> refSpace, handDrawnPath path |> Seq.toList |> CompositePath)
    |> Option.map (fun (refSpace, path) -> refSpace, { Shape = Path path; DrawType = drawType })
