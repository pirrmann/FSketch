#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

open FSketch
open FSketch.Dsl
open FSketch.Builder

let darkBlue = ArgbColor { Alpha = 1.000000; R = 0.215686275; G = 0.545098039; B = 0.729411765 }
let lightBlue = ArgbColor { Alpha = 1.000000; R = 0.188235294; G = 0.725490196; B = 0.858823529 }

let coolLogo = shapes {
    yield [
            lineTo (56., -56.)
            lineTo (0., 28.)
            lineTo (-28., 28.)
            lineTo (28., 28.)
            lineTo (0., 28.)
        ] |> toClosedPath
        |> at (5., 63.)
        |> withFill { Color = darkBlue }

    yield [
            lineTo (20., -20.)
            lineTo (0., 40.)
        ] |> toClosedPath
        |> at (41., 63.)
        |> withFill { Color = darkBlue }

    yield [
            lineTo (-58., -56.)
            lineTo (0., 28.)
            lineTo (28., 28.)
            lineTo (-28., 28.)
            lineTo (0., 28.)
        ] |> toClosedPath
        |> at (121., 63.)
        |> withFill { Color = lightBlue }
}


fsi.AddPrintTransformer(fun (shapes:Shapes) ->
                            shapes |> Winforms.WinformsDrawer.Draw |> ignore
                            null)

fsi.AddPrintTransformer(fun (strings:string array) ->
                            Array2D.init strings.Length 1 (fun i _ -> strings.[i])
                            |> DrawingDebugUtils.FromArray2D(ListDrawerOptions(bordersEnabled = false, gridCellSizes = Unconstrained))
                            |> Winforms.WinformsDrawer.Draw
                            |> ignore
                            null)
