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

  | Binary(le, op, re) ->
      let le, re = evaluateExpr vars le.Node, evaluateExpr vars re.Node
      match op with 
      | '+' -> box (unbox le + unbox re)
      | '*' -> box (unbox le * unbox re)
      | '/' -> box (unbox le / unbox re)
      | '-' -> box (unbox le - unbox re) 
      | _ -> failwith "Unsupported operator"

  // TODO: Implement member access (getProperty)
  | Member(obj, { Node = Variable name }) -> 
      null
  // DEMO: Implement let binding
  | Let(var, assign, body) ->
      null
  // DEMO: Show implementation of call
  | Call _ -> 
      null     
  
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

  // DEMO: Add member access and method call
  | MemberAccess _ 
  | MethodCall _ -> 
      null

  // TODO: Binding and reference
  | Binding(_, _, body) ->
      null
  | Reference(_, value) ->
      null

  | MethodCall _ -> failwith "Unexpected method call structure"
  | MemberAccess _ -> failwith "Unexpected member access structure"
  | Name _ -> failwith "Cannot evaluate name"
  | ArgumentList _ -> failwith "Cannot evaluate argument list"

and evaluateEntity ent = 
  // TODO: Modify this function to cache values
  evaluateEntityKind ent.Kind