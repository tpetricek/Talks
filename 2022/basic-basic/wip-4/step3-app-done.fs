module Basic.App
open Basic.Helpers

// STEP #3
// TODO: Echo using input/print & switch to 'async'
// DEMO: Paste Hello world example 
// TODO: Use 'print' and switch to 'async' + Sleep(100)

// STEP #4 
// TODO: Refactoring (Domain.fs and Evaluator.fs)
// DEMO: Insert guessing game sample
// TODO: Update the domain model to match
// TODO: Fix 'toString' & add variables
// DEMO: Add 'getNumber' function
// TODO: Handle Function "RND" and Binary '+'
// DEMO: Add remaining evalExpression cases
// TODO: Implement Assign, Input, If, List & fix App error!

// STEP #5
// DEMO: Add 'Parser.fs' and insert all the code
// TODO: Adapt main to use parseInput / tokenizeString
// DEMO: Insert recursive 'loop' & run final versioN!

// DOMAIN MODEL

type Value = 
  | String of string

type Expression = 
  | Literal of Value

type Command = 
  | Print of Expression
  | Goto of int
  | Run

// EVALUATOR

type State = 
  { Program : Map<int, Command> }
  
let evalExpression = function
  | Literal(v) -> v

let toString = function
  | String s -> s

let rec runCommand state (ln, cmd) = async {
  // Get program commands as sorted list
  let statements () = 
    [ for (KeyValue(k,v)) in state.Program -> Some k, v ] 
    |> Seq.sortBy fst 
  
  // Find the next line (if there is one) and run it
  let next state = async {
    match ln with 
    | None -> return state
    | Some ln -> 
        let next = statements() |> Seq.tryFind (fun (l, _) -> l > Some ln)
        match next with 
        | None -> return state
        | Some next -> return! runCommand state next }

  do! Async.Sleep 100
  match cmd with
  | Run ->
      return! runCommand state (Seq.head (statements ()))
  | Goto ln -> 
      return! runCommand state (Some ln, state.Program.[ln])
  | Print e -> 
      print (toString (evalExpression e))
      return! next state }

let runInput state (ln, cmd) = async {
  match ln with 
  | Some ln -> return { state with Program = state.Program.Add(ln, cmd) }
  | None -> return! runCommand state (None, cmd) }

// SAMPLE 

let hello = 
  [ Some 10, Print(Literal(String "HELLO WORLD")) 
    Some 20, Goto 10 
    None, Run ]    

async {
  let mutable state = { Program = Map.empty }
  for cmd in hello do
    let! newState = runInput state cmd 
    state <- newState }
|> Async.StartImmediate  