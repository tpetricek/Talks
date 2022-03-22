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
    Variables : Map<string, obj>
    Result : obj
    Times : (float * float * float * float) option
    Error : option<string> }
  member x.Success(newSource, prog, result, times) =
    { x with Source = newSource; Parsed = prog; Error = None; Result = result; Times = Some times }
  member x.Failed(newSource, error, ?parsed) = 
    let p = match parsed with None -> Parser.node(Expr.Variable(Parser.node "")) | Some p -> p
    { x with Source = newSource; Parsed = p; Times = None; Result = null; Error = Some error }
  
type Event = UpdateSource of string


let render trigger state = 
  h?div [] [
    h?textarea 
      [ "input" =!> fun ta _ -> trigger(UpdateSource(unbox<HTMLTextAreaElement>(ta).value)) ]
      [ text state.Source ]
    
    h?div [ "class" => "info" ] [
      yield h?h2 [] [text "Evaluation result"]
      match state.Error, state.Result with
      | Some err, _ -> yield h?p ["style"=>"margin-left:30px;color:red"] [text err]
      | _, res -> yield h?p ["style"=>"margin-left:30px;"] [text (sprintf "Resulting value: %A" res)]
    ] 

    h?div [ "class" => "info" ] [
      match state.Times with
      | Some(t0, t1, t2, t3) ->
          yield h?h2 [] [text "Performance statistics"]
          yield h?ul [] [
              h?li [] [text "Parsing: "; h?strong[] [text(sprintf "%dms" (int (t1-t0)))] ]
              h?li [] [text "Binding: "; h?strong[] [text(sprintf "%dms" (int (t2-t0)))] ]
              h?li [] [text "Evaluation: "; h?strong[] [text(sprintf "%dms" (int (t3-t0)))] ]
            ]
      | _ -> ()
    ]
  ]


let update state (UpdateSource newSource) = async {
  let t0 = performance.now()
  match Parser.parse newSource with
  | Some prog ->
      let t1 = performance.now()
      let ent, _ = Binder.bindProgram state.BindingContext prog
      try
        let t2 = performance.now()
        // TODO: Switch to Evaluator.evaluateEntity ent
        match Evaluator.evaluateEntity ent with
        | :? GammaImage as img -> 
            let mutable times = None
            let! a = img.render()
            let t3 = performance.now()
            return state.Success(newSource, prog, "(image)", (t0,t1,t2,t3))
        | res -> 
            let t3 = performance.now()
            return state.Success(newSource, prog, res, (t0,t1,t2,t3))
      with e -> return state.Failed(newSource, sprintf "Evaluator failed: %s" e.Message, prog) 
  | None -> return state.Failed(newSource, "Parser failed") }



let img = box (GammaImageConstructor())
let vars = Map.ofList [ "image", img ]
let ents = Map.ofList [ "image", createGlobalEntity img ]

let initial = 
  { Variables = vars; Source = ""
    Error = None; Result = null; Times = None
    Parsed = Parser.node(Expr.Variable(Parser.node ""))
    BindingContext = Binder.createContext ents }

createApp "editor" initial render update