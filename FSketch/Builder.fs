namespace FSketch

module Builder =
    type ShapesBuilder() =
        member x.Zero() = []
        member x.Yield(shape:RefSpace * Shape) = [shape]
        member x.YieldFrom(space:RefSpace, shapes:Shapes) = shapes |> List.map (fun (refSpace, shape) -> space + refSpace, shape)
        member x.Delay(f) = f()
        member x.Combine(f1, f2) = f1 @ f2
        member x.For(s, f:'T -> (RefSpace * Shape) list) = s |> Seq.map f |> Seq.fold (@) []

    let shapes = new ShapesBuilder()
