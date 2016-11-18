namespace FSCalculator.Tests

open Xunit
open FSCalculator.CalculatorState

module CalculatorStateTest =
    
    [<Fact>]
    let ``Empty CalculatorState should return 0``() =
        let result = CalculatorState.Empty
        Assert.Equal(0.0, result.Result)

    [<Fact>]
    let ``Adding something to an empty state should insert it to the result``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Add) 5.0 state

        Assert.Equal(5.0, result.Result)

    [<Fact>]
    let ``Adding something to an empty state should insert it to the history``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Add) 5.0 state

        Assert.Equal("5 +", result.History)