namespace FSketch

type Frame = {
    Shapes: Shapes
    Viewport: Viewport option }
    with static member FromShapes shapes = { Shapes = shapes; Viewport = None }
