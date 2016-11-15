module AsyncHelpers
#nowarn "40"
open System

// --------------------------------------------------------------------------------------
// Helper to await IObservable (and properly unregister the event handler)
// --------------------------------------------------------------------------------------

/// Helper that can be used for writing CPS-style code that resumes
/// on the same thread where the operation was started.
let internal synchronize f =
  let ctx = System.Threading.SynchronizationContext.Current
  f (fun g ->
    let nctx = System.Threading.SynchronizationContext.Current
    if ctx <> null && ctx <> nctx then ctx.Post((fun _ -> g()), null)
    else g() )

type Microsoft.FSharp.Control.Async with
  /// Creates an asynchronous workflow that will be resumed when the
  /// specified observables produces a value. The workflow will return
  /// the value produced by the observable.
  static member AwaitObservable(ev1:IObservable<'T1>) =
    synchronize (fun f ->
      Async.FromContinuations((fun (cont,econt,ccont) ->
        let called = ref false
        let rec finish cont value =
          remover.Dispose()
          f (fun () -> lock called (fun () ->
              if not called.Value then
                 cont value
                 called.Value <- true) )
        and remover : IDisposable =
          ev1.Subscribe
            ({ new IObserver<_> with
                  member x.OnNext(v) = finish cont v
                  member x.OnError(e) = finish econt e
                  member x.OnCompleted() =
                    let msg = "Cancelling the workflow, because the Observable awaited using AwaitObservable has completed."
                    finish ccont (new System.OperationCanceledException(msg)) })
        () )))

// --------------------------------------------------------------------------------------
// Agents for handling tweet buffering and synchronization
// --------------------------------------------------------------------------------------

/// Internal type used by AlternativeSourceAgent
type AlternativeSourceAgentMessage<'T> =
  | Ping
  | AddEvent of 'T

/// Reports events from an alternative source (added using `AddEvent`) when the
/// main source does not produce event in a specified timeout. Produced events
/// from the main source are reported using `Ping`.
type AlternativeSourceAgent<'T>(timeout) =
  let event = new Event<'T>()
  let error = new Event<_>()

  let agent = MailboxProcessor.Start(fun inbox ->
    let rec loop lastMessage (lastPingTime:DateTime) = async {
      let sleep = max 10 (timeout - int (DateTime.UtcNow - lastPingTime).TotalMilliseconds)
      let! msg = inbox.TryReceive(sleep)
      match msg with
      | None ->
          try lastMessage |> Option.iter event.Trigger
          with e -> error.Trigger(e)
          return! loop None DateTime.UtcNow
      | Some(Ping) ->
          return! loop lastMessage DateTime.UtcNow
      | Some(AddEvent t) ->
          return! loop (Some t) lastPingTime }
    loop None DateTime.UtcNow )

  /// Exception has been thrown when triggering `EventOccurred`
  member x.ErrorOccurred = error.Publish
  /// Triggered when the alternative source event is produced
  member x.EventOccurred = event.Publish
  /// Report that main source produced a value
  member x.Ping() = agent.Post(Ping)
  /// Add new alternative source event to the buffer
  member x.AddEvent(e) = agent.Post(AddEvent e)


/// Remembers the current state and adds it to all events that happen
type KeepStateAgent<'T, 'S>() =
  let event = new Event<'S * 'T>()
  let error = new Event<_>()

  let agent = MailboxProcessor.Start(fun inbox ->
    let rec loop state = async {
      let! msg = inbox.Receive()
      match msg, state with
      | Choice1Of2 newState, _ -> return! loop (Some newState)
      | Choice2Of2 update, None -> return! loop state
      | Choice2Of2 update, Some state ->
          try event.Trigger( (state, update) )
          with e -> error.Trigger(e)
          return! loop (Some state) }
    loop None )

  /// Exception has been thrown when triggering `EventOccurred`
  member x.ErrorOccurred = error.Publish
  /// Triggered when the alternative source event is produced
  member x.EventOccurred = event.Publish
  /// Update the state
  member x.SetState(state) = agent.Post(Choice1Of2 state)
  /// Report an event
  member x.AddEvent(e) = agent.Post(Choice2Of2 e)


/// A simple ring buffer that starts with specified initial values
type RingBuffer<'T>(size, initial:'T) =
  let buffer = Array.create size initial
  let mutable index = 0
  member x.Add(v) =
    buffer.[index] <- v
    index <- index + 1
    if index = buffer.Length then index <- 0
  member x.Buffer = buffer
  member x.Values = [ for i in 0 .. buffer.Length-1 -> buffer.[(index+i)%buffer.Length] ]


/// Agent that keeps values in a ring buffer of the specified size and allows
/// the caller to calculate aggregates over the buffer
type MapBufferAgent<'T, 'R>(size, f, initial:'T) =
  let event = new Event<'R>()
  let error = new Event<_>()
  let agent = MailboxProcessor.Start(fun inbox -> async {
    let buffer = RingBuffer(size, initial)
    let index = ref 0
    while true do
      let! msg = inbox.Receive()
      buffer.Add(msg)
      try event.Trigger(f buffer.Buffer) with e -> error.Trigger(e)
  })

  /// Add new event to the buffer
  member x.AddEvent(v) = agent.Post(v)
  /// Triggered when the state changes happens
  member x.StateChanged = event.Publish
  /// Exception has been thrown when triggering `StateChanged`
  member x.ErrorOccurred = Event.merge agent.Error error.Publish


/// Agent that emits the specified events at the specified times
type SchedulerAgent<'T>() =
  let event = new Event<'T>()
  let error = new Event<_>()
  let agent = MailboxProcessor<DateTime * 'T>.Start(fun inbox ->

    // We keep a list of events together with the DateTime when they should occur
    let rec loop events = async {

      // Report events that are happening now & forget them
      let events, current =
        events |> List.partition (fun (time, e) -> time > DateTime.UtcNow)
      for _, e in current do
        try event.Trigger(e)
        with e -> error.Trigger(e)

      // Sleep until new events are added or until the first upcoming event
      let timeout =
        if List.isEmpty events then System.Threading.Timeout.Infinite else
          let t = int ((events |> List.map fst |> List.min) - DateTime.UtcNow).TotalMilliseconds
          max 10 t
      let! newEvents = inbox.TryReceive(timeout)
      let newEvents = match newEvents with Some v -> [v] | _ -> []
      return! loop (if List.isEmpty newEvents then events else events @ newEvents) }
    loop [])

  /// Schedule new events to happen in the future
  member x.AddEvent(event) = agent.Post(event)
  /// Triggered when an event happens
  member x.EventOccurred = event.Publish
  /// Exception has been thrown when triggering `EventOccurred`
  member x.ErrorOccurred = Event.merge agent.Error error.Publish


/// Limits the rate of emitted messages to at most one per the specified number of milliseconds
type RateLimitAgent<'T>(milliseconds:int) =
  let event = Event<'T>()
  let error = Event<_>()
  let agent = MailboxProcessor.Start(fun inbox ->
    let rec loop (lastMessageTime:DateTime) = async {
      let! e = inbox.Receive()
      let now = DateTime.UtcNow
      if (now - lastMessageTime).TotalMilliseconds > float milliseconds then
        try event.Trigger(e)
        with e -> error.Trigger(e)
        return! loop now
      else
        return! loop lastMessageTime }
    loop DateTime.MinValue )

  /// Triggered when an event happens (within the specified rate)
  member x.EventOccurred = event.Publish
  /// Send an event to the agent - it will either be ignored or forwarded
  member x.AddEvent(event) = agent.Post(event)
  /// Exception has been thrown when triggering `EventOccurred`
  member x.ErrorOccurred = Event.merge agent.Error error.Publish

// --------------------------------------------------------------------------------------
// Expose functionality as 'Observable' module extensions
// --------------------------------------------------------------------------------------

module Observable =
  let private observable f =
    { new IObservable<_> with
        member x.Subscribe(obs) = f obs }

  /// Limits the rate of emitted messages to at most one per the specified number of milliseconds
  let limitRate milliseconds (source:IObservable<_>) =
    observable (fun obs ->
      let rate = RateLimitAgent(milliseconds)
      rate.EventOccurred.Add(obs.OnNext)
      rate.ErrorOccurred.Add(obs.OnError)
      { new IObserver<_> with
          member x.OnCompleted() = obs.OnCompleted()
          member x.OnError(e) = obs.OnError(e)
          member x.OnNext(v) = rate.AddEvent(v) }
      |> source.Subscribe )

  /// Behaves like `Observable.map`, but does not stop when error happens
  let mapAsyncIgnoreErrors f (source:IObservable<_>) =
    observable (fun obs ->
      { new IObserver<_> with
          member x.OnCompleted() = obs.OnCompleted()
          member x.OnError(e) = obs.OnError(e)
          member x.OnNext(v) =
            async { try
                      let! r = f v
                      obs.OnNext(r)
                    with e -> obs.OnError(e) } |> Async.Start }
      |> source.Subscribe )

  /// Emits the specified events at the specified times
  let replay (source:IObservable<_>) =
    observable (fun obs ->
      let sched = SchedulerAgent()
      sched.EventOccurred.Add(obs.OnNext)
      sched.ErrorOccurred.Add(obs.OnError)
      { new IObserver<_> with
          member x.OnCompleted() = obs.OnCompleted()
          member x.OnError(e) = obs.OnError(e)
          member x.OnNext(v) = sched.AddEvent(v) }
      |> source.Subscribe )


  /// Keeps values in a ring buffer of the specified size and calculate aggregates over the buffer
  let aggregateOver count initial f (source:IObservable<_>) =
    observable (fun obs ->
      let sched = MapBufferAgent(count, f, initial)
      sched.StateChanged.Add(obs.OnNext)
      sched.ErrorOccurred.Add(obs.OnError)
      { new IObserver<_> with
          member x.OnCompleted() = obs.OnCompleted()
          member x.OnError(e) = obs.OnError(e)
          member x.OnNext(v) = sched.AddEvent(v) }
      |> source.Subscribe )

  /// Start the specified observable and keep it running forever.
  let start (source:IObservable<_>) =
    let onCompleted = Event<_>()
    let onError = Event<_>()
    let onNext = Event<_>()
    let subscription =
      { new IObserver<_> with
          member x.OnCompleted() = onCompleted.Trigger()
          member x.OnError(e) = onError.Trigger(e)
          member x.OnNext(v) = onNext.Trigger(v) }
      |> source.Subscribe
    let result =
      observable (fun obs ->
        let onNextHandler = Handler(fun _ v -> obs.OnNext(v))
        let onErrorHandler = Handler(fun _ e -> obs.OnError(e))
        let onCompletedHandler = Handler(fun _ _ -> obs.OnCompleted())
        onCompleted.Publish.AddHandler(onCompletedHandler)
        onError.Publish.AddHandler(onErrorHandler)
        onNext.Publish.AddHandler(onNextHandler)
        { new IDisposable with
            member x.Dispose() =
              onCompleted.Publish.RemoveHandler(onCompletedHandler)
              onError.Publish.RemoveHandler(onErrorHandler)
              onNext.Publish.RemoveHandler(onNextHandler) })
    result

  /// Keep state while processing events
  let stateful (state:'TState) f source =
    source
    |> Observable.scan (fun (s, _) v ->
        let s, r = f s v
        s, Some r) (state, None)
    |> Observable.choose (function n, Some tw -> Some(n, tw) | _ -> None)

  let addWithError f ef (source:IObservable<_>) =
    source.Subscribe
      { new IObserver<_> with
          member x.OnCompleted() = ()
          member x.OnError(e) = ef e
          member x.OnNext(v) = f v } |> ignore
