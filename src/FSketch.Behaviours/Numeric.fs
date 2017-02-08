namespace FSketch.Behaviours

type Numeric = Behaviour

module NumericOps =
    let Zero = forever 0.
    let One = forever 1.

    let inline negate (b:Numeric) = b >>> (fun x -> -x)
    let inline cos (b:Numeric) = b >>> (fun x -> cos x)
    let inline sin (b:Numeric) = b >>> (fun x -> sin x)
    let inline add (x:Numeric, y:Numeric) = lift (+) x y
    let inline substract (x:Numeric, y:Numeric) = lift (-) x y
    let inline multiply (x:Numeric, y:Numeric) = lift (*) x y
    let inline divide (x:Numeric, y:Numeric) = lift (/) x y
    let inline ofFloat (f:float) : Numeric = forever f
