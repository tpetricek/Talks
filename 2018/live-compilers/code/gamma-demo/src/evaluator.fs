module GammaDemo.Evaluator
open GammaDemo
open GammaDemo.Common

let rec evaluateExpr vars = function
  | Number n -> box n
  | String s -> box s
  
  | Variable(n) -> 
      match Map.tryFind n.Node vars with
      | Some res -> res
      | _ -> failwithf "Variable '%s' is not defined." n.Node

  | Member(obj, { Node = Variable name }) -> 
      let obj = evaluateExpr vars obj.Node
      Log.trace("evaluator", "Member access: %s", name.Node)
      getProperty obj name.Node

  | Call({ Node = Member(obj, {Node = Variable name }) }, args) -> 
      let obj = evaluateExpr vars obj.Node
      let args = [| for a in args.Node -> evaluateExpr vars a.Node |]
      Log.trace("evaluator", "Method call: %s", name.Node)
      apply (getProperty obj name.Node) obj args

  | Let(var, assign, body) ->
      let assign = evaluateExpr vars assign.Node
      evaluateExpr (Map.add var.Node assign vars) body.Node
      
  | Binary(le, op, re) ->
      let le, re = evaluateExpr vars le.Node, evaluateExpr vars re.Node
      match op with 
      | '+' -> box (unbox le + unbox re)
      | '*' -> box (unbox le * unbox re)
      | '/' -> box (unbox le / unbox re)
      | '-' -> box (unbox le - unbox re) 
      | _ -> failwith "Unsupported operator"
  
  | Member _ -> failwith "Unsupported member access" 
  | Call _ -> failwith "Unsupported call structure" 


let rec evaluateEntityKind = function
  | Root -> obj()
  | Constant v -> v
  | Operator(le, op, re) ->
      let le, re = evaluateEntity le, evaluateEntity re
      match op with 
      | '+' -> box (unbox le + unbox re)
      | '*' -> box (unbox le * unbox re)
      | '/' -> box (unbox le / unbox re)
      | '-' -> box (unbox le - unbox re) 
      | _ -> failwith "Unsupported operator"
  
  | Binding(_, _, body) ->
      evaluateEntity body
  | Reference(_, value) ->
      evaluateEntity value
  
  | MemberAccess(obj, { Kind = Name name }) -> 
      let obj = evaluateEntity obj
      Log.trace("evaluator", "Member access: %s", name)
      getProperty obj name

  | MethodCall({ Kind = MemberAccess(obj, {Kind = Name name }) }, { Kind = ArgumentList args }) -> 
      let obj = evaluateEntity obj
      let args = [| for a in args -> evaluateEntity a |]
      Log.trace("evaluator", "Method call: %s", name)
      apply (getProperty obj name) obj args
  
  | MethodCall _ -> failwith "Unexpected method call structure"
  | MemberAccess _ -> failwith "Unexpected member access structure"
  | Name _ -> failwith "Cannot evaluate name"
  | ArgumentList _ -> failwith "Cannot evaluate argument list"

and evaluateEntity ent = 
  if ent.Value = None then
    ent.Value <- Some(evaluateEntityKind ent.Kind)
  ent.Value.Value