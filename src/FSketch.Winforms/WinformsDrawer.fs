namespace FSketch.Winforms

open FSketch
open System.Drawing
open System.Windows.Forms

type ThingsToDisplay =
    | Frame of Frame
    | Scene of FSketch.Behaviours.Scene

type private Canvas() =
    inherit Control()
    do
        base.SetStyle(ControlStyles.UserPaint, true)
        base.SetStyle(ControlStyles.AllPaintingInWmPaint, true)
        base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true)

[<AllowNullLiteral>]
type CanvasForm () as this =
    inherit Form()

    let canvas = new Canvas()
    let mutable thingsToDisplay:ThingsToDisplay = ThingsToDisplay.Frame { Shapes = []; Viewport = None}
    let mutable startTime = System.DateTime.Now

    let timer = new System.Windows.Forms.Timer()

    let repaint (o:obj) (e:PaintEventArgs) =
        let graphics = e.Graphics
        graphics.Clear(Color.White)
        let frame =
            match thingsToDisplay with
            | Frame frame -> frame
            | Scene scene ->
                let time = (System.DateTime.Now - startTime).TotalSeconds % scene.Duration |> scene.TimeTransform
                let shapes, viewport = (scene.Shapes, scene.Viewport) |> FSketch.Behaviours.Camera.atTime time
                {
                    Shapes = shapes
                    Viewport = viewport
                }
        frame |> Drawer.Draw graphics (canvas.Width, canvas.Height)

    do
        this.MinimumSize <- new Size(400, 400)

        canvas.Top <- 0
        canvas.Left <- 0
        canvas.BackColor <- System.Drawing.Color.White
        canvas.Dock <- DockStyle.Fill
        this.Controls.Add(canvas)

        canvas.Paint.AddHandler(new PaintEventHandler(repaint))
        this.Resize.Add(fun _ -> canvas.Invalidate())

        timer.Interval <- 1000 / 24
        timer.Tick.Add(fun _ -> canvas.Invalidate())

    member this.ThingsToDisplay
        with get() = thingsToDisplay
        and set(value) =
            thingsToDisplay <- value
            canvas.Invalidate()

    member this.RestartTimer() =
        startTime <- System.DateTime.Now
        timer.Start()

    member this.StopTimer() =
        timer.Stop()

module WinformsDrawer =
    let mutable (window:CanvasForm) = null

    let ensureWindowExists() = 
        if isNull window || window.IsDisposed then
            window <- new CanvasForm()
            window.Text <- "Drawing debug view"
            window.Show()
        window

    let Draw (frame:Frame) =
        let window = ensureWindowExists()
        window.ThingsToDisplay <- ThingsToDisplay.Frame frame
        window.StopTimer()
        window

    let Play (scene:FSketch.Behaviours.Scene) =
        let window = ensureWindowExists()
        window.ThingsToDisplay <- ThingsToDisplay.Scene scene
        window.RestartTimer()
        window
