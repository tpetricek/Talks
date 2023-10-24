module Demo.Main

open Compost
open Compost.Html
open Demo.Helpers
open Browser

// ----------------------------------------------------------------------------
// Some *fun* input data about British politics
// ----------------------------------------------------------------------------

let elections = 
  [ "Conservative", "#1F77B4", 317, 365; "Labour", "#D62728", 262, 202; 
    "Liberal Democrats", "#FF7F0E", 12, 11; "SNP", "#BCBD22", 35, 48; 
    "Green", "#2CA02C", 1, 1; "DUP", "#8C564B", 10, 8 ]

let gbpusd = 
  [ 1.4414; 1.4447; 1.4517; 1.4464; 1.456; 1.4552; 1.4465; 1.4334; 1.4282; 
    1.4114; 1.4196; 1.4064; 1.4293; 1.4694; 1.467; 1.4687; 1.4798; 1.3621; 
    1.3152; 1.3322; 1.3523; 1.3429; 1.327; 1.3287; 1.3048; 1.2885; 1.2932; 
    1.296; 1.2987; 1.3186; 1.3215; 1.3323; 1.3231; 1.3286; 1.3136; 1.3175; 
    1.3211; 1.3085; 1.3122; 1.3116; 1.3114; 1.312; 1.3267 ]

let mtcars = [
  {| model="MazdaRX4"; mpg=float 21; cyl=float 6; disp=float 160; hp=float 110; drat=float 3.9; wt=float 2.62; qsec=float 16.46; vs=float 0; am=float 1; gear=float 4; carb=float 4 |}
  {| model="MazdaRX4Wag"; mpg=float 21; cyl=float 6; disp=float 160; hp=float 110; drat=float 3.9; wt=float 2.875; qsec=float 17.02; vs=float 0; am=float 1; gear=float 4; carb=float 4 |}
  {| model="Datsun710"; mpg=float 22.8; cyl=float 4; disp=float 108; hp=float 93; drat=float 3.85; wt=float 2.32; qsec=float 18.61; vs=float 1; am=float 1; gear=float 4; carb=float 1 |}
  {| model="Hornet4Drive"; mpg=float 21.4; cyl=float 6; disp=float 258; hp=float 110; drat=float 3.08; wt=float 3.215; qsec=float 19.44; vs=float 1; am=float 0; gear=float 3; carb=float 1 |}
  {| model="HornetSportabout"; mpg=float 18.7; cyl=float 8; disp=float 360; hp=float 175; drat=float 3.15; wt=float 3.44; qsec=float 17.02; vs=float 0; am=float 0; gear=float 3; carb=float 2 |}
  {| model="Valiant"; mpg=float 18.1; cyl=float 6; disp=float 225; hp=float 105; drat=float 2.76; wt=float 3.46; qsec=float 20.22; vs=float 1; am=float 0; gear=float 3; carb=float 1 |}
  {| model="Duster360"; mpg=float 14.3; cyl=float 8; disp=float 360; hp=float 245; drat=float 3.21; wt=float 3.57; qsec=float 15.84; vs=float 0; am=float 0; gear=float 3; carb=float 4 |}
  {| model="Merc240D"; mpg=float 24.4; cyl=float 4; disp=float 146.7; hp=float 62; drat=float 3.69; wt=float 3.19; qsec=float 20; vs=float 1; am=float 0; gear=float 4; carb=float 2 |}
  {| model="Merc230"; mpg=float 22.8; cyl=float 4; disp=float 140.8; hp=float 95; drat=float 3.92; wt=float 3.15; qsec=float 22.9; vs=float 1; am=float 0; gear=float 4; carb=float 2 |}
  {| model="Merc280"; mpg=float 19.2; cyl=float 6; disp=float 167.6; hp=float 123; drat=float 3.92; wt=float 3.44; qsec=float 18.3; vs=float 1; am=float 0; gear=float 4; carb=float 4 |}
  {| model="Merc280C"; mpg=float 17.8; cyl=float 6; disp=float 167.6; hp=float 123; drat=float 3.92; wt=float 3.44; qsec=float 18.9; vs=float 1; am=float 0; gear=float 4; carb=float 4 |}
  {| model="Merc450SE"; mpg=float 16.4; cyl=float 8; disp=float 275.8; hp=float 180; drat=float 3.07; wt=float 4.07; qsec=float 17.4; vs=float 0; am=float 0; gear=float 3; carb=float 3 |}
  {| model="Merc450SL"; mpg=float 17.3; cyl=float 8; disp=float 275.8; hp=float 180; drat=float 3.07; wt=float 3.73; qsec=float 17.6; vs=float 0; am=float 0; gear=float 3; carb=float 3 |}
  {| model="Merc450SLC"; mpg=float 15.2; cyl=float 8; disp=float 275.8; hp=float 180; drat=float 3.07; wt=float 3.78; qsec=float 18; vs=float 0; am=float 0; gear=float 3; carb=float 3 |}
  {| model="CadillacFleetwood"; mpg=float 10.4; cyl=float 8; disp=float 472; hp=float 205; drat=float 2.93; wt=float 5.25; qsec=float 17.98; vs=float 0; am=float 0; gear=float 3; carb=float 4 |}
  {| model="LincolnContinental"; mpg=float 10.4; cyl=float 8; disp=float 460; hp=float 215; drat=float 3; wt=float 5.424; qsec=float 17.82; vs=float 0; am=float 0; gear=float 3; carb=float 4 |}
  {| model="ChryslerImperial"; mpg=float 14.7; cyl=float 8; disp=float 440; hp=float 230; drat=float 3.23; wt=float 5.345; qsec=float 17.42; vs=float 0; am=float 0; gear=float 3; carb=float 4 |}
  {| model="Fiat128"; mpg=float 32.4; cyl=float 4; disp=float 78.7; hp=float 66; drat=float 4.08; wt=float 2.2; qsec=float 19.47; vs=float 1; am=float 1; gear=float 4; carb=float 1 |}
  {| model="HondaCivic"; mpg=float 30.4; cyl=float 4; disp=float 75.7; hp=float 52; drat=float 4.93; wt=float 1.615; qsec=float 18.52; vs=float 1; am=float 1; gear=float 4; carb=float 2 |}
  {| model="ToyotaCorolla"; mpg=float 33.9; cyl=float 4; disp=float 71.1; hp=float 65; drat=float 4.22; wt=float 1.835; qsec=float 19.9; vs=float 1; am=float 1; gear=float 4; carb=float 1 |}
  {| model="ToyotaCorona"; mpg=float 21.5; cyl=float 4; disp=float 120.1; hp=float 97; drat=float 3.7; wt=float 2.465; qsec=float 20.01; vs=float 1; am=float 0; gear=float 3; carb=float 1 |}
  {| model="DodgeChallenger"; mpg=float 15.5; cyl=float 8; disp=float 318; hp=float 150; drat=float 2.76; wt=float 3.52; qsec=float 16.87; vs=float 0; am=float 0; gear=float 3; carb=float 2 |}
  {| model="AMCJavelin"; mpg=float 15.2; cyl=float 8; disp=float 304; hp=float 150; drat=float 3.15; wt=float 3.435; qsec=float 17.3; vs=float 0; am=float 0; gear=float 3; carb=float 2 |}
  {| model="CamaroZ28"; mpg=float 13.3; cyl=float 8; disp=float 350; hp=float 245; drat=float 3.73; wt=float 3.84; qsec=float 15.41; vs=float 0; am=float 0; gear=float 3; carb=float 4 |}
  {| model="PontiacFirebird"; mpg=float 19.2; cyl=float 8; disp=float 400; hp=float 175; drat=float 3.08; wt=float 3.845; qsec=float 17.05; vs=float 0; am=float 0; gear=float 3; carb=float 2 |}
  {| model="FiatX1"; mpg=float 9; cyl=float 27.3; disp=float 4; hp=float 79; drat=float 66; wt=float 4.08; qsec=float 1.935; vs=float 18.9; am=float 1; gear=float 1; carb=float 1 |}
  {| model="Porsche914"; mpg=float 2; cyl=float 26; disp=float 4; hp=float 120.3; drat=float 91; wt=float 4.43; qsec=float 2.14; vs=float 16.7; am=float 0; gear=float 1; carb=float 2 |}
  {| model="LotusEuropa"; mpg=float 30.4; cyl=float 4; disp=float 95.1; hp=float 113; drat=float 3.77; wt=float 1.513; qsec=float 16.9; vs=float 1; am=float 1; gear=float 5; carb=float 2 |}
  {| model="FordPanteraL"; mpg=float 15.8; cyl=float 8; disp=float 351; hp=float 264; drat=float 4.22; wt=float 3.17; qsec=float 14.5; vs=float 0; am=float 1; gear=float 5; carb=float 4 |}
  {| model="FerrariDino"; mpg=float 19.7; cyl=float 6; disp=float 145; hp=float 175; drat=float 3.62; wt=float 2.77; qsec=float 15.5; vs=float 0; am=float 1; gear=float 5; carb=float 6 |}
  {| model="MaseratiBora"; mpg=float 15; cyl=float 8; disp=float 301; hp=float 335; drat=float 3.54; wt=float 3.57; qsec=float 14.6; vs=float 0; am=float 1; gear=float 5; carb=float 8 |}
  {| model="Volvo142E"; mpg=float 21.4; cyl=float 4; disp=float 121; hp=float 109; drat=float 4.11; wt=float 2.78; qsec=float 18.6; vs=float 1; am=float 1; gear=float 4; carb=float 2 |}
]

Shape.Layered 
  (
    mtcars 
    |> Seq.groupBy (fun c -> int c.cyl)
    |> Seq.filter (fun (_, g) -> Seq.length g > 1)
    |> Seq.indexed
    |> Seq.map (fun (i, (_, g)) -> 
      (//Shape.NestX(numv (i*10), numv ((i*10)+9),
        Shape.Axes(true, true, true, true, Shape.Layered [ for c in g -> 
          let clr = ["red";"blue";"green";"black";"yellow"].[i]
          Derived.FillColor(clr, Derived.StrokeColor("transparent", Shape.Bubble(numv c.mpg, numv c.drat, 3.0, 3.0))) ])) )
  )
|> render

//mtcars
//|> Seq.groupBy (fun c -> c.cyl)

(*

// ----------------------------------------------------------------------------
// DEMO #1: Creating a bar chart
// ----------------------------------------------------------------------------

// TODO: 'Shape.Layered' of 'Derived.Column' (ca, co) 
// TODO: Add 'Derived.FillColor' and 'Shape.Axes'

Shape.Axes(false, false, true, true, Shape.Layered
  [ for p, c, y17, y19 in elections ->
      Derived.FillColor(c, Derived.Column(ca p, co y17)) ])
|> render


// ----------------------------------------------------------------------------
// DEMO #2: Creating a colored line chart
// ----------------------------------------------------------------------------

// TODO: Create a line chart using 'Seq.indexed gbpusd' (numv i, numv v)
// TODO: Use 'Derived.StrokeColor' to make it black
// TODO: Add a 'Shape' (numv 0, numv 1) --> (numv 16, numv 1.8))
// TODO: Make it #aec7e8 using 'Derived.FillColor' 
// DEMO: Add second background box
// TODO: Add axes using 'Shape.Axes(f, t, t, t, body)'

let body =
  Shape.Axes(false, true, true, true, Shape.Layered [
    Derived.FillColor("#aec7e8", Shape.Shape [
      (numv 0, numv 1); (numv 0, numv 1.8);
      (numv 16, numv 1.8); (numv 16, numv 1)
    ])
    Derived.FillColor("#ff9896", Shape.Shape [ 
      (numv (List.length gbpusd - 1), numv 1); (numv 16, numv 1); 
      (numv 16, numv 1.8); (numv (List.length gbpusd - 1), numv 1.8) ] )
    Derived.StrokeColor("black", Shape.Line [
      for i, v in Seq.indexed gbpusd -> numv i, numv v ])
  ])

let title = 
  Shape.InnerScale
    (Some(Continuous(co 0, co 100)), Some(Continuous(co 0, co 100)),
      Derived.Font("14pt arial", "black",
        Shape.Text(numv 50, numv 50, Middle, 
          Center, 0.0, "GBP-USD exchange rate (June-July 2016)") ))

Shape.Layered [
  Shape.NestX(numv 0, numv 100, Shape.NestY(numv 90, numv 100, title))
  Shape.NestX(numv 0, numv 100, Shape.NestY(numv 0, numv 90, body))
]
|> render  




// DEMO: Create 'title' chart element
// DEMO: Align with chart using 'Shape.NestX' 


let WithTitle title body = 
  let title = 
    Shape.InnerScale
      (Some(Continuous(co 0, co 100)), Some(Continuous(co 0, co 100)),
        Derived.Font("14pt arial", "black",
          Shape.Text(numv 50, numv 50, Middle, 
            Center, 0.0, title) ))
  Shape.Layered [
    Shape.NestX(numv 0, numv 100, Shape.NestY(numv 90, numv 100, title))
    Shape.NestX(numv 0, numv 100, Shape.NestY(numv 0, numv 90, body))
  ]

Shape.Axes(false, false, true, true, Shape.Layered
  [ for p, c, y17, y19 in elections ->
      Derived.FillColor(c, Derived.Column(ca p, co y17)) ])
|> WithTitle "UK general elections (2017)"
|> render

// ----------------------------------------------------------------------------
// DEMO #3: Refactoring
// ----------------------------------------------------------------------------




// ----------------------------------------------------------------------------
// DEMO #4: Creating a colored line chart
// ----------------------------------------------------------------------------

// TODO: Render simple barchart using 'renderAnim state render update' & 'svg'
// TODO: Add 'Shape.InnerScale(None, Some(Continuous(co 0, co 400)), ...)'
// TODO: Define Start and Anim events, 'update' function and add button
// TODO: Scale data based on state, trigger timeout

let state = 0.0

type Event = 
  | Animate
  | Start

let update state e = 
  match e with 
  | Start -> 0.1
  | Animate -> state + 0.1

let render trigger state = 
  if state > 0.0 && state < 1.0 then 
    window.setTimeout((fun () -> trigger Animate), 10) |> ignore
  let data = [ for p, c, v0, v1 in elections -> 
    p, c, float v0 + float (v1 - v0) * state ]
  h?div [] [ 
    Shape.Axes(false, false, true, true, 
     Shape.InnerScale(None, Some(Continuous(co 0, co 400)),
       Shape.Layered [
         for p, clr, v in data -> 
           Derived.FillColor(clr,
             Shape.Padding((0., 10., 0., 10.), 
               Derived.Column(ca p, co v)) )
    ])) |> svg
    h?button ["click" =!> fun _ _ -> trigger Start ] [ text "Animate!"]
  ]

renderAnim state render update

*)