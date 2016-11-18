namespace FSCalculator 

open InputState
open CalculatorState

module Calculator =

    type CalculatorInput =
        | NumberInput of NumberInput
        | EvaluateInput of EvaluateInput
        | Clear

