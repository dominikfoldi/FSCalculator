namespace FSCalculator.Tests

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

    [<Fact>]
    let ``Calling evaluate one after another should pass the last inputState as the new number``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (NumberInput (Number 2)) |> ignore
        GetInput (EvaluateInput (Operation Add)) |> ignore
        GetInput (EvaluateInput (Evaluate)) |> ignore
        GetInput (EvaluateInput (Evaluate)) |> ignore

        Assert.Equal("6", inputState.Input)     
        
    [<Fact>]
    let ``NumberInput after EvaluateInput should reset inputstate``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (NumberInput (Number 2)) |> ignore
        GetInput (EvaluateInput (Operation Add)) |> ignore
        GetInput (NumberInput (Number 2)) |> ignore

        Assert.Equal("2", inputState.Input)     
        
    [<Fact>]
    let ``InputState should be set properly after evaluate``() =
        inputState <- InputState.Empty
        calculatorState <- CalculatorState.Empty

        GetInput (NumberInput (Number 9)) |> ignore
        GetInput (NumberInput DecimalPoint) |> ignore
        GetInput (NumberInput (Number 5)) |> ignore
        GetInput (NumberInput Negate) |> ignore
        GetInput (EvaluateInput (Operation Add)) |> ignore
        GetInput (NumberInput (Number 2)) |> ignore
        GetInput (EvaluateInput Evaluate) |> ignore

        Assert.Equal("-7.5", inputState.Input)         
        Assert.Equal(true, inputState.Negated)         
        Assert.Equal(2, inputState.NumberCount)         
        Assert.Equal(true, inputState.DecimalPoint)         
