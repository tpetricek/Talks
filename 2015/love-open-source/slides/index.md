- title : The Big F# and Open-Source Love Story
- description : True open-source is not about having the source code available on GitHub.
   It is about the community and the mentality in the community. In this talk, I'll look at
   the thing that I love the most about F# - the active open-source community around it.
- author : Tomas Petricek
- theme : night
- transition : none

***************************************************************************************************

# The big F# and open-source love story

<br /><br />
<img src="images/heart-pink.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

**Tomas Petricek**, F# Works  
[@tomaspetricek](http://twitter.com/tomaspetricek)
| [http://tomasp.net](http://tomasp.net)
| [http://fsharpworks.com](http://fsharpworks.com)

---------------------------------------------------------------------------------------------------

# open-source = community + code

***************************************************************************************************

# History of F# and open-source

<br /><br />
<img src="images/heart.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

---------------------------------------------------------------------------------------------------

## F#, before it was cool! _Before 2010_

<img src="images/vintage.png" style="width:700px;border-style:none;" alt="F# Foundation" />

---------------------------------------------------------------------------------------------------

## How to get 1500 followers in 10 minutes?

<div class="fragment">
<blockquote class="twitter-tweet" lang="en"><p>Guys, Don Syme is on twitter
<a href="https://twitter.com/dsyme">@dsyme</a> he is about to announce something huge. Everyone
follow!</p>&mdash; Miguel de Icaza (@migueldeicaza)
<a href="https://twitter.com/migueldeicaza/status/347721978351616">November 5, 2010</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
</div>

---------------------------------------------------------------------------------------------------

## F# Goes Open Source _11 November 2010_

![F# Goes Open Source](images/donsyme-oss.jpg)

---------------------------------------------------------------------------------------------------

## The birth of the F# Foundation _October 2012_

<img src="images/fsharporg.png" style="width:600px;border-style:none;" alt="F# Foundation" />

---------------------------------------------------------------------------------------------------

## Contributing to Visual F# _14 May 2014_

<blockquote class="twitter-tweet" data-cards="hidden" lang="en"><p>Visual FSharp Tools is accepting
community contributions to the FSharp compiler, FSI and core libraries:
<a href="http://t.co/INDJWPZYUN">http://t.co/INDJWPZYUN</a> <a href="https://twitter.com/hashtag/fsharp?src=hash">#fsharp</a></p>&mdash;
Visual F# Team (@VisualFSharp) <a href="https://twitter.com/VisualFSharp/status/466633178556661760">May 14, 2014</a></blockquote>
<script asyncsrc="//platform.twitter.com/widgets.js" charset="utf-8"></script>

---------------------------------------------------------------------------------------------------

## Moving to GitHub _13 January 2015_

<blockquote class="twitter-tweet" lang="en"><p>The day has arrived: Visual <a href="https://twitter.com/hashtag/fsharp?src=hash">#fsharp</a>
has moved to Github:<a href="https://t.co/HbVqEmAeGo">https://t.co/HbVqEmAeGo</a> Blog: <a href="http://t.co/bsABvO6epC">http://t.co/bsABvO6epC</a>
<a href="http://t.co/IxLtndj15y">pic.twitter.com/IxLtndj15y</a></p>&mdash; Visual F# Team (@VisualFSharp)
<a href="https://twitter.com/VisualFSharp/status/555113681038491649">January 13, 2015</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

***************************************************************************************************

# (Some of the)<br /> F# community projects

<br /><br />
<img src="images/heart.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

---------------------------------------------------------------------------------------------------

## Some F# community projects

<img src="images/diagrams/s1.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Some F# community projects

<img src="images/diagrams/s2.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Some F# community projects

<img src="images/diagrams/s3.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Some F# community projects

 - Important "enabler" projects
 - Trust in other OSS projects
 - Compositional library design

---------------------------------------------------------------------------------------------------

## Why people trust other OSS projects?

 - Community that builds things!
 - Documentation, tests, builds...
 - Common structure & common style

***************************************************************************************************

# Slow development methodology

<br /><br />
<img src="images/heart.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

---------------------------------------------------------------------------------------------------

## Slow development methodology

<br />
<img src="images/formatting.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Slow development methodology

+------------------------------+-----------+
| F# Snippets web site         | Sept 2010 |
+------------------------------+-----------+
| Markdown parser (TryJoinads) | Jan 2012  |
+------------------------------+-----------+
| F# Data documentation        | Jan 2013  |
+------------------------------+-----------+
| Deedle + ProjectScaffold     | Oct 2013  |
+------------------------------+-----------+
| FsLab Journal                | Apr 2014  |
+------------------------------+-----------+
| FsReveal project             | Jul 2014  |
+------------------------------+-----------+

---------------------------------------------------------------------------------------------------

## Slow development methodology

 - Start with experiments
   - Share & keep them for later
 - Wrap useful things in libraries
   - Add docs & make them useable
 - Follow a theme, not a specific goal
   - Figure out what you do along the way

---------------------------------------------------------------------------------------------------

## Libraries and frameworks

<img src="images/diagrams/s4.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Libraries and frameworks

<img src="images/diagrams/s5.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Libraries and frameworks

<img src="images/libfwk.png" style="border-style:none;background:transparent;" />

***************************************************************************************************

# Interesting integration points

<br /><br />
<img src="images/heart.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

---------------------------------------------------------------------------------------------------

## Atom editor bindings

<img src="images/diagrams/s3.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Atom editor bindings

<img src="images/diagrams/s6.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Calling `fsautocomplete` from Atom

    type T = { State:State; Child:ChildProcess option }

    let location () =
        Globals.atom.packages.packageDirPaths.[0]
        + "/core/bin/fsautocomplete.exe"

    let start t =
        let child =  location () |> Globals.spawn
        { t with State = State.On; Child = Some child }

    let send msg t =
        t.Child |> Option.iter (fun c ->
            c.stdin.write msg |> ignore)

<div style="height:0px"></div>

    let parse path text cb service =
        let str = "parse \"" + path + "\"\n" + text + "\n<<EOF>>\n"
        service |> AutocompleteService.send str

---------------------------------------------------------------------------------------------------

## Generating Atom bindings with FunScript

    // We generate F# quotation that returns all the
    // methods that we want to expose from the class...
    let ctor = typ.GetConstructor([||])
    let meths = typ.GetMethods((*[omit:...]*)BindingFlags.DeclaredOnly ||| BindingFlags.Public ||| BindingFlags.Instance(*[/omit]*))
    (*[omit:(Wrap functions using quotations)]*)
    /// Creates "(fun p1 .. pn -> <body>)" and "[p1; ..; pn]"
    /// (which is used when generating boxed lambdas that pass parameters to the actual function)
    let createParameterPassing (m:MethodBase) =
      let paramVars = m.GetParameters() |> Array.mapi (fun i p -> Var(sprintf "p%d" i, p.ParameterType))
      let paramArgs = [ for v in paramVars -> Expr.Var(v) ]
      let lambdaConstr = paramVars |> Seq.fold (fun fn var -> fun body -> Expr.Lambda(var, fn body)) id
      lambdaConstr, paramArgs

    let exportFunctions =
      [ for m in meths ->
          let tv = new Var("this", typ)
          let lambdaConstr, paramArgs = createParameterPassing m
          Expr.Lambda(tv, lambdaConstr (Expr.Call(Expr.Var(tv), m, paramArgs))) ]

    let exportCtor =
      Expr.Coerce
        ( Expr.Lambda(Var("ign", typeof<unit>), Expr.NewObject(typ.GetConstructor [||], [])),
          typeof<obj> )
    let funcs = [ for f in exportFunctions -> Expr.Coerce(f, typeof<obj>)]
    (*[/omit]*)
    let functionArray = Expr.NewArray(typeof<obj>, exportCtor::funcs)

<div style="height:0px"></div>

    // Call FunScript to do the translation!
    let coreJS = Compiler.Compile(functionArray)

<div style="height:0px"></div>

    // Now we just wrap the generated JavaScript...
    let moduleJS =
      [ yield "var CompositeDisposable = require('atom').CompositeDisposable;"
        (*[omit:(Some more JavaScript)]*)
        yield "var child_process = require('child_process');"
        yield "window.$ = require('jquery');"
        yield ""
        yield "function wrappedFunScript() { \n" + coreJS + "\n }"
        yield "var _funcs = wrappedFunScript();"
        yield "var _self = _funcs[0]();"
        yield ""
        (*[/omit]*)
        yield "module.exports = " + moduleName + " = {"
        for i, m in Seq.zip [1 .. meths.Length] meths do
          (*[omit:Helper bindings]*)let parNames = String.concat "" [ for j in 1 .. m.GetParameters().Length -> sprintf "p%i" j ]
          let parArgs = String.concat "" [ for j in 1 .. m.GetParameters().Length -> sprintf "(p%i)" j ](*[/omit]*)
          yield m.Name + ": function(" + parNames + ") {"
          yield "  return _funcs[" + string i + "](_self)" + parArgs + "; }" +
                 ( if i = meths.Length then "" else "," )
        yield "};" ]

---------------------------------------------------------------------------------------------------

## FsLab Journal runner

<img src="images/diagrams/s3.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## FsLab Journal runner

<img src="images/diagrams/s7.png" style="border-style:none;background:transparent;" />

---------------------------------------------------------------------------------------------------

## Improving library interop

    [<AutoOpen>]
    module FSharpChartingExtensions =
      type FSharp.Charting.Chart with
        static member Line(data:Series<'K, 'V>, ?Name,
            ?Title, ?Labels, ?Color, ?XTitle, ?YTitle) =
          Chart.Line(Series.observations data, ?Name=Name, ?Title=Title,
            ?Labels=Labels, ?Color=Color, ?XTitle=XTitle, ?YTitle=YTitle)

<div style="height:0px"></div>

    module Frame =
      let inline toMatrix frame =
        frame |> Frame.toArray2D |> DenseMatrix.ofArray2
    module Matrix =
      let inline toFrame matrix =
        matrix |> Matrix.toArray2 |> Frame.ofArray2D


---------------------------------------------------------------------------------------------------

## Evaluating snippets in Journal

    /// Builds FSI evaluator that can render
    /// System.Image, F# Charts, series & frames
    let createFsiEvaluator root output failedHandler =
      let transformation (value:obj, typ:System.Type) =
        match value with
        | :? ChartTypes.GenericChart as ch ->
            ( use ctl = new ChartControl((*[omit:...]*)chartStyle ch, Dock = DockStyle.Fill, Width=500, Height=300(*[/omit]*))
              ch.CopyAsBitmap().Save(output @@ "images" @@ file) )
            Some [ Paragraph [DirectImage ("", (root + "/images/" + file, None))]  ]
        | SeriesValues s ->
            let heads, row, aligns = (*[omit:...]*)
               s |> mapSteps sitms fst (function Some k -> td (k.ToString()) | _ -> td " ... "),
               s |> mapSteps sitms snd (function Some v -> formatValue floatFormat "N/A" (OptionalValue.asOption v) | _ -> td " ... "),
               s |> mapSteps sitms id (*[omit:...]*)(fun _ -> AlignDefault)(*[/omit]*)
            [ InlineMultiformatBlock("<div class=\"deedleseries\">", "\\vspace{1em}")
              TableBlock(Some ((td "Keys")::heads),
                         AlignDefault::aligns, [ (td "Values")::row ])
              InlineMultiformatBlock("</div>","\\vspace{1em}") ] |> Some
        (*[omit:(Format Deedle frames, Math.NET Matrices etc.)]*)
        | :? IFrame as f ->
          // Pretty print frame!
          {new IFrameOperation<_> with
            member x.Invoke(f) =
              let heads  = f.ColumnKeys |> mapSteps fcols id (function Some k -> td (k.ToString()) | _ -> td " ... ")
              let aligns = f.ColumnKeys |> mapSteps fcols id (fun _ -> AlignDefault)
              let rows =
                f.Rows |> Series.observationsAll |> mapSteps frows id (fun item ->
                  let def, k, data =
                    match item with
                    | Some(k, Some d) -> "N/A", k.ToString(), Series.observationsAll d |> Seq.map snd
                    | Some(k, _) -> "N/A", k.ToString(), f.ColumnKeys |> Seq.map (fun _ -> None)
                    | None -> " ... ", " ... ", f.ColumnKeys |> Seq.map (fun _ -> None)
                  let row = data |> mapSteps fcols id (function Some v -> formatValue floatFormat def v | _ -> td " ... ")
                  (td k)::row )
              Some [
                InlineMultiformatBlock("<div class=\"deedleframe\">","\\vspace{1em}")
                TableBlock(Some ([]::heads), AlignDefault::aligns, rows)
                InlineMultiformatBlock("</div>","\\vspace{1em}")
              ] }
          |> f.Apply

        | :? Matrix<float> as m -> Some [ MathDisplay (m |> formatMatrix (formatMathValue floatFormat)) ]
        | :? Matrix<float32> as m -> Some [ MathDisplay (m |> formatMatrix (formatMathValue floatFormat)) ]
        | :? Vector<float> as v -> Some [ MathDisplay (v |> formatVector (formatMathValue floatFormat)) ]
        | :? Vector<float32> as v -> Some [ MathDisplay (v |> formatVector (formatMathValue floatFormat)) ](*[/omit]*)
        | _ -> None

      // Create FSI evaluator, register transformations & return
      let fsiEvaluator = FsiEvaluator()
      fsiEvaluator.RegisterTransformation(transformation)
      fsiEvaluator

***************************************************************************************************

# Conclusions

<br /><br />
<img src="images/heart.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br /><br />

---------------------------------------------------------------------------------------------------

# F# <img src="images/heart-pink.png" style="width:200px;position:relative;top:85px;border-style:none;background:transparent;" /> open-source!

<br />
<br />

---------------------------------------------------------------------------------------------------

### Open-source is an accepted model

 - Core libraries are open-source
 - Same structure & style helps

### Slow development methodology

 - Very compositional design
 - Scripts to prototypes to libraries
 - Figure thing out as you go

---------------------------------------------------------------------------------------------------

# Thank you!

<br />
<img src="images/heart-pink.png" style="width:200px;border-style:none;background:transparent;" />
<br /><br /><br />

**Tomas Petricek**, F# Works  
[@tomaspetricek](http://twitter.com/tomaspetricek)
| [http://tomasp.net](http://tomasp.net)
| [http://fsharpworks.com](http://fsharpworks.com)

---------------------------------------------------------------------------------------------------
