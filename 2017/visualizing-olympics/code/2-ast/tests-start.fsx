#r "lib/thegamma.dll"
open TheGamma
open TheGamma.Ast

// --------------------------------------------------------------------------------------
// Helpers for writing tests for parser
// --------------------------------------------------------------------------------------

/// Type-safe assertion
let equal (expected:'T) (actual:'T) = 
  if expected <> actual then failwithf "%A <> %A" expected actual

/// Assert that expression contains given sub-expression
let rec hasSubExpr f e =
  if f e then true else
    match e with 
    | ExprNode(es, _) -> es |> List.exists (fun e -> hasSubExpr f e.Node)
    | ExprLeaf _ -> false

/// Assert that result contains given sub-expression
let assertSubExpr f (code, (cmds:Node<Command> list), errs) = 
  let matches = 
    cmds |> List.exists (fun cmd ->
      match cmd.Node with
      | Command.Expr e -> hasSubExpr f e.Node
      | Command.Let(_, e) -> hasSubExpr f e.Node )
  equal true matches

/// Tokenize & parse test code
let parse (code:string) = 
  let code = code.Replace("\r", "").Replace("\n    ","\n")
  let res, errs = Parser.parseProgram code
  for e in errs do if e.Number = 299 then failwith e.Message
  code, res.Body.Node, [ for e in errs -> e.Number, (e.Range.Start, e.Range.End) ]

/// Specify conditions on arguments
let hasArgValues conds { Node = args } = 
  List.zip conds args |> List.forall (fun (f, (arg:Argument)) -> 
    f arg.Value.Node)

// --------------------------------------------------------------------------------------
// DEMO: Checking properties, calls and values
// --------------------------------------------------------------------------------------


let isProperty name e = 
  // TODO: match Expr
  true

let isCall name ac e = 
  // TODO: match Expr
  true

let isVal (v:float) e =   
  // TODO: match Expr
  true

// --------------------------------------------------------------------------------------
// TESTS: Call chains and nesting
// --------------------------------------------------------------------------------------

let actual1 = parse """
olympics
  .'group data'.'by Team'.'sum Gold'.then
  .'get the data'
"""

actual1 |> assertSubExpr (isProperty "sum Gold")
actual1 |> assertSubExpr (isProperty "get the data")




let actual2 = parse """
olympics
  .'group data'.'by Team'.'sum Gold'.then
  .paging.take(10)
  .'get the data'
"""
actual2 |> assertSubExpr (isCall "take" (hasArgValues [isVal 10.0]))
actual2 |> assertSubExpr (isProperty "get the data")






// 
