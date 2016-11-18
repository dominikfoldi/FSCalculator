namespace FSCalculator 

module Models =

    type Operation = 
        | Add
        | Subtract
        | Multiply
        | Divide

    type Input = 
        | Operation of Operation
        | Number of int
        | DecimalPoint of char
        | Negate
        | Result
        | Delete
        | Clear

    type InputState = 
        { Input: string; DecimalPoint: bool }

        static member Empty = 
            { Input = "0"; DecimalPoint = false }

    let ModifyInputState (input: Input) (inputState: InputState) = 
        match input with
            | Number n -> 
                if inputState.Input = "0" then 
                    { inputState with Input = n.ToString() }
                else
                    { inputState with Input = inputState.Input + n.ToString() }

    type ProcessedState = { History: string; Result: float; PendingOperation: Operation; PendingNumber: float }