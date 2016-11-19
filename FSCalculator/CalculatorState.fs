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
        let EvaluatePendingOperation (operation: Operation) (number: float) (result: float) =
            match operation with
            | Add -> result + number
            | Subtract -> result - number
            | Multiply -> result * number
            | Divide -> result / number

        match evaluateInput with
        | Operation operation ->
            let newState =
                 { Result = 
                        if not (calculatorState.Pending = None) then 
                            EvaluatePendingOperation (fst calculatorState.Pending.Value) number calculatorState.Result
                        else
                            number
                   History = calculatorState.History + number.ToString()
                   Pending = Some (operation, number) }

            match operation with
            | Add -> 
                { newState with History = newState.History + " + " }
            | Subtract -> 
                 { newState with History = newState.History + " - " }
            | Multiply -> 
                 { newState with History = newState.History + " * " }
            | Divide -> 
                 { newState with History = newState.History + " / " }