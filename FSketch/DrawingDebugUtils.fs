namespace FSketch

open Dsl

type GridCellSizes = | SameHeight | SameWidth | AllSquare | Unconstrained

type ListDrawerOptions (?defaultPen:Pen, ?defaultBrush:Brush, ?bordersEnabled:bool, ?bordersPen:Pen, ?gridCellSizes:GridCellSizes) =
    let defaultPen = defaultArg defaultPen Pens.Black

    member val DefaultPen = defaultPen
    member val DefaultBrush = defaultArg defaultBrush Brushes.Blue
    member val BordersEnabled = defaultArg bordersEnabled true
    member val BordersPen = defaultArg bordersPen defaultPen
    member val GridCellSizes = defaultArg gridCellSizes AllSquare
    
    static member Default = ListDrawerOptions()

module internal DrawingDebugUtilsInternal =

    let fromItemsShapes (options:ListDrawerOptions) itemsShapes =

        let maxY, maxX = Array2D.length1 itemsShapes - 1, Array2D.length2 itemsShapes - 1

        let shapesAndBoundaries =
            itemsShapes
            |> Array2D.map (fun shapes -> shapes, DrawingUtils.computeBoundingBox shapes)

        let getMaxBoundaries xRange yRange =
            seq {
                for y in yRange do
                for x in xRange do
                yield shapesAndBoundaries.[y, x] }
            |> Seq.map snd
            |> Seq.reduce DrawingUtils.boundariesReducer

        let rowHeights, colWidths =
            let rowHeights' =
                seq {
                    for y in [0..maxY] do
                    let _, cellTop, _, cellBottom = getMaxBoundaries [0..maxX] [y..y]
                    yield cellBottom - cellTop }
                |> Seq.toArray

            let colWidths' =
                seq {
                    for x in [0..maxX] do
                    let cellLeft, _, cellRight, _ = getMaxBoundaries [x..x] [0..maxY]
                    yield cellRight - cellLeft }
                |> Seq.toArray

            match options.GridCellSizes with
            | AllSquare ->
                let maxHeight, maxWidth = Seq.max rowHeights', Seq.max colWidths'
                rowHeights' |> Array.map (fun _ -> maxHeight), colWidths' |> Array.map (fun _ -> maxWidth)
            | SameHeight ->
                let maxHeight = Seq.max rowHeights'
                rowHeights' |> Array.map (fun _ -> maxHeight), colWidths'
            | SameWidth ->
                let maxWidth = Seq.max colWidths'
                rowHeights', colWidths' |> Array.map (fun _ -> maxWidth)
            | Unconstrained ->
                rowHeights', colWidths'

        [
            for y in 0 .. maxY do
            for x in 0 .. maxX do
                let shapes, (shapesLeft, shapesTop, shapesRight, shapesBottom) = shapesAndBoundaries.[y, x]

                let shift =
                    colWidths |> Seq.take x |> Seq.sum,
                    rowHeights |> Seq.take y |> Seq.sum

                for shape in shapes do
                    if options.BordersEnabled then
                        yield rectangle (colWidths.[x], rowHeights.[y]) |> at shift |> withContour options.BordersPen

                    yield shape |> translatedBy shift
        ]

    let Transform options mapper input =
        let options = defaultArg options ListDrawerOptions.Default
        let inputAsArray = List.toArray input
        Array2D.init 1 inputAsArray.Length (fun _ x -> mapper options inputAsArray.[x])
        |> fromItemsShapes options

    let Transform2D options mapper =
        let options = defaultArg options ListDrawerOptions.Default
        Array2D.map (mapper options) >> fromItemsShapes options

    let fromShapes mapper (options:ListDrawerOptions) = mapper

    let fromShape mapper (options:ListDrawerOptions) =
        mapper >> at origin >> List.singleton

    let fromPath mapper (options:ListDrawerOptions) =
        mapper >> (fun p -> Path(p, options.DefaultPen)) >> at origin >> List.singleton

    let toShape (options:ListDrawerOptions) closedShape =
        match closedShape with
        | Rectangle _
        | Ellipse _ -> ClosedShape(closedShape, Fill options.DefaultBrush)
        | ClosedPath _ -> ClosedShape(closedShape, Contour options.DefaultPen)

    let fromClosedShape mapper (options:ListDrawerOptions) =
        mapper >> toShape options >> (at origin) >> List.singleton

open DrawingDebugUtilsInternal

type DrawingDebugUtils =

    static member FromList<'a> (mapper: 'a -> Shapes, ?options: ListDrawerOptions) =
        Transform options (fromShapes mapper)

    static member FromList<'a> (mapper: 'a -> Shape, ?options: ListDrawerOptions) =
        Transform options (fromShape mapper)

    static member FromList<'a> (mapper: 'a -> Path, ?options: ListDrawerOptions) =
        Transform options (fromPath mapper)

    static member FromList<'a> (mapper: 'a -> ClosedShape, ?options: ListDrawerOptions) =
        Transform options (fromClosedShape mapper)

    static member FromArray2D<'a> (mapper: 'a -> Shapes, ?options: ListDrawerOptions) =
        Transform2D options (fromShapes mapper)

    static member FromArray2D<'a> (mapper: 'a -> Shape, ?options: ListDrawerOptions) =
        Transform2D options (fromShape mapper)

    static member FromArray2D<'a> (mapper: 'a -> Path, ?options: ListDrawerOptions) =
        Transform2D options (fromPath mapper)

    static member FromArray2D<'a> (mapper: 'a -> ClosedShape, ?options: ListDrawerOptions) =
        Transform2D options (fromClosedShape mapper)
