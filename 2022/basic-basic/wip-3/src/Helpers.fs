module Basic.Helpers
open Browser.Dom

let awaitObservable (obs:System.IObservable<'T>) = 
  Async.FromContinuations(fun (cont, _, _) ->
    let mutable sub : System.IDisposable option = None
    sub <- Some <| obs.Subscribe(fun v -> 
      sub.Value.Dispose()
      cont v ) )

let input = 
  let inputEl = document.getElementById("in") :?> Browser.Types.HTMLInputElement
  let inputEvt = Event<string>()
  inputEl.onkeypress <- fun ke -> 
    if ke.code = "Enter" then 
      inputEvt.Trigger(inputEl.value)
      inputEl.value <- ""
  inputEvt.Publish
  
let print s = 
  document.getElementById("out").innerText <- 
    document.getElementById("out").innerText + s + "\n"
