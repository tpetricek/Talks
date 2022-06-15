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

let evalExpression e = 
  match e with
  | Literal v -> v

let toString v = 
  match v with 
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
      let first = statements () |> Seq.head
      runCommand state first
  | Goto ln ->
      runCommand state (Some ln, state.Program.[ln])
  | Print e -> 
      e |> evalExpression |> toString |> printfn "%s"
      next state

let acceptInput state (ln, cmd) = 
  match ln with
  | None -> runCommand state (ln, cmd)
  | Some ln -> { state with Program = state.Program.Add(ln, cmd) }

let hello = 
  [ Some 10, Print(Literal(String "HELLO WORLD")) 
    Some 20, Goto 10 
    None, Run ]

let mutable state = { Program = Map.empty }
for cmd in hello do 
  state <- acceptInput state cmd

// STEP #2
// TODO: Add Goto, Run and update 'hello' sample  
// TODO: Add 'acceptInput' function & return new state
// TODO: Initial state & mutable main loop
// DEMO: Add helpers to 'runCommand' & make it recursive 
// TODO: Add Run and Goto implementations & call 'next' in Print