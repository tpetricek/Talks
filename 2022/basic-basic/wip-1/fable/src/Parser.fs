module Basic.Parser
open Basic.Domain

type Token =
  | Equals
  | Ident of string
  | Operator of char
  | Bracket of char
  | Numeral of float
  | Text of string

let str rcl = System.String(Array.rev(Array.ofSeq rcl))
let isLetter c = (c >= 'A' && c <= 'Z') || c = '$'
let isOp c = "+-*/<>".Contains(string c)
let isBracket c = "()".Contains(string c)
let isNumber c = (c >= '0' && c <= '9')

let rec tokenize toks = function
  | c::cs when isLetter c -> ident toks [c] cs
  | c::cs when isNumber c -> number toks [c] cs
  | c::cs when isOp c -> tokenize ((Operator c)::toks) cs
  | c::cs when isBracket c -> tokenize ((Bracket c)::toks) cs
  | '='::cs -> tokenize (Equals::toks) cs
  | '"'::cs -> strend toks [] cs
  | ' '::cs -> tokenize toks cs
  | [] -> List.rev toks
  | cs -> failwithf "Cannot tokenize: %s" (str (List.rev cs))

and strend toks acc = function
  | '"'::cs -> tokenize (Text(str acc)::toks) cs
  | c::cs -> strend toks (c::acc) cs
  | [] -> failwith "End of string not found"

and ident toks acc = function
  | c::cs when isLetter c -> ident toks (c::acc) cs
  | '$'::input -> tokenize (Ident(str ('$'::acc))::toks) input
  | input -> tokenize (Ident(str acc)::toks) input

and number toks acc = function
  | c::cs when isNumber c -> number toks (c::acc) cs
  | '.'::cs when not (List.contains '.' acc) -> number toks ('.'::acc) cs
  | input -> tokenize (Numeral(float (str acc))::toks) input

let tokenizeString s = tokenize [] (List.ofSeq s)

let rec parseBinary left = function
  | (Operator o)::toks -> 
      let right, toks = parseExpr toks
      Binary(o, left, right), toks
  | Equals::toks -> 
      let right, toks = parseExpr toks
      Binary('=', left, right), toks
  | toks -> left, toks

and parseExpr = function
  | (Text s)::toks -> parseBinary (Literal(String s)) toks
  | (Numeral n)::toks -> parseBinary (Literal(Number(decimal n))) toks
  | (Ident i)::(Bracket '(')::toks ->
      let arg, toks = parseExpr toks 
      match toks with 
      | (Bracket ')')::toks -> Function(i, arg), toks
      | _ -> 
        let bin, toks = parseBinary arg toks
        Function(i, bin), toks
  | (Ident v)::toks -> parseBinary (Variable v) toks
  | toks -> failwithf "Parsing expr failed. Unexpected: %A" toks

let rec parseInput toks = 
  let line, toks = 
    match toks with
    | (Numeral ln)::toks -> Some(int ln), toks
    | _ -> None, toks
  match toks with 
  | (Ident "RUN")::[] -> line, Run
  | (Ident "GOTO")::(Numeral lbl)::[] -> line, Goto(int lbl)
  | (Ident "INPUT")::(Ident var)::[] -> line, Input(var)
  | (Ident "IF")::toks -> 
      let arg1, toks = parseExpr toks
      match toks with 
      | (Ident "GOTO")::(Numeral ln)::[] ->
          line, If(arg1, int ln)      
      | _ ->
          failwithf "Parsing IF failed. Expected GOTO."
  | (Ident "PRINT")::toks -> 
      let arg, toks = parseExpr toks
      if toks <> [] then failwithf "Parsing PRINT failed. Unexpected: %A" toks
      line, Print(arg)
  | (Ident id)::Equals::toks ->
      let arg, toks = parseExpr toks
      if toks <> [] then failwithf "Parsing = failed. Unexpected: %A" toks
      line, Assign(id, arg)
  | _ -> failwithf "Parsing command failed. Unexpected: %A" toks  