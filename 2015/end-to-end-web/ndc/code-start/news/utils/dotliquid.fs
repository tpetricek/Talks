module Suave.DotLiquid

open System
open System.IO
open DotLiquid
open Microsoft.FSharp.Reflection
open Suave.Utils
open Suave.Types
open Suave.Http
open Suave.Http.Files

// -------------------------------------------------------------------------------------------------
// Registering things with DotLiquid
// -------------------------------------------------------------------------------------------------

let private localFileSystem root =
  { new DotLiquid.FileSystems.IFileSystem with
      member this.ReadTemplateFile(context, templateName) =
        let templatePath = context.[templateName] :?> string
        let fullPath = Path.Combine(root, templatePath + ".html")
        if not (File.Exists(fullPath)) then failwithf "File not found: %s" fullPath
        File.ReadAllText(fullPath) }

let private safe =
  let o = obj()
  fun f -> lock o f

let private registerTypeTree ty =
  let rec loop ty =
    if FSharpType.IsRecord ty then
      let fields = FSharpType.GetRecordFields(ty)
      Template.RegisterSafeType(ty, [| for f in fields -> f.Name |])
      for f in fields do loop f.PropertyType
    elif ty.IsGenericType &&
        ( let t = ty.GetGenericTypeDefinition()
          in t = typedefof<seq<_>> || t = typedefof<list<_>> ) then
      loop (ty.GetGenericArguments().[0])
  safe (fun () -> loop ty)

Template.NamingConvention <- DotLiquid.NamingConventions.CSharpNamingConvention()

// -------------------------------------------------------------------------------------------------
// Parsing and loading DotLiquid templates and caching the results
// -------------------------------------------------------------------------------------------------

let private asyncMemoize isValid f =
  let cache = Collections.Concurrent.ConcurrentDictionary<_ , _>()
  fun x -> async {
      match cache.TryGetValue(x) with
      | true, res when isValid x res -> return res
      | _ ->
          let! res = f x
          cache.[x] <- res
          return res }

let private parseTemplate template ty =
  registerTypeTree ty
  let t = Template.Parse(template)
  fun k v -> t.Render(Hash.FromDictionary(dict [k, v]))

let private loadTemplate (ty, fileName) = async {
  let writeTime = File.GetLastWriteTime(fileName)
  use file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
  use reader = new StreamReader(file)
  let razorTemplate = reader.ReadToEnd()
  return writeTime, parseTemplate razorTemplate ty }

let private loadTemplateCached =
  loadTemplate |> asyncMemoize (fun (_, templatePath) (lastWrite, _) ->
    File.GetLastWriteTime(templatePath) <= lastWrite )

// -------------------------------------------------------------------------------------------------
// Public API
// -------------------------------------------------------------------------------------------------

let mutable private templatesDir = None

let setTemplatesDir dir =
  templatesDir <- Some dir
  safe (fun () ->
    Template.FileSystem <- localFileSystem dir)

let page<'T> path (model : 'T) r = async {
  let path =
    match templatesDir with
    | None -> Path.Combine(__SOURCE_DIRECTORY__, "../templates", path)
    | Some root -> Path.Combine(root, path)
  let! writeTime, renderer = loadTemplateCached (typeof<'T>, path)
  let content = renderer "model" (box model)
  return! Response.response HTTP_200 (UTF8.bytes content) r }

let registerFiltersByName name =
  let asm = System.Reflection.Assembly.GetExecutingAssembly()
  let typ =
    [ for t in asm.GetTypes() do
        if t.FullName.EndsWith(name) && not(t.FullName.Contains("<StartupCode")) then yield t ]
    |> Seq.last
  Template.RegisterFilter(typ)

let registerFiltersByType typ =
  Template.RegisterFilter(typ)
