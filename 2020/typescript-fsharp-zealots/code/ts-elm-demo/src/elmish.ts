import { h,createProjector, VNode } from 'maquette'

export function startElmishApp<TState, TEvent>
    ( element:string, init:TState, 
      render:(trigger:((event:TEvent) => void), state:TState) => VNode,
      update:(state:TState, event:TEvent) => TState) : void {
  let state = init
  let proj = createProjector();
  function trigger(event:TEvent | null) {
    if (event) state = update(state, event);
    proj.scheduleRender()
  }
  let el = <HTMLElement>document.getElementById(element);
  proj.replace(el, () => h('div', {'id':element}, [render(trigger, state) ]))
  trigger(null);
}
