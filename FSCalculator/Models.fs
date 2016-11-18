﻿namespace FSCalculator 

module Models =

    type Operation = 
        | Add
        | Subtract
        | Multiply
        | Divide

    type Input = 
        | Operation of Operation
        | Number of int
        | DecimalPoint
        | Negate
        | Evaluate
        | Delete
        | Clear

    type InputState = 
        { Input: string; DecimalPoint: bool; NumberCount: int; Negated: bool }

        static member Empty = 
            { Input = "0"; DecimalPoint = false; NumberCount = 1; Negated = false }

    exception MoreThanOneDecimalPointException
    exception MoreThan16NumberException

    let ModifyInputState (input: Input) (inputState: InputState) = 
        match input with
            | Number n -> 
                if inputState.Input = "0" then 
                    { inputState with Input = n.ToString() }
                else
                    if inputState.NumberCount < 16 then
                        { inputState with 
                            Input = inputState.Input + n.ToString()
                            NumberCount = inputState.NumberCount + 1 }
                    else
                        raise MoreThan16NumberException
            | DecimalPoint -> 
                if not inputState.DecimalPoint then
                    { inputState with 
                        Input = inputState.Input + "."
                        DecimalPoint = true }
                else
                    raise MoreThanOneDecimalPointException
            | Negate ->
                if not inputState.Negated then
                    { inputState with 
                        Input = "-" + inputState.Input
                        Negated = true }
                else
                    { inputState with 
                        Input = inputState.Input.Substring 1
                        Negated = false }
            | Delete ->
                if inputState.NumberCount = 1 && not inputState.DecimalPoint then
                    InputState.Empty
                else 
                    { inputState with 
                        NumberCount = 
                            if inputState.Input.EndsWith "." then 
                                inputState.NumberCount 
                            else
                                inputState.NumberCount - 1
                        Input = inputState.Input.Substring( 0, inputState.Input.Length - 1)}
            | Clear ->
                InputState.Empty

    type ProcessedState = { History: string; Result: float; PendingOperation: Operation; PendingNumber: float }