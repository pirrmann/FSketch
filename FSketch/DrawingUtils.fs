namespace FSketch

module DrawingUtils =
    
    let boundariesReducer (left1, top1, right1, bottom1) (left2, top2, right2, bottom2) = min left1 left2, min top1 top2, max right1 right2, max bottom1 bottom2
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

    let computeBoundingBox (shapes:Shapes) =
        shapes |> Seq.map computeShapeBoundingBox |> Seq.reduce boundariesReducer
