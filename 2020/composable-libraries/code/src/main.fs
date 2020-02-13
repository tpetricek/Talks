module Demo.Main

open Compost
open Compost.Html
open Demo.Helpers
open Fable.Import.Browser

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

// ----------------------------------------------------------------------------
// DEMO #1: Creating a bar chart
// ----------------------------------------------------------------------------

// TODO: 'Shape.Layered' of 'Derived.Column' (ca, co) 
// TODO: Add 'Derived.FillColor' and 'Shape.Padding'
// TODO: Add 'Shape.Axes'





// ----------------------------------------------------------------------------
// DEMO #2: Creating a colored line chart
// ----------------------------------------------------------------------------

// TODO: Create a line chart using 'Seq.indexed gbpusd' (numv i, numv v)
// TODO: Use 'Derived.StrokeColor' to make it black
// TODO: Add a 'Shape' (numv 0, numv 1) --> (numv 16, numv 1.8))
// TODO: Make it #aec7e8 using 'Derived.FillColor' 
// DEMO: Add second background box
// DEMO: Add axes using 'Shape.Axes(f, t, t, t, body)'

// DEMO: Create 'title' chart element
// TODO: Align with chart using OuterScale (Some(Continuous(co 0, co 100))





// ----------------------------------------------------------------------------
// DEMO #3: Refactoring
// ----------------------------------------------------------------------------

// TODO: Extract the 'Title' function




// ----------------------------------------------------------------------------
// DEMO #4: Creating a colored line chart
// ----------------------------------------------------------------------------

// TODO: Render simple barchart using 'renderAnim state render update' & 'svg'
// TODO: Define Start and Anim events, 'update' function and add button
// TODO: Scale data based on state, trigger timeout
