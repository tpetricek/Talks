module Basic.App
open Basic.Helpers
open Basic.Domain
open Basic.Evaluator
open Basic.Parser

let lines = 
  [ "10 Q=1+INT(100*RND(1))"
    "20 N=0"
    "30 PRINT \"GUESS A NUMBER BETWEEN 1 AND 100!\""
    "40 INPUT G"
    "50 IF G=Q GOTO 130"
    "60 N=N+1"
    "70 IF N=7 GOTO 120"
    "80 IF G<Q GOTO 100"
    "90 IF G>Q GOTO 110"
    "100 PRINT \"NOT ENOUGH! TRY AGAIN\""
    "101 GOTO 30"
    "110 PRINT \"TOO MUCH! TRY AGAIN\""
    "111 GOTO 30"
    "120 PRINT \"YOU LOST!\""
    "121 GOTO 150"
    "130 PRINT \"YOU WON!\""
    "150 PRINT \"THANKS FOR PLAYING\"" ]

let rec loop state = async {
  let! line = input |> awaitObservable
  let cmd = parseInput (tokenizeString line)
  let! newState = acceptInput state cmd 
  return! loop newState }

async {
  let mutable state = { Program = Map.empty; Variables = Map.empty }
  for ln in lines do 
    let ln, cmd = parseInput (tokenizeString ln)
    let! newState = acceptInput state (ln, cmd)
    state <- newState 
  return! loop state }
|> Async.StartImmediate

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

