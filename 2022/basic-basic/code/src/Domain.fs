module Basic.Domain

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
  | Assign of string * Expression
  | Input of string
  | If of Expression * int 
  | Print of Expression
  | Goto of int 
  | Run
  | List
