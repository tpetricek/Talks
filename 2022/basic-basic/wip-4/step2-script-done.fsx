// STEP #1
// TODO: Value is string, Expression is literal, Command is print
// TODO: Hello world
// TODO: evalExpression, toString and runCommand, main loop

type Value = 
  | String of string

type Expression = 
  | Literal of Value

type Command = 
  | Print of Expression
  | Goto of int
  | Run

type State = 
  { Program : Map<int, Command> }
  
let evalExpression = function
  | Literal(v) -> v

let toString = function
  | String s -> s

let rec runCommand state (ln, cmd) = 
  // Get program commands as sorted list
  let statements () = 
    [ for (KeyValue(k,v)) in state.Program -> Some k, v ] 
    |> Seq.sortBy fst 
  
  // Find the next line (if there is one) and run it
  let next state = 
    match ln with 
    | None -> state
    | Some ln -> 
        let next = statements() |> Seq.tryFind (fun (l, _) -> l > Some ln)
        match next with 
        | None -> state
        | Some next -> runCommand state next

  match cmd with
  | Run ->
      runCommand state (Seq.head (statements ()))
  | Goto ln -> 
      runCommand state (Some ln, state.Program.[ln])
  | Print e -> 
      printfn "%s" (toString (evalExpression e))
      next state

let runInput state (ln, cmd) = 
  match ln with 
  | Some ln -> { state with Program = state.Program.Add(ln, cmd) }
  | None -> runCommand state (None, cmd)

let hello = 
  [ Some 10, Print(Literal(String "HELLO WORLD")) 
    Some 20, Goto 10 
    None, Run ]    

let mutable state = { Program = Map.empty }
for cmd in hello do
  state <- runInput state cmd 

// STEP #2
// TODO: Add Goto, Run and update 'hello' sample  
// TODO: Add 'runInput' function & return new state
// TODO: Initial state & mutable main loop
// DEMO: Add helpers to 'runCommand' & make it recursive 
// TODO: Add Run and Goto implementations & call 'next' in Print