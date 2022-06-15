module Basic.Evaluator
open Basic.Domain
open Basic.Helpers

type State = 
  { Program : Map<int, Command>
    Variables : Map<string, Value> }
  
let rnd = System.Random()

let getNumber = function
  | Number n -> n
  | _ -> failwith "Not a number"

let rec evalExpression state = function
  | Literal(v) -> v
  | Variable(v) -> state.Variables.[v]
  | Function("RND", _) -> Number(decimal(rnd.NextDouble()))
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
  | Binary('+', l, r) -> 
      Number(getNumber (evalExpression state l) + 
        getNumber (evalExpression state r))
  | Binary _ -> failwith "Unsupported operator"
  | Function _ -> failwith "Unsupported function"

let toString = function
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

  do! Async.Sleep 100
  match cmd with
  | Input(v) ->
      let! inp = input |> awaitObservable
      let n = Number(decimal inp)
      return! next { state with Variables = state.Variables.Add(v, n) }
  | Assign(v, e) ->
      return! next { state with Variables = state.Variables.Add(v, evalExpression state e) }
  | List ->
      for ln, cmd in statements() do
        print (sprintf "%d %A" ln.Value cmd)
      return! next state
  | Run ->
      return! runCommand state (Seq.head (statements ()))
  | If(cond, ln) -> 
      if evalExpression state cond = Bool true then
        return! runCommand state (Some ln, state.Program.[ln])
      else
        return! next state
  | Goto ln -> 
      return! runCommand state (Some ln, state.Program.[ln])
  | Print e -> 
      print (toString (evalExpression state e))
      return! next state }

let runInput state (ln, cmd) = async {
  match ln with 
  | Some ln -> return { state with Program = state.Program.Add(ln, cmd) }
  | None -> return! runCommand state (None, cmd) }
