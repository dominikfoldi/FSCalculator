namespace FSCalculator.Tests

open Xunit
open FSCalculator.Models

module CalculatorTest =

    [<Fact>]
    let ``Empty InputState should return 0``() =
        let result = InputState.Empty
        Assert.Equal("0", result.Input)

    [<Fact>]
    let ``Empty InputState modified by one number should return that number``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 0) inputState

        Assert.Equal("0", result.Input)

    [<Fact>]
    let ``Not empty InputState modified by one number should concat that number``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 1) inputState |> ModifyInputState (Number 2)

        Assert.Equal("12", result.Input)

    [<Fact>]
    let ``Not empty InputState modified by one number should concat that number``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 1) inputState |> ModifyInputState (Number 2)

        Assert.Equal("12", result.Input)
