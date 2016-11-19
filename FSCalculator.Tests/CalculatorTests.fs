﻿namespace FSCalculator.Tests

open Xunit
open FSCalculator.Calculator
open FSCalculator.InputState
open FSCalculator.CalculatorState

module CalculatorTest =
    
    [<Fact>]
    let ``GetInput should handle NumberInput``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (NumberInput (Number 2)) |> ignore
        
        Assert.Equal("2", inputState.Input)  
            
    [<Fact>]
    let ``GetInput should handle EvaluateInput``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (EvaluateInput (Operation Add)) |> ignore
        
        Assert.Equal("0 + ", calculatorState.History)  

    [<Fact>]
    let ``Getting an EvaluateInput should parse the inputState``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (NumberInput (Number 2)) |> ignore
        GetInput (EvaluateInput (Operation Add)) |> ignore
        
        Assert.Equal("2 + ", calculatorState.History)  

    [<Fact>]
    let ``Clear should reset the input and calculator states``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (NumberInput (Number 2)) |> ignore
        GetInput (EvaluateInput (Operation Add)) |> ignore
        GetInput Clear|> ignore

        Assert.Equal("", calculatorState.History)          
        Assert.Equal("0", inputState.Input)  