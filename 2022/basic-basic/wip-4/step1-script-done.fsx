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


let evalExpression = function
  | Literal(v) -> v

let toString = function
  | String s -> s

let runCommand cmd = 
  match cmd with
  | Print e -> 
      printfn "%s" (toString (evalExpression e))


let hello = 
  [ Print(Literal(String "HELLO WORLD")) ]    

for cmd in hello do
  runCommand cmd      

// STEP #2
// TODO: Add Goto, Run and update 'hello' sample  
// TODO: Add 'runInput' function & return new state
// TODO: Initial state & mutable main loop
// DEMO: Add helpers to 'runCommand' & make it recursive 
// TODO: Add Run and Goto implementations & call 'next' in Print