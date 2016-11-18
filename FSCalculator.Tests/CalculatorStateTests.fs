namespace FSCalculator.Tests

open Xunit
open FSCalculator.Models

module CalculatorStateTest =
    
    [<Fact>]
    let ``Empty CalculatorState should return 0``() =
        let result = CalculatorState.Empty
        Assert.Equal(0.0, result.Result)