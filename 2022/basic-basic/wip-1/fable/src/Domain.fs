module Basic.Domain

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
  | Print of Expression 
  | Goto of int
  | Input of string
  | Assign of string * Expression
  | If of Expression * int
  | Run 
