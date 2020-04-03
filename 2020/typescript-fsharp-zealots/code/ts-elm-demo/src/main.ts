import { h } from 'maquette'
import { startElmishApp } from './elmish'

// DEMO: Implementing an Elmish counter 
// DEMO: Turning on strict null checks

interface Model {
  count : number | null
}

interface ResetEvent { kind:'reset' }
interface UpdateEvent { kind:'update', by:number }
type Event = ResetEvent | UpdateEvent

function render(trigger:(evt:Event) => void, state:Model) {
  if (state.count != null) {
    return h('div', {}, [
      h('h1', {}, [ 'Current count: ', state.count.toString() ]),
      h('button', { key:'inc', onclick:()=>trigger({kind:'update', by:1}) }, [ '+1' ]),
      h('button', { key:'dec', onclick:()=>trigger({kind:'update', by:-1}) }, [ '-1' ])      
    ])
  } else { 
    return h('div', {}, [
      h('h1', {}, [ 'Welcome to Counter!' ]),
      h('button', { onclick:()=>trigger({kind:'reset'}) }, [ 'Start' ])
    ])
  }
}

function update(state:Model, evt:Event) {
  switch(evt.kind) {
    case "reset":
      return { count: 0 }
    case "update":
      if (state.count != null) return { count: state.count + evt.by }
      else return state;
  }
}

let init = { count: null }

startElmishApp('out', init, render, update)