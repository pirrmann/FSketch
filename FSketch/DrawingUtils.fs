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

    let computeBoundingPolygon shape =
        match shape with
        | ClosedShape (cs, _) ->
            match cs with
            | Rectangle(Vector(w, h))
            | Ellipse(Vector(w, h)) ->
                [
                    -w/2., -h/2.
                    w/2. , -h/2.
                    w/2. , h/2.
                    -w/2., h/2.
                ]
            | ClosedPath p ->
                //TODO
                [
                    0., 0.
                    1., 0.
                    1., 1.
                    0., 1.
                ]
        | Path (p, _) ->
            //TODO
            [
                0., 0.
                1., 0.
                1., 1.
                0., 1.
            ]
        

    let computeShapeBoundingBox (refSpace, shape) =
        let boundingPolygon =
            computeBoundingPolygon shape
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
