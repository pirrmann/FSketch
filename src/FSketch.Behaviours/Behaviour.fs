namespace FSketch.Behaviours

type BehaviourContext = { Time : float }

type Behaviour = | Behaviour of (BehaviourContext -> float)

[<AutoOpen>]
module Behaviours =

    let inline (>>>) (Behaviour(f)) g = Behaviour(f >> g)
    let lift f (Behaviour(x)) (Behaviour(y)) = Behaviour(fun c -> f (x c) (y c))

    let forever x = Behaviour (fun _ -> x)
    let time = Behaviour (fun ctx -> ctx.Time)

    let ( >+> ) = lift (+)
    let ( >-> ) = lift (-)
    let ( >*> ) = lift (*)
    let ( >/> ) = lift (/)
