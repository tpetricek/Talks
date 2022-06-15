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
  | Literal v -> v

let runProgram (ln, cmd) state = 
  match cmd with 
  | Print(e, true) -> printfn $"{toString(evalExpr state e)}"
  | Print(e, false) -> printf $"{toString(evalExpr state e)}"
  state

let runInput (ln, cmd) state = 
  match ln with 
  | Some ln -> { state with Program = state.Program.Add(ln, cmd) }
  | None -> runProgram (None, cmd) state
      
let runInputs inputs = 
  let empty = { Program = Map.empty; Variables = Map.empty }
  inputs |> Seq.fold (fun state input -> runInput input state) empty
    
let hello1 = 
  [ None, Print(Literal(String "HELLO WORLD"), true) ]

runInputs hello1