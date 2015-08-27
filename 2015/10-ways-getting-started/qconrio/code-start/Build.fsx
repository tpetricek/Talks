#I "packages/FAKE/tools"
#r "packages/FAKE/tools/FakeLib.dll"
open System
open Fake 
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let release = parseReleaseNotes (IO.File.ReadAllLines "RELEASE_NOTES.md")

Target "Clean" (fun _ ->
  CleanDirs ["Calculator/bin"; "Calculator.Tests/bin" ]
)

Target "AssemblyInfo" (fun _ ->
  let fileName = __SOURCE_DIRECTORY__ @@ "Calculator/Properties/AssemblyInfo.cs"
  CreateCSharpAssemblyInfo fileName
    [ Attribute.Title "Demo"
      Attribute.Product "Demo"
      Attribute.Description "This is just a demo"
      Attribute.Version release.AssemblyVersion
      Attribute.FileVersion release.AssemblyVersion] 
)

Target "Build" (fun _ ->
  !! "Calculator/Calculator.Library.csproj" 
  |> MSBuildRelease "" "Rebuild" |> Log "AppBuild-Output: "

  !! "Calculator.Tests/Calculator.Tests.fsproj" 
  |> MSBuildRelease "" "Rebuild" |> Log "AppBuild-Output: "
)


Target "RunTests" (fun _ ->
  !! "Calculator.Tests/bin/Release/*Tests.dll"
  |> NUnit (fun p ->
    { p with
        DisableShadowCopy = true
        TimeOut = TimeSpan.FromMinutes 20.
        OutputFile = "TestResults.xml" })
)

Target "All" DoNothing

"Clean"
  ==> "AssemblyInfo"
  ==> "Build"
  ==> "RunTests"
  ==> "All" 

RunTargetOrDefault "All"