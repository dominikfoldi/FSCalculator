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
        let ApplyPendingOperation (pending: (Operation * float) option) (number: float) (result: float) =
            if not (pending = None) then 
                match (fst pending.Value) with
                | Add -> result + number
                | Subtract -> result - number
                | Multiply -> result * number
                | Divide -> result / number
             else
                number

        match evaluateInput with
        | Operation operation ->
            let newState =
                 { Result = ApplyPendingOperation calculatorState.Pending number calculatorState.Result
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
         | Evaluate ->
            if not (calculatorState.History = "") then
                { calculatorState with 
                    Result = ApplyPendingOperation calculatorState.Pending number calculatorState.Result
                    Pending = Some ((fst calculatorState.Pending.Value), number)
                    History = "" }
            elif not <| obj.ReferenceEquals (calculatorState.Pending, null) then
                 { calculatorState with 
                    Result = ApplyPendingOperation calculatorState.Pending number (snd calculatorState.Pending.Value) }
            else
                calculatorState