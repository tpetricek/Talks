module Helpers

let awaitObservable (obs:System.IObservable<'T>) = 
  Async.FromContinuations(fun (cont, _, _) ->
    let mutable sub : System.IDisposable option = None
    sub <- Some <| obs.Subscribe(fun v -> 
      sub.Value.Dispose()
      cont v ) )

