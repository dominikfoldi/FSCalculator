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

    let mutable evaluated = false

    let GetInput (input: CalculatorInput) =
        match input with
        | NumberInput numberInput ->
            if evaluated && (numberInput <> Negate && numberInput <> Delete && numberInput <> DecimalPoint) then
                inputState <- InputState.Empty
            if evaluated then
                evaluated <- false
            inputState <- ModifyInputState numberInput inputState
        | EvaluateInput evaluateInput ->
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
            evaluated <- true
            if evaluateInput = Evaluate then
                calculatorState <- { calculatorState with Result = 0.0 }
        | Clear ->
            inputState <- InputState.Empty
            calculatorState <- CalculatorState.Empty
        

