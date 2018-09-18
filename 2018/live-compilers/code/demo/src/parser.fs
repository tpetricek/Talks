module GammaDemo.Parser

open GammaDemo
open GammaDemo.Parsec

// Create node with no entity set
let node e = { Node = e; Entity = None }

// Basics: operators (+, -, *, /), cell reference (e.g. A10), number (e.g. 123)
let nonQuote = pred (fun t -> t <> '"')
let operator = char '+' <|> char '-' <|> char '*' <|> char '/'
let number = integer |> map (Number >> node)
let ident = oneOrMore letter |> map (fun c -> node(System.String(Array.ofList c)))
let str = 
  char '"' <*>> zeroOrMore nonQuote <<*> char '"' 
  |> map (fun chars -> node(String(System.String(Array.ofList chars))))

// We need to use `expr` recursively, which is handled via mutable slots.
let exprSetter, expr = slot ()
let chainEndSetter, chainEnd = slot ()

// Member accesses and calls
let arguments = separatedOrEmpty (anySpace <*> char ',' <*> anySpace) expr
let variable = ident |> map (Variable >> node)
let memberEnd = char '.' <*>> anySpace <*>> variable <*> chainEnd |> map (fun (e2, ce) e1 -> ce(node(Member(e1, e2))))
let endEnd = unit () |> map (fun _ e -> e)
let callEnd = 
  char '(' <*>> anySpace <*>> arguments <<*> anySpace <<*> char ')' <*> chainEnd
  |> map (fun (args, ce) inst -> ce(node(Call(inst, node args))))
let chain = variable <<*> anySpace <*> chainEnd |> map (fun (e, f) -> f e)
let chainEndAux = memberEnd <|> callEnd <|> endEnd
chainEndSetter.Set chainEndAux

// Let binding (which is a bit long)
let binding = 
  string "let" <*>> anySpace <*>> ident <<*> anySpace <<*> 
    char '=' <<*> anySpace <*> expr <<*> anySpace <*> expr
  |> map (fun ((n, e1), e2) -> node(Expr.Let(n, e1, e2)))

// Nested operator uses need to be parethesized, for example (1 + (3 * 4)).
// <expr> is a binary operator without parentheses, number, reference or
// nested brackets, while <term> is always bracketed or primitive.
let brack = char '(' <*>> anySpace <*>> expr <<*> anySpace <<*> char ')'
let term = str <|> number <|> brack <|> binding <|> chain 
let binary = term <<*> anySpace <*> operator <<*> anySpace <*> term |> map (fun ((l,op), r) -> node(Binary(l, op, r)))
let exprAux = binary <|> term
exprSetter.Set exprAux

// Run the parser on a given input
let program = anySpace <*>> expr <<*> anySpace 
let parse input = run program input