module GammaDemo.Main

open Fable.Core
open Fable.Import
open Fable.Import.Browser
open GammaDemo
open GammaDemo.Binder
open GammaDemo.Html
open GammaDemo.Images

type State = 
  { Source : string 
    BindingContext : BindingContext
    Parsed : Node<Expr>
    Result : obj
    Times : (float * float * float * float) option
    Error : option<string> }

type Event = 
  | UpdateSource of string
  | UpdateTimes of (float * float * float * float)

let render trigger state = 
  h?div [] [
    h?textarea 
      [ "input" =!> fun ta _ -> trigger(UpdateSource(unbox<HTMLTextAreaElement>(ta).value)) ]
      [ text state.Source ]
    
    h?div [] [
      match state.Times with
      | Some(t0, t1, t2, t3) ->
          yield h?ul [] [
              h?li [] [text(sprintf "Parsing: %dms" (int (t1-t0)))]
              h?li [] [text(sprintf "Binding: %dms" (int (t2-t0)))]
              h?li [] [text(sprintf "Evaluation: %dms" (int (t3-t0)))]
            ]
      | _ -> ()
    ]
    h?div [] [
      match state.Error, state.Result with
      | Some err, _ -> yield h?p ["style"=>"color:red"] [text err]
      | _, res -> yield h?p [] [text (sprintf "Resulting value: %A" res)]
    ] 
  ]


let img = box (GammaImageConstructor())
let vars = Map.ofList [ "image", img ]
let ents = Map.ofList [ "image", { Kind = EntityKind.Constant img; Symbol = Common.createSymbol(); Value = Some img} ]

let update trigger state = function
  | UpdateTimes times ->
      { state with Times = Some times }

  | UpdateSource newSource -> 
      let t0 = performance.now()
      match Parser.parse newSource with
      | Some prog ->
          let t1 = performance.now()
          let ent, _ = Binder.bindProgram state.BindingContext prog
          try
            let t2 = performance.now()
            match Evaluator.evaluateEntity ent with
            //match Evaluator.evaluateExpr vars prog.Node with
            | :? GammaImage as img -> 
                let mutable times = None
                img.render(fun _ ->
                    let t3 = performance.now()
                    times <- Some(t0, t1, t2, t3)
                    trigger (UpdateTimes times.Value) )
                { state with 
                    Source = newSource; Parsed = prog; Error = None; Result = "(image)"
                    Times = times }
            | res -> 
                let t3 = performance.now()
                { state with 
                    Source = newSource; Parsed = prog; Error = None; Result = res
                    Times = Some(t0, t1, t2, t3) }
          with e -> 
            { state with 
                Source = newSource; Parsed = prog; Result = null; Times = None
                Error = Some (sprintf "Evaluator failed: %A" e) }
      | None ->
          { state with 
              Source = newSource; Parsed = Parser.node(Expr.Variable(Parser.node "")); 
              Times = None; Result = null; Error = Some "Parser failed" }

let initial = 
  { Source = ""; Error = None; Result = null; Times = None
    Parsed = Parser.node(Expr.Variable(Parser.node ""))
    BindingContext = Binder.createContext ents }

createApp "demo" initial render update

open GammaDemo.Images

(*
let p = image.load("pope.png")
let s = image.load("shadow.png").greyScale().blur(5)
p.combine(s,50)
*)