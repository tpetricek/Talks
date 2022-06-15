module Basic.App

open Browser.Dom
open Basic.Helpers
open Basic.Domain
open Basic.Evaluator
open Basic.Parser

// DEMO

let hello = 
  [ Some 10, Print(Literal(String "HELLO WORLD"))
    Some 20, Goto 10
    None, Run ]

// DEMO: Add guess

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
    None, List ]

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
  let! cmd = input |> awaitObservable
  let cmd = parseInput (tokenizeString cmd)
  let! state = runInput state cmd
  return! loop state }

async {
  let mutable state = { Program = Map.empty; Variables = Map.empty }
  for line in lines do 
    let cmd = parseInput (tokenizeString line)
    let! newState = runInput state cmd
    state <- newState 
  return! loop state }
|> Async.StartImmediate
