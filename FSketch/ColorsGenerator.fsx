let colorType = System.Type.GetType("System.Drawing.Color")

let colors =
    typeof<System.Drawing.Color>.GetProperties(
        System.Reflection.BindingFlags.Static ||| System.Reflection.BindingFlags.Public)
    |> Seq.map(fun m ->
        let color = m.GetMethod.Invoke(null, [||]) :?> System.Drawing.Color
        let toRelative x = (double x) / 255.0
        m.Name, (color.A |> toRelative, color.R |> toRelative, color.G |> toRelative, color.B |> toRelative), color.A <> 0uy)
    |> Seq.toList

let genLines = seq {
    yield "#if BEHAVIOURS"
    yield "namespace FSketch.Behaviours"
    yield "#else"
    yield "namespace FSketch"
    yield "#endif"
    yield ""
    yield "module Colors ="
    yield ""
    for (name, (a, r, g, b), _) in colors do
        yield sprintf "    let %s = ArgbColor { Alpha = ofFloat %f; R = ofFloat %f; G = ofFloat %f; B = ofFloat %f }" name a r g b
    yield ""
    yield "module Pens ="
    for (name, _, gen) in colors do
        if gen then
            yield sprintf "   let %s = { Color = Colors.%s; Thickness = ofFloat 1.0; LineJoin = LineJoin.Miter }" name name
    yield ""
    yield "module Brushes ="
    for (name, _, gen) in colors do
        if gen then
            yield sprintf "   let %s = { Color = Colors.%s }" name name
        else
            yield sprintf "   let %s = { Color = Colors.%s }" "Solid" name
}

System.IO.File.WriteAllLines(
    System.IO.Path.Combine(__SOURCE_DIRECTORY__, "Colors.fs"),
    genLines |> Seq.toArray)
