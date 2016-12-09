#r "System.Xml.Linq.dll"

#r "bin/debug/FSketch.dll"
#r "bin/debug/FSketch.Svg.dll"
#r "bin/debug/FSketch.Drawing.dll"
#r "bin/debug/FSketch.Winforms.dll"

open FSketch

fsi.AddPrintTransformer(fun (shapes:Shapes) ->
                            shapes |> Winforms.WinformsDrawer.Draw |> ignore
                            null)

Svg.SvgParser.FromFile @"C:\Users\Pierre\Pictures\Léo\Skull_and_crossbones.svg"
