module App
open Browser.Dom

let inputEl = document.getElementById("in") :?> Browser.Types.HTMLInputElement
let inputEvt = Event<string>()
inputEl.onkeypress <- fun ke -> 
  if ke.code = "Enter" then 
    inputEvt.Trigger(inputEl.value)
    inputEl.value <- ""

let awaitObservable (obs:System.IObservable<'T>) = 
  Async.FromContinuations(fun (cont, _, _) ->
    let mutable sub : System.IDisposable option = None
    sub <- Some <| obs.Subscribe(fun v -> 
      sub.Value.Dispose()
      cont v ) )

let print s = 
  document.getElementById("out").innerText <- 
    document.getElementById("out").innerText + s

        
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

  | c -> 
      printfn "EXPR FAILED %A" c
      failwith "!" 

let rec runProgram (ln, cmd) state = async {
  printf "%A" (Map.toList state.Variables)
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
  | Print(e, true) -> 
      print $"{toString(evalExpr state e)}\n"
      return! next state
  | Print(e, false) -> 
      print $"{toString(evalExpr state e)}"
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
      let! input = inputEvt.Publish |> awaitObservable
      print (input + "\n")
      let e = Literal(Number (decimal input))
      return! next { state with Variables = state.Variables.Add(v, evalExpr state e) }
  | Assign(v, e) ->
      return! next { state with Variables = state.Variables.Add(v, evalExpr state e) }
  | c -> 
      printfn "COMMAND FAILED %A" c
      return failwith "!" }

let runInput (ln, cmd) state = async {
  match ln with 
  | Some ln -> 
      return { state with Program = state.Program.Add(ln, cmd) }
  | None -> 
      return! runProgram (None, cmd) state }
      
let runInputs inputs = async {
  let mutable state = { Program = Map.empty; Variables = Map.empty }
  for input in inputs do
    let! nstate = runInput input state
    state <- nstate 
  return state }

let guess = 
  [ Some 10, Assign("Q", Binary('+', 
      Function("INT",Binary('*', Function("RND", Literal(Number 1M)), 
        Literal(Number 100M))), Literal(Number 1M)))
    Some 20, Assign("N", Literal(Number 0M))
    Some 30, Print(Literal(String "GUESS A NUMBER BETWEEN 1 AND 100!"), true)
    Some 40, Input "G"
    Some 50, If(Binary('=', Variable "G", Variable "Q"), 130)
    Some 60, Assign("N", Binary('+', Variable("N"), Literal(Number 1M)))
    Some 70, If(Binary('=', Variable "N", Literal(Number 7M)), 120)
    Some 80, If(Binary('<', Variable "G", Variable "Q"), 100)
    Some 90, If(Binary('>', Variable "G", Variable "Q"), 110)
    Some 100, Print(Literal(String "NOT ENOUGH! TRY AGAIN"), true)
    Some 101, Goto 30
    Some 110, Print(Literal(String "TOO MUCH! TRY AGAIN"), true)
    Some 111, Goto 30
    Some 120, Print(Literal(String "YOU LOST!"), true)
    Some 121, Goto 150
    Some 130, Print(Literal(String "YOU WON!"), true)
    Some 150, Print(Literal(String "THANKS FOR PLAYING"), true) 
    None, Run ]
    
runInputs guess |> Async.Ignore |> Async.StartImmediate

