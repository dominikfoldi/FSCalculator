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
            if evaluated then
                inputState <- InputState.Empty
                evaluated <- false
            inputState <- ModifyInputState numberInput inputState
        | EvaluateInput evaluateInput ->
            calculatorState <- ModifyCalculatorState evaluateInput (float inputState.Input) calculatorState
            inputState <- { inputState with Input = (string calculatorState.Result) }
            evaluated <- true
        | Clear ->
            inputState <- InputState.Empty
            calculatorState <- CalculatorState.Empty
        

