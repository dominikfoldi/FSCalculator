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
    let ``Decimal point should be added after a number``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 1) inputState |> ModifyInputState DecimalPoint

        Assert.Equal("1.", result.Input)

    [<Fact>]
    let ``Number should added after decimal point``() =
        let inputState = InputState.Empty

        let result = ModifyInputState DecimalPoint inputState |> ModifyInputState (Number 1)   

        Assert.Equal("0.1", result.Input)

    [<Fact>]
    let ``Only one decimal point should be added``() =
        let inputState = InputState.Empty

        Assert.Throws<MoreThanOneDecimalPointException>(fun () -> 
            ModifyInputState DecimalPoint inputState |> ModifyInputState DecimalPoint |> ignore )

    [<Fact>]
    let ``Maximum 16 number should be added``() =
        let inputState = InputState.Empty

        Assert.Throws<MoreThan16NumberException>(fun () -> 
            Seq.replicate 17 (Number 4) |> Seq.fold (fun s i -> ModifyInputState i s) inputState
            |> ignore)

    [<Fact>]
    let ``Negation should put a minus sign to the front``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState Negate 

        Assert.Equal("-2", result.Input)

    [<Fact>]
    let ``Double negation should clear the minus sign from the front``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState Negate |> ModifyInputState Negate

        Assert.Equal("2", result.Input)