// --------------------------------------------------------------------------------------
// A simple FAKE build script that:
//  1) Hosts Suave server locally & reloads web part that is defined in 'app.fsx'
//  2) Deploys the web application to Azure web sites when called with 'build deploy'
// --------------------------------------------------------------------------------------

#r "packages/FAKE/tools/FakeLib.dll"
open Fake

open System
open System.IO
open System.Diagnostics

// --------------------------------------------------------------------------------------
// Watch the current directory for changes
// --------------------------------------------------------------------------------------

Target "artifact" (fun _ ->
  let path = __SOURCE_DIRECTORY__
  let fsw = new FileSystemWatcher(path,"*.tex")
  fsw.Changed.Add(fun _ ->
    fsw.EnableRaisingEvents <- false
    let ps =
      ProcessStartInfo
        ( FileName = @"C:\Programs\Publishing\MiKTeX 2.9\miktex\bin\x64\pdflatex.exe",
          Arguments = "-interaction=nonstopmode artifact.tex",
          WorkingDirectory = path,
          UseShellExecute = false,
          CreateNoWindow = true )
    let p = Process.Start(ps)
    p.WaitForExit()
    fsw.EnableRaisingEvents <- true )

  fsw.EnableRaisingEvents <- true

  let ps =
    ProcessStartInfo
      ( FileName = @"C:\Programs\Publishing\SumatraPDF\SumatraPDF.exe",
        Arguments = "\"" + __SOURCE_DIRECTORY__ + "\\artifact.pdf\"",
        WorkingDirectory = path )
  let p = Process.Start(ps)  
  System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite)
)

Target "run" (fun _ ->
  let path = __SOURCE_DIRECTORY__
  let fsw = new FileSystemWatcher(path,"*.tex")
  fsw.Changed.Add(fun _ ->
    fsw.EnableRaisingEvents <- false
    let ps =
      ProcessStartInfo
        ( FileName = @"C:\Programs\Publishing\MiKTeX 2.9\miktex\bin\x64\pdflatex.exe",
          Arguments = "-interaction=nonstopmode paper.tex",
          WorkingDirectory = path,
          UseShellExecute = false,
          CreateNoWindow = true )
    let p = Process.Start(ps)
    p.WaitForExit()
    fsw.EnableRaisingEvents <- true )

  fsw.EnableRaisingEvents <- true

  let ps =
    ProcessStartInfo
      ( FileName = @"C:\Programs\Publishing\SumatraPDF\SumatraPDF.exe",
        Arguments = "\"" + __SOURCE_DIRECTORY__ + "\\paper.pdf\"",
        WorkingDirectory = path )
  let p = Process.Start(ps)  
  System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite)
)

// --------------------------------------------------------------------------------------
// Targets for running build script in background (for Atom)
// --------------------------------------------------------------------------------------

open System.Diagnostics

let runningFileLog = __SOURCE_DIRECTORY__ @@ "build.log"
let runningFile = __SOURCE_DIRECTORY__ @@ "build.running"

Target "spawn" (fun _ ->
  if File.Exists(runningFile) then
    failwith "The build is already running!"

  let ps =
    ProcessStartInfo
      ( WorkingDirectory = __SOURCE_DIRECTORY__,
        FileName = __SOURCE_DIRECTORY__  @@ "packages/FAKE/tools/FAKE.exe",
        Arguments = "run --fsiargs build.fsx",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false )
  use fs = new FileStream(runningFileLog, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)
  use sw = new StreamWriter(fs)
  let p = Process.Start(ps)
  p.ErrorDataReceived.Add(fun data -> printfn "%s" data.Data; sw.WriteLine(data.Data); sw.Flush())
  p.OutputDataReceived.Add(fun data -> printfn "%s" data.Data; sw.WriteLine(data.Data); sw.Flush())
  p.EnableRaisingEvents <- true
  p.BeginOutputReadLine()
  p.BeginErrorReadLine()

  File.WriteAllText(runningFile, string p.Id)
  while File.Exists(runningFile) do
    System.Threading.Thread.Sleep(500)
  p.Kill()
)

Target "attach" (fun _ ->
  if not (File.Exists(runningFile)) then
    failwith "The build is not running!"
  use fs = new FileStream(runningFileLog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
  use sr = new StreamReader(fs)
  while File.Exists(runningFile) do
    let msg = sr.ReadLine()
    if not (String.IsNullOrEmpty(msg)) then
      printfn "%s" msg
    else System.Threading.Thread.Sleep(500)
)

Target "stop" (fun _ ->
  if not (File.Exists(runningFile)) then
    failwith "The build is not running!"
  File.Delete(runningFile)
)

RunTargetOrDefault "run"
