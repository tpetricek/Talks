﻿// ----------------------------------------------------------------------------
// Domain model
// ----------------------------------------------------------------------------

type Value = 
  | String of string

type Expression = 
  | Literal of Value
  | Variable of string

type Command =
  | Input of string
  | Print of Expression
  | Goto of int 
  | Run

// ----------------------------------------------------------------------------
// Parser
// ----------------------------------------------------------------------------

let (|Prefix|_|) (prefix:string) (s:string) = 
  if s.StartsWith prefix then Some(s.Substring(prefix.Length).Trim())
  else None

let (|Number|_|) (s:string) = 
  let i = s |> Seq.takeWhile System.Char.IsNumber |> Seq.length  
  match System.Int32.TryParse(s.Substring(0, i)) with 
  | true, n -> Some(n, s.Substring(i).Trim())
  | _ -> None

let (|Quoted|_|) q (s:string) =
  if s.[0] = q then
    let i = s.IndexOf(q, 1)
    if i > -1 then Some(s.Substring(1, i-1), s.Substring(i+1).Trim())
    else None
  else None    

let parseExpression s = 
  match s with 
  | Quoted '"' (s, _) -> Literal(String s)
  | v -> Variable v 

let parseCommand cmd =
  match cmd with 
  | Prefix "PRINT" arg -> Print(parseExpression arg)
  | Prefix "INPUT" var -> Input(var)
  | Prefix "GOTO" (Number(line, "")) -> Goto(line)
  | Prefix "RUN" "" -> Run
  | _ -> failwith $"Unknown command: {cmd}"    

let parse input = 
  match input with 
  | Number (line, cmd) -> Some line, parseCommand cmd
  | cmd -> None, parseCommand cmd

// DEMO: Step 1 - add parsing functions
// TODO: parseExpression - turn quoted into a literal; else variable
// TODO: parseCommand - PRINT to produce Print of parseExpression
// TODO: parse input starting with Number

// ----------------------------------------------------------------------------
// Runtime
// ----------------------------------------------------------------------------

type State = 
  { Variables : Map<string, Value>
    Lines : list<int * Command> }

let rec run ctx (line, cmd) = 
  let next ctx = 
    if line <> -1 then 
      let rest = ctx.Lines |> List.filter (fun (ln, _) -> ln > line) 
      if rest.Length = 0 then ctx else
      run ctx (Seq.minBy fst rest)
    else ctx  
  match cmd with 
  | Input var ->
      let s = System.Console.ReadLine()
      let ctx = { ctx with Variables = ctx.Variables.Add(var, String s) }
      next ctx
  | Print(Literal(String s)) ->
      printfn "%s" s
      next ctx
  | Print(Variable v) ->
      printfn "%A" ctx.Variables.[v]
      next ctx
  | Goto(ln) ->
      run ctx (List.find (fun (l, _) -> l = ln) ctx.Lines)
  | Run ->
      run ctx (List.minBy (fun (l, _) -> l) ctx.Lines)

// DEMO: Add the state representation
// TODO: recursive 'run' takes ctx and (line, cmd)
// TODO: Implement everything except for 'next'
// DMEO: Add the 'next' operation

// ----------------------------------------------------------------------------
// Main loop
// ----------------------------------------------------------------------------

// DEMO: Add the 'main' loop
let rec main ctx : unit =
  let s = System.Console.ReadLine()
  match parse s with 
  | Some ln, cmd -> main { ctx with Lines = (ln,cmd) :: ctx.Lines }
  | None, cmd -> main (run ctx (-1, cmd))

main { Lines = []; Variables = Map.empty }      