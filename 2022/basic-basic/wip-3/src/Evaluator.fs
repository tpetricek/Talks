module Basic.Evaluator
open Basic.Domain
open Basic.Helpers

type State =
  { Program : Map<int, Command> 
    Variables : Map<string, Value> }

// TODO: RND and Binary
// DEMO: All the rest

let rnd = System.Random()

let getNumber = function
  | Number n -> n
  | _ -> failwith "Not a number"

let rec evalExpr state = function
  | Variable s -> state.Variables.[s]
  | Literal v -> v
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

let toString = function
  | String s -> s
  | Number n -> string n
  | Bool b -> string b

let rec runCommand state (ln, cmd) = async {
  do! Async.Sleep 100
  let statements () = 
    [ for (KeyValue(k,v)) in state.Program -> Some k, v ] 
    |> Seq.sortBy fst 
  let next state = async {
    match ln with 
    | None -> return state
    | Some ln -> 
        let next = statements() |> Seq.tryFind (fun (l, _) -> l > Some ln)
        match next with 
        | None -> return state
        | Some next -> return! runCommand state next }
  match cmd with 
  | List ->
      for ln, cmd in statements () do
        print (sprintf "%A %A" ln cmd)
      return! next state
  | Input var ->
      let! inp = input |> awaitObservable
      print inp
      let n = decimal inp
      let state = { state with Variables = state.Variables.Add(var, Number n) }
      return! next state
  | Assign(v, e) ->
      let state = { state with Variables = state.Variables.Add(v, evalExpr state e) }
      return! next state
  | If(cond, ln) ->
      if evalExpr state cond = Bool true then 
        return! runCommand state (Some ln, state.Program.[ln])
      else 
        return! next state
  | Print(e) -> 
      print (toString(evalExpr state e))
      return! next state
  | Goto l -> return! runCommand state (Some l, state.Program.[l])
  | Run -> return! runCommand state (Seq.head (statements ())) }

let runInput state (ln, cmd) = async {
  match ln with 
  | None -> return! runCommand state (ln, cmd) 
  | Some ln -> return { state with Program = state.Program.Add(ln, cmd) } }
