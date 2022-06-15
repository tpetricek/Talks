// DOMAIN MODEL
// TODO: Value is string, Expression is literal, Command is print

type Value = 
  | String of string
   
type Expression = 
  | Literal of Value

type Command = 
  | Print of Expression 

// Evaluation
// TODO: evalExpr, toString and runCommand

let rec evalExpr = function
  | Literal v -> v

let toString = function
  | String s -> s

let runCommand cmd = 
  match cmd with 
  | Print(e) -> printfn "%s" (toString(evalExpr e))

// Demo
// TODO: hello world, run commands
let hello = 
  [ Print(Literal(String "HELLO WORLD")) ]

for cmd in hello do
  runCommand cmd
