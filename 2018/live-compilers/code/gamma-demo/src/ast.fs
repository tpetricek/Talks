namespace GammaDemo
open GammaDemo.Common

/// Represents different kinds of entities that we create. Roughhly
/// corresponds to all places in code where something has a name.
type EntityKind = 
  | Root
  | Binding of variable:Entity * assignment:Entity * body:Entity
  | Constant of obj
  | Operator of left:Entity * operator:char * right:Entity
  | Reference of name:string * value:Entity
  | ArgumentList of arguments:Entity list
  | MethodCall of instance:Entity * arguments:Entity
  | MemberAccess of instance:Entity * name:Entity 
  | Name of name:string
  
/// An entity represents a thing in the source code to which we attach additional info.
/// It is uniquely identified by its `Symbol` (which is also used for lookups)
and Entity = 
  { Kind : EntityKind
    Symbol : Symbol 
    mutable Value : obj option }

/// Node wraps syntax element with other information. Whitespce before/after are tokens 
/// around it that the parser skipped (they may be whitespace, but also skipped error tokens).
/// Entity is assigned to the expression later by a binder.
type Node<'T> = 
  { Node : 'T
    mutable Entity : Entity option }

/// An expression represents parsed expression. Nodes provide
/// access to entities attached to expression after binding.
type Expr = 
  | Let of Node<string> * Node<Expr> * Node<Expr>
  | Variable of Node<string>
  | Member of Node<Expr> * Node<Expr>
  | Call of Node<Expr> * Node<Node<Expr> list>
  | Number of int
  | String of string
  | Binary of Node<Expr> * char * Node<Expr>
  