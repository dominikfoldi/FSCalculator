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
    let ``Number bigger than 9 should not be added``() =
        let inputState = InputState.Empty

        Assert.Throws<NumberOutOfBoundsException>(fun () -> 
            ModifyInputState (Number 20) inputState |> ignore )

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

        let result = ModifyInputState DecimalPoint inputState |> ModifyInputState DecimalPoint

        Assert.Equal("0.", result.Input)

    [<Fact>]
    let ``Maximum 16 number should be added``() =
        let inputState = InputState.Empty

        let result = Seq.replicate 17 (Number 4) |> Seq.fold (fun s i -> ModifyInputState i s) inputState

        Assert.Equal("4444444444444444", result.Input)

    [<Fact>]
    let ``Maximum 16 number should be added wit a decimal point``() =
        let inputState = InputState.Empty

        let result = Seq.replicate 15 (Number 4) |> Seq.fold (fun s i -> ModifyInputState i s) inputState |>
                        ModifyInputState DecimalPoint |> ModifyInputState (Number 1)     

        Assert.Equal("444444444444444.1", result.Input)

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

    [<Fact>]
    let ``Delete should delete one char from the end``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState (Number 2) |> ModifyInputState Delete

        Assert.Equal("2", result.Input)

    [<Fact>]
    let ``Delete should decrease NumberCount by one``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState (Number 2) |> ModifyInputState Delete

        Assert.Equal(1, result.NumberCount)

    [<Fact>]
    let ``Deleted decimal point should not decrease NumberCount``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState DecimalPoint |> ModifyInputState Delete

        Assert.Equal(1, result.NumberCount)

    [<Fact>]
    let ``Delete should not delete a null input``() =
        let inputState = InputState.Empty

        let result = ModifyInputState Delete inputState

        Assert.Equal("0", result.Input)

    [<Fact>]
    let ``Delete should change one number long input to 0``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState Delete

        Assert.Equal("0", result.Input)

    [<Fact>]
    let ``Delete should change Negated flag to false if it deletes one number long input``() =
        let inputState = InputState.Empty

        let result = ModifyInputState (Number 2) inputState |> ModifyInputState Negate |> ModifyInputState Delete

        Assert.False(result.Negated)