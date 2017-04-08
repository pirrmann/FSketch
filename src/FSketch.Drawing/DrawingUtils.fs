namespace FSketch

module DrawingUtils =

    let boundariesReducer b1 b2 =
        match b1, b2 with
        | Some (left1, top1, right1, bottom1), Some (left2, top2, right2, bottom2) ->
            Some (min left1 left2, min top1 top2, max right1 right2, max bottom1 bottom2)
        | Some b1, None -> Some b1
        | None, Some b2 -> Some b2
        | _ -> None

    let minMaxReducer (min1, max1) (min2, max2) = min min1 min2, max max1 max2

    let getBezierPointAt t ((xD, yD) as D, B, C) =
        let inline interpolateLine (x1, y1) (x2, y2) =
            x1 + (x2 - x1) * t, y1 + (y2 - y1) * t
        let ((xE, yE) as E) = interpolateLine (0., 0.) B
        let ((xF, yF) as F) = interpolateLine B C
        let ((xG, yG) as G) = interpolateLine C D
        let ((xH, yH) as H) = interpolateLine E F
        let ((xJ, yJ) as J) = interpolateLine F G
        interpolateLine H J

    let getPathPoints (path:Path) =
        let offset = ref Vector.Zero
        let toPoint (Vector(x, y)) = x, y
        [
            for subPath in path.SubPaths do
                offset := subPath.Start
                yield !offset |> toPoint

                for pathPart in subPath.Parts do
                match pathPart with
                | Line v ->
                    offset := !offset + v
                    yield !offset |> toPoint
                | Bezier ((Vector(vx, vy) as v), (Vector(cx1, cy1) as cp1), (Vector(cx2, cy2) as cp2)) ->
                    for t in 0.0 .. 0.05 .. 1.0 do
                        let p = getBezierPointAt t ((vx, vy), (cx1, cy1), (cx2, cy2))
                        yield !offset + (Vector p) |> toPoint
                    offset := !offset + v
                    yield !offset |> toPoint
        ]

    let computeBoundingPolygon shape =
        shape
        |> Pathetizer.ConvertToPath
        |> getPathPoints

    let computeShapeBoundingBox (refSpace, shape) =
        let boundingPolygon =
            computeBoundingPolygon shape.Shape
            |> List.map (fun p -> p * refSpace.transform)

        (
            boundingPolygon |> Seq.map fst |> Seq.min,
            boundingPolygon |> Seq.map snd |> Seq.min,
            boundingPolygon |> Seq.map fst |> Seq.max,
            boundingPolygon |> Seq.map snd |> Seq.max
        )

    let computeBoundingBox centerOnOrigin (shapes:Shapes) =
        match shapes with
        | [] -> None
        | _ ->
            shapes
            |> Seq.map (computeShapeBoundingBox >> Some)
            |> Seq.reduce boundariesReducer
            |> Option.map
                (fun (left, top, right, bottom) ->
                    if centerOnOrigin then
                        let x = max (abs left) (abs right)
                        let y = max (abs top) (abs bottom)
                        -x, -y, x, y
                    else
                        left, top, right, bottom)
