#r "System.Xml.Linq.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open System
open System.IO
open FSharp.Data
open FSharp.Data.JsonExtensions

// ----------------------------------------------------------------------------
// Domain model
// ----------------------------------------------------------------------------

type Value = 
  | String of string
  | Number of decimal

type Aggregation = 
  | GroupKey
  | CountAll
  | CountDistinct of string
  | ReturnUnique of string
  | ConcatValues of string
  | Sum of string
  | Mean of string

type SortDirection =
  | Ascending
  | Descending 

type Paging =
  | Take of int
  | Skip of int
  
type Transformation = 
  | DropColumns of string list
  | SortBy of (string * SortDirection) list
  | GroupBy of string list * Aggregation list
  | FilterBy of (string * bool * string) list
  | Paging of Paging list
  | Empty

// ----------------------------------------------------------------------------
// Parsing query strings
// ----------------------------------------------------------------------------

let ops = 
  [ "count-dist", CountDistinct; "unique", ReturnUnique; 
    "concat-vals", ConcatValues; "sum", Sum; "mean", Mean ]

let trimIdent (s:string) = 
  if s.StartsWith("'") && s.EndsWith("'") then s.Substring(1, s.Length-2)
  else s

let parseAggOp op =
  if op = "key" then GroupKey
  elif op = "count-all" then CountAll
  else
    let parsed = ops |> List.tryPick (fun (k, f) ->
      if op.StartsWith(k) then Some(f(trimIdent(op.Substring(k.Length + 1))))
      else None)
    if parsed.IsSome then parsed.Value else failwith "Unknonw operation"

let parseCondition (cond:string) = 
  let cond = cond.Trim()
  let start = if cond.StartsWith("'") then cond.IndexOf('\'', 1) else 0
  let neq, eq = cond.IndexOf(" neq ", start), cond.IndexOf(" eq ", start)
  if neq <> -1 then trimIdent (cond.Substring(0, neq)), false, cond.Substring(neq + 5)
  elif eq <> -1 then trimIdent (cond.Substring(0, eq)), true, cond.Substring(eq + 4)
  else failwith "Incorrectly formatted condition"

let parseTransform (op, args) = 
  match op, args with
  | "drop", columns -> DropColumns(List.map trimIdent columns)
  | "sort", columns -> SortBy(columns |> List.map (fun col -> 
      if col.EndsWith(" asc") then trimIdent (col.Substring(0, col.Length-4)), Ascending
      elif col.EndsWith(" desc") then trimIdent (col.Substring(0, col.Length-5)), Descending
      else trimIdent col, Ascending))
  | "filter", conds -> FilterBy(List.map parseCondition conds)
  | "groupby", ops ->
      let keys = ops |> List.takeWhile (fun s -> s.StartsWith "by ") |> List.map (fun s -> trimIdent (s.Substring(3)))
      let aggs = ops |> List.skipWhile (fun s -> s.StartsWith "by ") |> List.map parseAggOp
      GroupBy(keys, aggs)
  | "take", [n] -> Paging [Take (int n)]
  | "skip", [n] -> Paging [Skip (int n)]
  | _ -> failwith "Unsupported transformation"

let parseArgs (s:string) = 
  let rec loop i quoted current acc = 
    let parseCurrent () = System.String(Array.ofList (List.rev current))
    if i = s.Length then List.rev (parseCurrent()::acc) else
    let c = s.[i]
    if c = '\'' && quoted then loop (i + 1) false (c::current) acc
    elif c = '\'' && not quoted then loop (i + 1) true (c::current) acc
    elif c = ',' && not quoted then loop (i + 1) quoted [] (parseCurrent()::acc)
    else loop (i + 1) quoted (c::current) acc
  loop 0 false [] [] 

let parseChunk (s:string) =
  let openPar, closePar = s.IndexOf('('), s.LastIndexOf(')')
  if openPar = -1 || closePar = -1 then s, []
  else s.Substring(0, openPar), parseArgs (s.Substring(openPar + 1, closePar - openPar - 1))
  
let fromUrl (s:string) = 
  System.Web.HttpUtility.UrlDecode(s)
    .Split([|'$'|], StringSplitOptions.RemoveEmptyEntries) 
  |> List.ofSeq
  |> List.map parseChunk 
  |> List.map parseTransform

// ----------------------------------------------------------------------------
// Helpers for query evaluations
// ----------------------------------------------------------------------------

let inline pickField name obj = 
  Array.pick (fun (n, v) -> if n = name then Some v else None) obj

let inline the s = match List.ofSeq s with [v] -> v | _ -> failwith "Not unique"
let asString = function String s -> s | Number n -> string n
let asDecimal = function String s -> decimal s | Number n -> n

let applyAggregation kvals group = function
 | GroupKey -> kvals
 | CountAll -> [ "count", Number(group |> Seq.length |> decimal) ]
 | CountDistinct(fld) -> [ fld, Number(group |> Seq.distinctBy (pickField fld) |> Seq.length |> decimal) ]
 | ReturnUnique(fld) -> [ fld, group |> Seq.map (pickField fld) |> the ]
 | ConcatValues(fld) -> [ fld, group |> Seq.map(fun obj -> pickField fld obj |> asString) |> Seq.distinct |> String.concat ", " |> String ]
 | Sum(fld) -> [ fld, group |> Seq.sumBy (fun obj -> pickField fld obj |> asDecimal) |> Number ]
 | Mean(fld) -> [ fld, group |> Seq.averageBy (fun obj -> pickField fld obj |> asDecimal) |> Number ]

let compareFields o1 o2 (fld, order) = 
  let reverse = if order = Descending then -1 else 1
  match pickField fld o1, pickField fld o2 with
  | Number d1, Number d2 -> reverse * compare d1 d2
  | String s1, String s2 -> reverse * compare s1 s2
  | _ -> failwith "Cannot compare values"

// ----------------------------------------------------------------------------
// Apply single transformation to data 
// ----------------------------------------------------------------------------

let transformData (objs:seq<(string * Value)[]>) = function
  // TODO: Empty
  // TODO: Paging
  // TODO: Drop columns
  // DEMO: Sort, group, filter
  | _ -> objs

// ----------------------------------------------------------------------------
// Loading data for olympics 
// ----------------------------------------------------------------------------

let file = __SOURCE_DIRECTORY__ + "/data/olympics.json"
let data =  
  JsonValue.Parse(File.ReadAllText(file)).AsArray()
  |> Seq.map (function
      | JsonValue.Record(kvs) -> 
        kvs |> Array.map (function
          | k, JsonValue.String s -> k, String s
          | k, JsonValue.Number n -> k, Number n 
          | _ -> failwith "Wrong row")
      | _ -> failwith "Wrong row")

let transforms = 
  fromUrl
    ( "filter(Games eq Rio (2016))$" +
      "groupby(by Athlete,sum Gold,key)$" +
      "sort(Gold desc)$take(3)" )

transforms
|> List.fold (fun data transform ->
    transformData data transform) data


















// 
