module Basic.App
open Basic.Helpers
open Basic.Evaluator
open Basic.Domain

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

let guess = 
  [ Some 10, Assign("Q", Binary('+', 
      Function("INT",Binary('*', Function("RND", Literal(Number 1M)), 
        Literal(Number 100M))), Literal(Number 1M)))
    Some 20, Assign("N", Literal(Number 0M))
    Some 30, Print(Literal(String "GUESS A NUMBER BETWEEN 1 AND 100!"))
    Some 40, Input "G"
    Some 50, If(Binary('=', Variable "G", Variable "Q"), 130)
    Some 60, Assign("N", Binary('+', Variable("N"), Literal(Number 1M)))
    Some 70, If(Binary('=', Variable "N", Literal(Number 7M)), 120)
    Some 80, If(Binary('<', Variable "G", Variable "Q"), 100)
    Some 90, If(Binary('>', Variable "G", Variable "Q"), 110)
    Some 100, Print(Literal(String "NOT ENOUGH! TRY AGAIN"))
    Some 101, Goto 30
    Some 110, Print(Literal(String "TOO MUCH! TRY AGAIN"))
    Some 111, Goto 30
    Some 120, Print(Literal(String "YOU LOST!"))
    Some 121, Goto 150
    Some 130, Print(Literal(String "YOU WON!"))
    Some 150, Print(Literal(String "THANKS FOR PLAYING")) 
    None, List
    None, Run ]

async {
  let mutable state = { Program = Map.empty; Variables = Map.empty }
  for cmd in guess do
    let! newState = runInput state cmd 
    state <- newState }
|> Async.StartImmediate  