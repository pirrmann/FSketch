namespace FSketch

open Microsoft.FSharp.Math

type Vector = Vector of float * float with
    static member Zero = Vector (0.0, 0.0)
    static member (+) (Vector(x1, y1) , Vector(x2, y2)) = Vector(x1 + x2, y1 + y2)
    static member (-) (Vector(x1, y1) , Vector(x2, y2)) = Vector(x1 - x2, y1 - y2)
    member this.X = match this with | Vector(x, _) -> x
    member this.Y = match this with | Vector(_, y) -> y
    override this.ToString() = sprintf "%A" this

type TransformMatrix =
    | TransformMatrix of (float * float) * (float * float) * (float * float) with
    member this.x = match this with | TransformMatrix(_,_,(x,_)) -> x
    member this.y = match this with | TransformMatrix(_,_,(_,y)) -> y
    static member (*) (x, y) =
        match x, y with
        | TransformMatrix((m11, m12), (m21, m22), (mx, my)), TransformMatrix((n11, n12), (n21, n22), (nx, ny)) ->
            TransformMatrix((m11 * n11 + m12 * n21, m11 * n12 + m12 * n22),
                            (m21 * n11 + m22 * n21, m21 * n12 + m22 * n22),
                            (mx * n11 + my * n21 + nx, mx * n12 + my * n22 + ny))
    static member (*) (x, ratio) =
        match x with
        | TransformMatrix((m11, m12), (m21, m22), (mx, my)) ->
            TransformMatrix(((m11 - 1.0) * ratio + 1.0, m12 * ratio),
                            (m21 * ratio, (m22 - 1.0) * ratio + 1.0),
                            (mx * ratio, my * ratio))
    static member (*) ((x, y), TransformMatrix((m11, m12), (m21, m22), (mx, my))) =
        x * m11 + y * m21 + mx, x * m12 + y * m22 + my 
    override this.ToString() = sprintf "%A" this

module Transforms =
    let id = TransformMatrix((1.0, 0.0), (0.0, 1.0), (0.0, 0.0))
    let rotate alpha = TransformMatrix((cos alpha, sin alpha), (-sin alpha, cos alpha), (0.0, 0.0))
    let translate (x, y) = TransformMatrix((1.0, 0.0), (0.0, 1.0), (x, y))
    let scale ratio = TransformMatrix((ratio, 0.0), (0.0, ratio), (0.0, 0.0))
    let scaleX ratio = TransformMatrix((ratio, 0.0), (0.0, 1.0), (0.0, 0.0))
    let scaleY ratio = TransformMatrix((1.0, 0.0), (0.0, ratio), (0.0, 0.0))
    let flipX = scaleX (-1.0)
    let flipY = scaleY (-1.0)

type RefSpace = { transform:TransformMatrix; z:float } with
    static member Origin = { transform = Transforms.translate (0.0, 0.0); z = 0.0 }
    static member At(x, y) = { transform = Transforms.translate (x, y); z = 0.0 }
    static member Transform(transform) = { transform = transform; z = 0.0 }
    static member (+) (s1, s2) = { transform = s2.transform * s1.transform; z = s1.z + s2.z }
    static member (*) (s, ratio) = { transform = s.transform * ratio; z = s.z * ratio }
    member this.x = this.transform.x
    member this.y = this.transform.y
    override this.ToString() = sprintf "%A" this

type Color = { Alpha:float; R: float; G: float; B: float}

type Pen = { Color:Color; Thickness:float }

type Brush = { Color:Color } with
    static member FromColor(color) = { Color = color }

type Path =
    | Line of Vector:Vector
    | Bezier of Vector:Vector * tangent1:Vector * tangent2:Vector
    | CompositePath of Path list
    with member x.End = match x with
                        | Line v -> v
                        | Bezier (v, _, _) -> v
                        | CompositePath path -> path |> List.map (fun p -> p.End) |> List.sum

type ClosedShape =
    | Rectangle of Size:Vector
    | Ellipse of Size:Vector
    | ClosedPath of Path with
    override this.ToString() = sprintf "%A" this

type DrawType =
    | Contour of Pen
    | Fill of Brush
    | ContourAndFill of Pen * Brush with
    member x.Pen = match x with | Contour(p) | ContourAndFill(p, _) -> Some p | _ -> None
    member x.Brush = match x with | Fill(b) | ContourAndFill(_, b) -> Some b | _ -> None
    override this.ToString() = sprintf "%A" this

type Text = { Text:string; Size: float }

type Shape =
    | ClosedShape of ClosedShape * DrawType
    | Path of Path * Pen
    | Text of Text * Brush with
    override this.ToString() = sprintf "%A" this
and Shapes = (RefSpace * Shape) list