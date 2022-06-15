module Basic.Domain

open Browser.Dom
open Basic.Helpers

type Value = 
  | String of string
  | Number of decimal
  | Bool of bool

type Expression = 
  | Literal of Value
  | Variable of string
  | Binary of char * Expression * Expression
  | Function of string * Expression

type Command = 
  | Print of Expression 
  | Goto of int
  | Run 
  | List
  | Assign of string * Expression
  | Input of string
  | If of Expression * int

type Input = int option * Command
