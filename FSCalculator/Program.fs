namespace FSCalculator 

type Operation = 
    | Add
    | Subtract
    | Multiply
    | Divide
    | Negate
    | Result

type Expression = 
    private
    | Operation of Operation
    | Number of int
    | Delete
    | Clear

type State = { History: string; Result: float; Operation: Operation }

module Calculator =

    [<EntryPoint>]
    let main argv = 
        let d = Delete
        0 // return an integer exit code
