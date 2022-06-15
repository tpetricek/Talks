module Basic.Evaluator
open Basic.Domain


let toString = function 
  | Number n -> string n
  | Bool b -> string b
  | String s -> s

type State = 
  { Variables : Map<string, Value> 
    Program : Map<int, Command>
    Print : string -> unit 
    Input : System.IObservable<string> }

// ADDED
let getNumber = function
  | Number n -> n 
  | _ -> failwith "Not a number"

let rnd = System.Random()

let rec evalExpr state = function
  | Variable v -> state.Variables.[v]
  | Literal v -> v
  // MORE
  | Function("RND", e) ->
      Number(decimal (rnd.NextDouble()))
  | Function("INT", e) ->
      Number(decimal (int (getNumber (evalExpr state e))))
  | Binary('+', l, r) -> 
      Number(getNumber (evalExpr state l) + getNumber(evalExpr state r))
  | Binary('*', l, r) -> 
      Number(getNumber (evalExpr state l) * getNumber(evalExpr state r))
  | Binary('<', l, r) -> 
      Bool(getNumber (evalExpr state l) < getNumber(evalExpr state r))
  | Binary('>', l, r) -> 
      Bool(getNumber (evalExpr state l) > getNumber(evalExpr state r))
  | Binary('=', l, r) -> 
      Bool(getNumber (evalExpr state l) = getNumber(evalExpr state r))

let rec runProgram (ln, cmd) state = async {
  let sortedProg () = 
    state.Program |> Map.toSeq |> Seq.sortBy fst 
  let next state = async {
    match ln with 
    | None -> return state
    | Some ln -> 
        let nextcmd = sortedProg () |> Seq.tryFind (fun (l, cmd) -> l > ln)
        do! Async.Sleep 100
        match nextcmd with 
        | Some(ln, cmd) -> return! runProgram (Some ln, cmd) state
        | _ -> return state }

  match cmd with 
  | Goto(ln) -> return! runProgram (Some ln, state.Program.[ln]) state
  | Print(e) -> 
      state.Print $"{toString(evalExpr state e)}"
      return! next state
  | Run ->
      let ln, cmd = sortedProg() |> Seq.head
      return! runProgram (Some ln, cmd) state 
  // add more commands
  | If(cond, ln) ->
      if evalExpr state cond = Bool true then
        return! runProgram (None, Goto ln) state
      else 
        return! next state
  | Input(v) ->
      let! input = state.Input |> Helpers.awaitObservable
      state.Print input
      let e = Literal(Number (decimal input))
      return! next { state with Variables = state.Variables.Add(v, evalExpr state e) }
  | Assign(v, e) ->
      return! next { state with Variables = state.Variables.Add(v, evalExpr state e) } }

let runInput (ln, cmd) state = async {
  match ln with 
  | Some ln -> 
      return { state with Program = state.Program.Add(ln, cmd) }
  | None -> 
      return! runProgram (None, cmd) state }
      
let runInputs state inputs = async {
  let mutable state = state 
  for input in inputs do
    let! nstate = runInput input state
    state <- nstate 
  return state }