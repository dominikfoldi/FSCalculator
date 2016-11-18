namespace FSCalculator 

type Operation = 
    | Add
    | Subtract
    | Multiply
    | Divide

type Input = 
    | Operation of Operation
    | Number of int
    | Negate
    | Result
    | Delete
    | Clear

type InputState = { Input: string; DecimalPoint: bool }

type ProcessedState = { History: string; Result: float; PendingOperation: Operation; PendingNumber: float }

module Calculator =

    [<EntryPoint>]
    let main argv = 
        0 // return an integer exit code
