type Value = 
  | Number of decimal
  | String of string
  | Bool of bool
   
type Expression = 
  | Variable of string
  | Literal of Value
  | Binary of char * Expression * Expression
  | Function of string * Expression

type Command = 
  | Print of Expression * bool
  | Goto of int
  | Input of string
  | Assign of string * Expression
  | If of Expression * int
  | Run 

let toString = function 
  | Number n -> string n
  | Bool b -> string b
  | String s -> s

type State = 
  { Variables : Map<string, Value> 
    Program : Map<int, Command> }

let rec evalExpr state = function
  | Variable v -> state.Variables.[v]
  | Literal v -> v

let rec runProgram (ln, cmd) state = 
  let sortedProg () = 
    state.Program |> Map.toSeq |> Seq.sortBy fst 
  let next () = 
    match ln with 
    | None -> state
    | Some ln -> 
        let nextcmd = sortedProg () |> Seq.tryFind (fun (l, cmd) -> l > ln)
        match nextcmd with 
        | Some(ln, cmd) -> runProgram (Some ln, cmd) state
        | _ -> state

  match cmd with 
  | Goto(ln) -> runProgram (Some ln, state.Program.[ln]) state
  | Print(e, true) -> 
      printfn $"{toString(evalExpr state e)}"
      next ()
  | Print(e, false) -> 
      printf $"{toString(evalExpr state e)}"
      next ()
  | Run ->
      let ln, cmd = sortedProg() |> Seq.head
      runProgram (Some ln, cmd) state

let runInput (ln, cmd) state = 
  match ln with 
  | Some ln -> 
      { state with Program = state.Program.Add(ln, cmd) }
  | None -> 
      runProgram (None, cmd) state
      
let runInputs inputs = 
  let empty = { Program = Map.empty; Variables = Map.empty }
  inputs |> Seq.fold (fun state input -> runInput input state) empty

let hello1 = 
  [ None, Print(Literal(String "HELLO WORLD"), true) ]

runInputs hello1

let hello2 = 
  [ Some 10, Print(Literal(String "HELLO WORLD"), true) 
    Some 20, Goto 10 
    None, Run ]

runInputs hello2