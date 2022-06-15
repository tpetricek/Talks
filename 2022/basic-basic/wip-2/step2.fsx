// DOMAIN MODEL
// TODO: Input with line, Goto & Run commands

type Value = 
  | String of string
   
type Expression = 
  | Literal of Value

type Command = 
  | Print of Expression 
  | Goto of int
  | Run 

type Input = int option * Command

// EVALUATION
// TODO: Add State with Program, add runInput
// TODO: statements() & next state

type State =
  { Program : Map<int, Command> }

let rec evalExpr state = function
  | Literal v -> v

let toString = function
  | String s -> s

let rec runCommand state (ln, cmd) = 
  let statements () = 
    [ for (KeyValue(k,v)) in state.Program -> Some k, v ] 
    |> Seq.sortBy fst 
  let next state = 
    match ln with 
    | None -> state
    | Some ln -> 
        let next = statements() |> Seq.tryFind (fun (l, _) -> l > Some ln)
        match next with 
        | None -> state
        | Some next -> runCommand state next
  match cmd with 
  | Print(e) -> 
      printfn $"{toString(evalExpr state e)}"
      next state
  | Goto l -> runCommand state (Some l, state.Program.[l])
  | Run -> runCommand state (Seq.head (statements ()))

let runInput state (ln, cmd) = 
  match ln with 
  | None -> runCommand state (ln, cmd) 
  | Some ln -> { state with Program = state.Program.Add(ln, cmd) }

// DEMO
// TODO: hello world, run commands
let hello = 
  [ Some 10, Print(Literal(String "HELLO WORLD"))
    Some 20, Goto 10
    None, Run ]

let state = { Program = Map.empty }
hello |> Seq.fold runInput state 
