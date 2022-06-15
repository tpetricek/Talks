module App
open Browser.Dom

let inputEl = document.getElementById("in") :?> Browser.Types.HTMLInputElement
let inputEvt = Event<string>()
inputEl.onkeypress <- fun ke -> 
  if ke.code = "Enter" then 
    inputEvt.Trigger(inputEl.value)
    inputEl.value <- ""

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

let rec evalExpr state = function
  | Variable v -> state.Variables.[v]
  | Literal v -> v

let rec runProgram (ln, cmd) state = async {
  let sortedProg () = 
    state.Program |> Map.toSeq |> Seq.sortBy fst 
  let next () = async {
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
      return! next ()
  | Print(e, false) -> 
      print $"{toString(evalExpr state e)}"
      return! next ()
  | Run ->
      let ln, cmd = sortedProg() |> Seq.head
      return! runProgram (Some ln, cmd) state }

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

let hello2 = 
  [ Some 10, Print(Literal(String "HELLO WORLD"), true) 
    Some 20, Goto 10 
    None, Run ]

runInputs hello2 |> Async.Ignore |> Async.StartImmediate

