namespace FSCalculator.Tests

open Xunit
open FSCalculator.CalculatorState

module CalculatorStateTest =
    
    [<Fact>]
    let ``Empty CalculatorState should return 0``() =
        let result = CalculatorState.Empty
        Assert.Equal(0.0, result.Result)

    [<Fact>]
    let ``Modifying an empty state should insert the number to the result``() =
        let stateAdd = CalculatorState.Empty
        let stateSub = CalculatorState.Empty
        let stateMul = CalculatorState.Empty
        let stateDiv = CalculatorState.Empty

        let resultAdd = ModifyCalculatorState (Operation Add) 5.0 stateAdd
        let resultSub = ModifyCalculatorState (Operation Subtract) 5.0 stateSub
        let resultMul = ModifyCalculatorState (Operation Multiply) 5.0 stateMul
        let resultDiv = ModifyCalculatorState (Operation Divide) 5.0 stateDiv

        Assert.Equal(5.0, resultAdd.Result)
        Assert.Equal(5.0, resultSub.Result)
        Assert.Equal(5.0, resultMul.Result)
        Assert.Equal(5.0, resultDiv.Result)

    [<Fact>]
    let ``Modifying an empty state should insert the number and operation to the history``() =
        let stateAdd = CalculatorState.Empty
        let stateSub = CalculatorState.Empty
        let stateMul = CalculatorState.Empty
        let stateDiv = CalculatorState.Empty

        let resultAdd = ModifyCalculatorState (Operation Add) 5.0 stateAdd
        let resultSub = ModifyCalculatorState (Operation Subtract) 5.0 stateSub
        let resultMul = ModifyCalculatorState (Operation Multiply) 5.0 stateMul
        let resultDiv = ModifyCalculatorState (Operation Divide) 5.0 stateDiv

        Assert.Equal("5 + ", resultAdd.History)
        Assert.Equal("5 - ", resultSub.History)
        Assert.Equal("5 * ", resultMul.History)
        Assert.Equal("5 / ", resultDiv.History)

    [<Fact>]
    let ``Modifying a state should insert the operation and number to the Pending property``() =
        let stateAdd = CalculatorState.Empty
        let stateSub = CalculatorState.Empty
        let stateMul = CalculatorState.Empty
        let stateDiv = CalculatorState.Empty

        let resultAdd = ModifyCalculatorState (Operation Add) 5.0 stateAdd
        let resultSub = ModifyCalculatorState (Operation Subtract) 5.0 stateSub
        let resultMul = ModifyCalculatorState (Operation Multiply) 5.0 stateMul
        let resultDiv = ModifyCalculatorState (Operation Divide) 5.0 stateDiv

        Assert.Equal(Some (Add, 5.0), resultAdd.Pending)
        Assert.Equal(Some (Subtract, 5.0), resultSub.Pending)
        Assert.Equal(Some (Multiply, 5.0), resultMul.Pending)
        Assert.Equal(Some (Divide, 5.0), resultDiv.Pending)

    [<Fact>]
    let ``Modyfying a not empty state should added to the history``() =
        let stateAdd = CalculatorState.Empty
        let stateSub = CalculatorState.Empty
        let stateMul = CalculatorState.Empty
        let stateDiv = CalculatorState.Empty

        let resultAdd = ModifyCalculatorState (Operation Add) 5.0 stateAdd |> ModifyCalculatorState (Operation Add) 4.0
        let resultSub = ModifyCalculatorState (Operation Subtract) 5.0 stateSub |> ModifyCalculatorState (Operation Add) 4.0
        let resultMul = ModifyCalculatorState (Operation Multiply) 5.0 stateMul |> ModifyCalculatorState (Operation Add) 4.0
        let resultDiv = ModifyCalculatorState (Operation Divide) 5.0 stateDiv |> ModifyCalculatorState (Operation Add) 4.0

        Assert.Equal("5 + 4 + ", resultAdd.History)        
        Assert.Equal("5 - 4 + ", resultSub.History)
        Assert.Equal("5 * 4 + ", resultMul.History)
        Assert.Equal("5 / 4 + ", resultDiv.History)

    [<Fact>]
    let ``Pending operation Add should add the next number to the result``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Add) 5.0 state |> ModifyCalculatorState (Operation Add) 4.0

        Assert.Equal(9.0, result.Result)

    [<Fact>]
    let ``Pending operation Subtract should subtract the next number from the result``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Subtract) 5.0 state |> ModifyCalculatorState (Operation Add) 4.0

        Assert.Equal(1.0, result.Result)

    [<Fact>]
    let ``Pending operation Multiply should multiply the next number with the result``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Multiply) 5.0 state |> ModifyCalculatorState (Operation Add) 4.0

        Assert.Equal(20.0, result.Result)

    [<Fact>]
    let ``Pending operation Divide should divide the next number with the result``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Divide) 5.0 state |> ModifyCalculatorState (Operation Add) 4.0

        Assert.Equal(1.25, result.Result)

    [<Fact>]
    let ``Adding 5 to 15 and divide by 4 should return 5``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Add) 15.0 state |> ModifyCalculatorState (Operation Divide) 5.0 |>
                        ModifyCalculatorState (Operation Multiply) 4.0

        Assert.Equal(5.0, result.Result)

    [<Fact>]
    let ``Evaluate operation should apply the last operation on the last number``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Add) 15.0 state |> ModifyCalculatorState (Operation Divide) 5.0 |>
                        ModifyCalculatorState Evaluate 4.0

        Assert.Equal(5.0, result.Result)

    [<Fact>]
    let ``Evaluate operation should reset the history``() =
        let state = CalculatorState.Empty

        let result = ModifyCalculatorState (Operation Add) 15.0 state |> ModifyCalculatorState (Operation Divide) 5.0 |>
                        ModifyCalculatorState Evaluate 4.0

        Assert.Equal("", result.History)