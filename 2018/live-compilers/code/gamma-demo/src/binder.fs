module GammaDemo.Binder

open GammaDemo
open GammaDemo.Common

/// Represents case of the EntityKind union
type EntityCode = int

/// As we bind, we keep root entity, current scope & variables in scope
type BindingContext = 
  { Variables : Map<string, Entity>  
    Root : Entity

    /// Table with previously created entities. This is a mutable mapping from 
    /// list of symbols (antecedent entities) together with entity kind & name
    /// to the actual entity. Antecedents capture dependencies (if dependency 
    /// changed, we need to recreate the entity that depends on them)
    Table : ListDictionary<Symbol, Map<EntityCode * string, Entity>> 
    /// Collects all bound entities
    Bound : ResizeArray<Entity> }

let entityCodeNameAndAntecedents = function
  | EntityKind.Root -> 1, "root", []
  | EntityKind.Binding(v, a, b) -> 2, "let", [v;a;b]
  | EntityKind.Constant(:? int as n) -> 3, "number " + string n, []
  | EntityKind.Constant(:? string as s) -> 3, "string " + s, []
  | EntityKind.Operator(l,'+',r) -> 4, "+ operator", []
  | EntityKind.Operator(l,'*',r) -> 5, "* operator", []
  | EntityKind.Operator(l,'/',r) -> 6, "/ operator", []
  | EntityKind.Operator(l,'-',r) -> 7, "- operator", []
  | EntityKind.Operator _ -> failwith "Invalid operator"
  | EntityKind.Reference(n, v) -> 8, "variable " + n, [v]
  | EntityKind.ArgumentList(args) -> 9, "arguments", args
  | EntityKind.MethodCall(i,a) -> 10, "call", [i;a]
  | EntityKind.MemberAccess(i, m) -> 11, "member", [i;m]
  | EntityKind.Name(n) -> 12, "name " + n, []
  | EntityKind.Constant _ -> failwith "Unexpected constant"

/// Lookup entity (if it can be reused) or create & cache a new one
let bindEntity ctx kind =
  let code, name, antecedents = entityCodeNameAndAntecedents kind
  let symbols = ctx.Root::antecedents |> List.map (fun a -> a.Symbol)
  let nestedDict = 
    match ListDictionary.tryFind symbols ctx.Table with
    | None -> Map.empty
    | Some res -> res
  if nestedDict.ContainsKey (code, name) then 
    Log.trace("binder", "Cached: binding %s", name)
    nestedDict.[code, name]
  else
    Log.trace("binder", "New: binding %s", name)
    let symbol = createSymbol ()
    let entity = { Kind = kind; Symbol = symbol; Value = None }
    ListDictionary.set symbols (Map.add (code, name) entity nestedDict) ctx.Table
    entity    

/// Assign entity to a node in parse tree
let setEntity ctx node entity = 
  ctx.Bound.Add(entity)
  node.Entity <- Some entity
  entity

/// Bind entities to expressions in the parse tree
let rec bindExpression ctx node = 
  match node.Node with
  | Expr.Variable(name) ->
      match ctx.Variables.TryFind name.Node with 
      | Some decl -> bindEntity ctx (EntityKind.Reference(name.Node, decl)) |> setEntity ctx node
      | _ -> bindEntity ctx (EntityKind.Name(name.Node)) |> setEntity ctx node

  | Expr.Binary(lExpr, op, rExpr) ->
      let lEnt = bindExpression ctx lExpr
      let rEnt = bindExpression ctx rExpr
      bindEntity ctx (EntityKind.Operator(lEnt, op, rEnt)) |> setEntity ctx node

  | Expr.Number n -> 
      bindEntity ctx (EntityKind.Constant(n)) |> setEntity ctx node

  | Expr.String s -> 
      bindEntity ctx (EntityKind.Constant(s)) |> setEntity ctx node

  | Expr.Member(instExpr, memExpr) ->
      let instEnt = bindExpression ctx instExpr
      let memEnt = bindExpression ctx memExpr 
      bindEntity ctx (EntityKind.MemberAccess(instEnt, memEnt)) |> setEntity ctx node

  | Expr.Call(instExpr, argsNode) ->
      let inst = bindExpression ctx instExpr
      let args = argsNode.Node |> List.map (bindExpression ctx)
      let argList = bindEntity ctx (EntityKind.ArgumentList(args)) |> setEntity ctx argsNode
      bindEntity ctx (EntityKind.MethodCall(inst, argList)) |> setEntity ctx node 

  | Expr.Let(name, assignExpr, bodyExpr) ->
      let inst = bindExpression ctx assignExpr
      let ctx = { ctx with Variables = Map.add name.Node inst ctx.Variables }
      bindExpression ctx bodyExpr |> setEntity ctx bodyExpr

/// Bind entities to all nodes in the program
let bindProgram ctx (program:Node<Expr>) =
  ctx.Bound.Clear()
  bindExpression ctx program |> setEntity ctx program,
  ctx.Bound.ToArray()
  
/// Create a new binding context - this stores cached entities
let createContext globals =
  let root = { Kind = EntityKind.Root; Symbol = createSymbol(); Value = None }
  { Table = System.Collections.Generic.Dictionary<_, _>(); 
    Bound = ResizeArray<_>(); Variables = globals; Root = root; }