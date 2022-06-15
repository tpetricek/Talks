module Basic.Evaluator
open Basic.Helpers
open Basic.Domain

type State = 
  { Program : Map<int, Command>
    Variables : Map<string, Value> }

let getNumber = function
  | Number n -> n
  | _ -> failwith "Not a number"

let rnd = System.Random()

let rec evalExpression state e = 
  match e with
  | Variable n -> state.Variables.[n]
  | Literal v -> v
  | Binary('+', l, r) ->
      Number(getNumber (evalExpression state l) + 
        getNumber (evalExpression state r))
  | Function("RND", _) ->
      Number(decimal (rnd.NextDouble()))
  | Function("INT", e) ->
      Number(decimal (int (getNumber (evalExpression state e))))
  | Binary('*', l, r) -> 
      Number(getNumber (evalExpression state l) * 
        getNumber(evalExpression state r))
  | Binary('<', l, r) -> 
      Bool(getNumber (evalExpression state l) < 
        getNumber(evalExpression state r))
  | Binary('>', l, r) -> 
      Bool(getNumber (evalExpression state l) > 
        getNumber(evalExpression state r))
  | Binary('=', l, r) -> 
      Bool(getNumber (evalExpression state l) = 
        getNumber(evalExpression state r))
  | Binary _ -> failwith "Unsupported operator"
  | Function _ -> failwith "Unsupported function"
  
let toString v = 
  match v with 
  | String s -> s
  | Number n -> string n
  | Bool b -> string b


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
  do! Async.Sleep(100)
  match cmd with
  | Assign(v, e) ->
      let state = { state with Variables = state.Variables.Add(v, evalExpression state e) }
      return! next state
  | If(cond, ln) ->
      if evalExpression state cond = Bool true then
        return! runCommand state (Some ln, state.Program.[ln])
      else
        return! next state
  | Input v ->
      let! inp = input |> awaitObservable
      let n = Number(decimal inp)
      let state = { state with Variables = state.Variables.Add(v, n) }
      return! next state
  | List ->
      for ln, cmd in statements() do
        print (sprintf "%d %A" ln.Value cmd)
      return! next state
  | Run -> 
      let first = statements () |> Seq.head
      return! runCommand state first
  | Goto ln ->
      return! runCommand state (Some ln, state.Program.[ln])
  | Print e -> 
      e |> evalExpression state |> toString |> print
      return! next state }

let acceptInput state (ln, cmd) = async {
  match ln with
  | None -> return! runCommand state (ln, cmd)
  | Some ln -> return { state with Program = state.Program.Add(ln, cmd) } }
