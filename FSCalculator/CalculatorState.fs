namespace FSCalculator 

module CalculatorState =

    type Operation = 
        | Add
        | Subtract
        | Multiply
        | Divide

    type EvaluateInput =
        | Operation of Operation
        | Evaluate

    type CalculatorState = 
        { History: string; Result: float; Pending: (Operation * float) option}    

        static member Empty =
            { History = ""; Result = 0.0; Pending = None }

    let ModifyCalculatorState (evaluateInput: EvaluateInput) (number: float) (calculatorState: CalculatorState) = 
        let newState = 
            if calculatorState.History = "" then
                { calculatorState with Result = number }
            else
                calculatorState
        match evaluateInput with
        | Operation operation ->
            match operation with
            | Add -> 
                { newState with 
                    Pending = Some (Add, number)
                    History = number.ToString() + " + " }