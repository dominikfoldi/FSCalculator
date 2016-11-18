namespace FSCalculator 

module Models =

    type Operation = 
        | Add
        | Subtract
        | Multiply
        | Divide

    type NumberInput = 
        | Number of int
        | DecimalPoint
        | Negate
        | Delete

    type EvaluateInput =
        | Operation of Operation
        | Evaluate

    type CalculatorInput =
        | NumberInput of NumberInput
        | EvaluateInput of EvaluateInput
        | Clear

    exception NumberOutOfBoundsException of string

    type InputState = 
        { Input: string; DecimalPoint: bool; NumberCount: int; Negated: bool }

        static member Empty = 
            { Input = "0"; DecimalPoint = false; NumberCount = 1; Negated = false }

    type CalculatorState = 
        { History: string; Result: float; Pending: (Operation * float) option}    

        static member Empty =
            { History = ""; Result = 0.0; Pending = None }

    let ModifyInputState (input: NumberInput) (inputState: InputState) = 
        match input with
            | Number n -> 
                if n > 9 || n < 0 then
                    raise (NumberOutOfBoundsException "Cannot process numbers not between 0 and 9")
                elif inputState.Input = "0" then 
                    { inputState with Input = n.ToString() }
                else
                    if inputState.NumberCount < 16 then
                        { inputState with 
                            Input = inputState.Input + n.ToString()
                            NumberCount = inputState.NumberCount + 1 }
                    else
                        inputState
            | DecimalPoint -> 
                if not inputState.DecimalPoint then
                    { inputState with 
                        Input = inputState.Input + "."
                        DecimalPoint = true }
                else
                    inputState
            | Negate ->
                if not inputState.Negated then
                    { inputState with 
                        Input = "-" + inputState.Input
                        Negated = true }
                else
                    { inputState with 
                        Input = inputState.Input.Substring 1
                        Negated = false }
            | Delete ->
                if inputState.NumberCount = 1 && not inputState.DecimalPoint then
                    InputState.Empty
                else 
                    { inputState with 
                        NumberCount = 
                            if inputState.Input.EndsWith "." then 
                                inputState.NumberCount 
                            else
                                inputState.NumberCount - 1
                        Input = inputState.Input.Substring( 0, inputState.Input.Length - 1)}