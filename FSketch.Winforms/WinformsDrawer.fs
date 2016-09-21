namespace FSketch.Winforms

open FSketch
open System.Drawing
open System.Windows.Forms

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
    let mutable shapes:Shapes = []

    let repaint (o:obj) (e:PaintEventArgs) =
        let graphics = e.Graphics
        graphics.Clear(Color.White)
        shapes |> Drawer.Draw graphics (canvas.Width, canvas.Height)

    do
        this.MinimumSize <- new Size(400, 400)

        canvas.Top <- 0
        canvas.Left <- 0
        canvas.BackColor <- System.Drawing.Color.White
        canvas.Dock <- DockStyle.Fill
        this.Controls.Add(canvas)

        canvas.Paint.AddHandler(new PaintEventHandler(repaint))
        this.Resize.Add(fun _ -> canvas.Invalidate())

    member this.Shapes
        with get() = shapes
        and set(value) =
            shapes <- value
            canvas.Invalidate()

module WinformsDrawer =
    let mutable (window:CanvasForm) = null

    let ensureWindowExists() = 
        if isNull window || window.IsDisposed then
            window <- new CanvasForm()
            window.Text <- "Drawing debug view"
            window.Show()
        window

    let Draw (shapes:Shapes) =
        let window = ensureWindowExists()
        window.Shapes <- shapes
        window
