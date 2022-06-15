module Basic.Domain

type Value = 
  | String of string
  | Number of decimal
  | Bool of bool

type Expression = 
  | Variable of string
  | Literal of Value
  | Binary of char * Expression * Expression
  | Function of string * Expression

type Command = 
  | Print of Expression
  | Goto of int
  | Assign of string * Expression
  | Input of string
  | If of Expression * int
  | Run
  | List

