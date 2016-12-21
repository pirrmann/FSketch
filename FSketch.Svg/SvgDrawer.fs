namespace FSketch.Svg

open FSketch
open FSketch.Dsl
open FSketch.Builder

module internal SvgDrawerHelper =
    let toHexaColor color =
        let r, g, b = int (color.R * 255.0), int (color.G * 255.0), int (color.B * 255.0)
        if color.Alpha > 0.999 then
            sprintf "#%02x%02x%02x" r g b
        else
            sprintf "rgba(%d, %d, %d, %f)" r g b color.Alpha

    let getPathString (path:Path) =
        let rec getPathStringParts (path:Path) = seq {
            match path with
            | Line v ->
                yield sprintf "l %f,%f" v.X v.Y
            | Bezier (v, t1, t2) ->
                yield sprintf "c %f,%f %f,%f %f,%f" t1.X t1.Y (v.X+t2.X) (v.Y+t2.Y) v.X v.Y
            | CompositePath ps ->
                yield! ps |> Seq.collect getPathStringParts }
        path |> getPathStringParts |> String.concat " "

    let toSvgElement (refSpace:RefSpace, styledShape) =
        let transform =
                match refSpace.transform with
                | TransformMatrix((1., 0.), (0., 1.), (0., 0.)) -> ""
                | TransformMatrix((1., 0.), (0., 1.), (mx, my)) ->
                    sprintf @" transform=""translate(%f,%f)""" mx my
                | TransformMatrix((m11, m12), (m21, m22), (mx, my)) ->
                    sprintf @" transform=""matrix(%f,%f,%f,%f,%f,%f)""" m11 m12 m21 m22 mx my

        let drawType = styledShape.DrawType
        let fill =
            match drawType with
            | Fill brush
            | ContourAndFill (_, brush) -> sprintf "fill:%s" (toHexaColor brush.Color)
            | Contour _ -> "fill:none"
        let stroke =
            match drawType with
            | Fill _ -> "stroke:none"
            | ContourAndFill (pen, _)
            | Contour pen -> sprintf "stroke:%s; stroke-width: %f;" (toHexaColor pen.Color) pen.Thickness

        let style = sprintf @"style=""%s;%s""" fill stroke

        match styledShape.Shape with
        | Rectangle size ->
            sprintf @"<rect x=""%f"" y=""%f"" width=""%f"" height=""%f"" %s%s/>" (-size.X/2.) (-size.Y/2.)size.X size.Y style transform
        | Ellipse size ->
            sprintf @"<ellipse cx=""0"" cy=""0"" rx=""%f"" ry=""%f"" %s%s/>" (size.X/2.) (size.Y/2.) style transform
        | Path path ->
            let pathString = getPathString path
            sprintf @"<path d=""m 0,0 %s"" %s%s/>" pathString style transform
        | Text _ -> failwith "Not supported yet"

    let toSvgElements shapesToTranslate =
        match shapesToTranslate |> DrawingUtils.computeBoundingBox false with
        | Some (left, top, right, bottom) ->
            let transletedShapes = shapes { yield! shapesToTranslate |> at (-left, -top) }
            seq {
                yield  @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>"
                yield @"<svg"
                yield @"xmlns:dc=""http://purl.org/dc/elements/1.1/"""
                yield @"xmlns:cc=""http://creativecommons.org/ns#"""
                yield @"xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"""
                yield @"xmlns:svg=""http://www.w3.org/2000/svg"""
                yield @"xmlns=""http://www.w3.org/2000/svg"""
                yield sprintf @"width=""%f""" (right-left)
                yield sprintf @"height=""%f""" (bottom-top)
                yield @"version=""1.0"">"
                yield! transletedShapes |> Seq.map toSvgElement
                yield "</svg>" }
        | None -> Seq.empty

module SvgDrawer =
    let Draw (shapes:Shapes) =
        shapes |> SvgDrawerHelper.toSvgElements |> String.concat "\n"