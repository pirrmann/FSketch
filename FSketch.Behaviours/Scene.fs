namespace FSketch.Behaviours

type Scene = {
    Duration: float
    TimeTransform: float -> float
    Shapes: Shapes
    Viewport: Viewport option
}
