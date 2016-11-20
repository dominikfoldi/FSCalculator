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

    let GetInput (input: CalculatorInput) =
        match input with
        | NumberInput numberInput ->
            inputState <- ModifyInputState numberInput inputState
        | EvaluateInput evaluateInput ->
            calculatorState <- ModifyCalculatorState evaluateInput (float inputState.Input) calculatorState
            inputState <- { inputState with Input = (string calculatorState.Result) }
        | Clear ->
            inputState <- InputState.Empty
            calculatorState <- CalculatorState.Empty
        

