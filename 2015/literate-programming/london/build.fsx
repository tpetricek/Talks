#I @"packages/FsReveal/fsreveal/"
#I @"packages/FAKE/tools/"
#I @"packages/Suave/lib/net40"

#r "FakeLib.dll"
#r "suave.dll"

#load "fsreveal.fsx"

// --------------------------------------------------------------------------------------
// Custom FSI Evaluator fun
// --------------------------------------------------------------------------------------

open Fake
open FSharp.Literate
open FSharp.Markdown

#load "FsiMock.fs"
#load "packages/FsLab/FsLab.fsx"
#load "Formatters.fs"
let fsiEvaluator1 = FsiEvaluator() 
let fsiEvaluator = FsLab.Formatters.wrapFsiEvaluator fsiEvaluator1 "." (System.IO.Path.Combine(__SOURCE_DIRECTORY__,"output")) "G4"

let transformation (value:obj, typ:System.Type) : MarkdownParagraph list option =
  let toHtml = typ.GetMethod("ToHtml") 
  if toHtml <> null then
    let html = toHtml.Invoke(value, [||]) :?> string
    Some [ MarkdownParagraph.InlineBlock html ]
  else None

fsiEvaluator1.RegisterTransformation(transformation)
fsiEvaluator1.EvaluationFailed.Add(fun e ->
  traceImportant <| sprintf "Evaluation failed: %s" e.StdErr
)

// --------------------------------------------------------------------------------------

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted
let gitOwner = "myGitUser"
let gitHome = "https://github.com/" + gitOwner
// The name of the project on GitHub
let gitProjectName = "MyProject"

open FsReveal
open Fake
open Fake.Git
open System.IO
open System.Diagnostics
open Suave
open Suave.Web
open Suave.Http
open Suave.Http.Files

let outDir = __SOURCE_DIRECTORY__ @@ "output"
let slidesDir = __SOURCE_DIRECTORY__ @@ "slides"

Target "Clean" (fun _ ->
    CleanDirs [outDir]
)

//let fsiEvaluator = 
//    let evaluator = FSharp.Literate.FsiEvaluator()
//    evaluator.EvaluationFailed.Add(fun err -> 
//        traceImportant <| sprintf "Evaluating F# snippet failed:\n%s\nThe snippet evaluated:\n%s" err.StdErr err.Text )
//    evaluator 

let copyStylesheet() =
    try
        CopyFile (outDir @@ "css" @@ "custom.css") (slidesDir @@ "custom.css")
    with
    | exn -> traceImportant <| sprintf "Could not copy stylesheet: %s" exn.Message

let copyPics() =
    try
      !! (slidesDir @@ "images/*.*")
      |> CopyFiles (outDir @@ "images")
    with
    | exn -> traceImportant <| sprintf "Could not copy picture: %s" exn.Message    

let generateFor (file:FileInfo) = 
    try
        copyPics()
        let rec tryGenerate trials =
            try
                FsReveal.GenerateFromFile(file.FullName, outDir, fsiEvaluator = fsiEvaluator)
            with 
            | exn when trials > 0 -> tryGenerate (trials - 1)
            | exn -> 
                traceImportant <| sprintf "Could not generate slides for: %s" file.FullName
                traceImportant exn.Message

        tryGenerate 3

        copyStylesheet()
    with
    | :? FileNotFoundException as exn ->
        traceImportant <| sprintf "Could not copy file: %s" exn.FileName

let handleWatcherEvents (e:FileSystemEventArgs) =
    let fi = fileInfo e.FullPath 
    traceImportant <| sprintf "%s was changed." fi.Name
    match fi.Attributes.HasFlag FileAttributes.Hidden || fi.Attributes.HasFlag FileAttributes.Directory with
            | true -> ()
            | _ -> generateFor fi

let startWebServer () =
    let serverConfig = 
        { defaultConfig with
           homeFolder = Some (FullName outDir)
        }
    let app =
        Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
        >>= Writers.setHeader "Pragma" "no-cache"
        >>= Writers.setHeader "Expires" "0"
        >>= browseHome
    startWebServerAsync serverConfig app |> snd |> Async.Start
    Process.Start "http://localhost:8083/talk.html" |> ignore

Target "GenerateSlides" (fun _ ->
    // Overwrite things that need to be fixed...
    (slidesDir @@ "../special/FsLab.fsx")
    
    |> CopyFile (slidesDir @@ "../packages/FsLab/FsLab.fsx")

    (slidesDir @@ "template.html")
    
    |> CopyFile (slidesDir @@ "../packages/FsReveal/fsreveal/template.html")

    

    !! (slidesDir @@ "*.md")
      ++ (slidesDir @@ "*.fsx")
    |> Seq.map fileInfo
    |> Seq.iter generateFor
)

Target "KeepRunning" (fun _ ->
    use watcher = new FileSystemWatcher(FullName slidesDir,"*.*")
    watcher.EnableRaisingEvents <- true
    watcher.IncludeSubdirectories <- true
    watcher.Changed.Add(handleWatcherEvents)
    watcher.Created.Add(handleWatcherEvents)
    watcher.Renamed.Add(handleWatcherEvents)

    startWebServer ()

    traceImportant "Waiting for slide edits. Press any key to stop."

    System.Console.ReadKey() |> ignore

    watcher.EnableRaisingEvents <- false
    watcher.Dispose()
)

Target "ReleaseSlides" (fun _ ->
    if gitOwner = "myGitUser" || gitProjectName = "MyProject" then
        failwith "You need to specify the gitOwner and gitProjectName in build.fsx"
    let tempDocsDir = __SOURCE_DIRECTORY__ @@ "temp/gh-pages"
    CleanDir tempDocsDir
    Repository.cloneSingleBranch "" (gitHome + "/" + gitProjectName + ".git") "gh-pages" tempDocsDir

    fullclean tempDocsDir
    CopyRecursive outDir tempDocsDir true |> tracefn "%A"
    StageAll tempDocsDir
    Git.Commit.Commit tempDocsDir "Update generated slides"
    Branches.push tempDocsDir
)

"Clean"
  ==> "GenerateSlides"
  ==> "KeepRunning"

"GenerateSlides"
  ==> "ReleaseSlides"
  
RunTargetOrDefault "KeepRunning"