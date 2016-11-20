namespace FSCalculator 

open InputState
open CalculatorState

module Calculator =

    type CalculatorInput =
        | NumberInput of NumberInput
        | EvaluateInput of EvaluateInput
        | Clear

    let mutable inputState = InputState.Empty
    let mutable calculatorState = CalculatorState.Empty

    let mutable operation = false
    let mutable evaluated = false   

    let GetInput (input: CalculatorInput) =
        match input with
        | NumberInput numberInput ->
            if operation && (numberInput <> Negate && numberInput <> Delete && numberInput <> DecimalPoint) then
                inputState <- InputState.Empty
            if operation then
                operation <- false
            inputState <- ModifyInputState numberInput inputState
        | EvaluateInput evaluateInput ->
            if evaluated && 
                (evaluateInput = (Operation Add) || evaluateInput = (Operation Subtract) || 
                    evaluateInput = (Operation Multiply) || evaluateInput = (Operation Divide)) then
                calculatorState <- CalculatorState.Empty
                evaluated <- false
            calculatorState <- ModifyCalculatorState evaluateInput (float inputState.Input) calculatorState
            inputState <- 
                let resultString = (string calculatorState.Result) 
                { Input = (string calculatorState.Result) 
                  NumberCount = 
                    let mutable tempLength = String.length resultString
                    if calculatorState.Result < 0.0 then
                      tempLength <- tempLength - 1
                    if resultString.Contains(".") then
                      tempLength <- tempLength - 1
                    tempLength
                  Negated = (calculatorState.Result < 0.0) 
                  DecimalPoint = resultString.Contains(".")}
            operation <- true
            if evaluateInput = Evaluate then
                evaluated <- true
                calculatorState <- { calculatorState with 
                                        Result = 0.0
                                        History = "" }
        | Clear ->
            inputState <- InputState.Empty
            calculatorState <- CalculatorState.Empty
        

