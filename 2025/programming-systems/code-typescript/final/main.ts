// --------------------------------------------------------------------------------------
// Turtle graphics using <canvas> - basic setup
// --------------------------------------------------------------------------------------

var ctx : CanvasRenderingContext2D | null = null;
var canvas : HTMLCanvasElement | null = null;

function setupCanvas() {
  canvas = document.getElementById("out") as HTMLCanvasElement
  ctx = canvas!.getContext("2d")
  let dpr = window.devicePixelRatio || 1
  let rect = canvas!.getBoundingClientRect()
  canvas.width = rect.width * dpr
  canvas.height = rect.height * dpr
  ctx!.scale(dpr, dpr)
}

var turtle = { x:50, y:50, angle:0 }

// --------------------------------------------------------------------------------------
// Grammar of the "language"
// --------------------------------------------------------------------------------------

type Commands = Command[]

type Command = 
  | { operation:string; parameters:Parameter[], element:Element }

type Parameter = 
  | { kind:'identifier', element:Element, name:string }
  | { kind:'symbol', element:Element, name:string }
  | { kind:'primitive', element:Element, value:string }
  | { kind:'box', element:Element }


// --------------------------------------------------------------------------------------
// Helpers that are used to implement the 'parseParam' function
// --------------------------------------------------------------------------------------

function isPrimitive(el:Element) {
  return el.tagName == "S" || el.tagName == "B"
}
function isIdentifier(el:Element) {
  return el.tagName == "I"
}
function isSymbol(el:Element) {
  return el.tagName == "Q"
}
function isBox(nd:Node) {
  let el = nd as Element
  return el.tagName == "DIV" && el.classList.contains("box")
}
function isNamed(nd:Node) {
  let el = nd as Element
  return el.tagName == "DIV" && el.classList.contains("name")
}
function isNamedWithName(nd:Node, name:string) {
  let el = nd as Element
  return el.tagName == "DIV" && el.classList.contains("name") && el.children[0].innerHTML == name;
}

// --------------------------------------------------------------------------------------
// Parsing of document structure
// --------------------------------------------------------------------------------------

function parseParam(el:Element) : Parameter {
  if (isIdentifier(el)) return { kind:"identifier", name:el.innerHTML, element:el }
  if (isSymbol(el)) return { kind:"symbol", name:el.innerHTML, element:el }
  if (isPrimitive(el)) return { kind:"primitive", value:el.innerHTML, element:el }
  if (isBox(el)) return { kind:"box", element:el }

  console.error(el)
  throw "parseParam: Expected <i>, <q>, <s>, <b> or a <div>."
}

function parseBox(box:Element) : Commands { 
  let commands : Commands = [] 
  for(let el of box.children) {
    if (el.tagName != "SPAN") throw "parseBox: Expected only <span> elements!"
    if (el.children.length < 1) throw "parseBox: Missing operation"
    if (isBox(el.children[0]) || isNamed(el.children[0])) continue
    
    let operation = el.children[0].innerHTML
    let params : Parameter[] = [] 
    for(var i=1; i<el.children.length; i++) params.push(parseParam(el.children[i]))
    commands.push({ operation:operation, parameters:params, element:el })
  }
  return commands
}


// --------------------------------------------------------------------------------------
// Helpers used in builtins
// --------------------------------------------------------------------------------------

function valNumber(val:Parameter) {
  if (val.kind != "primitive") throw "valNumber: Expected a primitive.";
  return parseInt(val.value);
}
function valString(val:Parameter) {
  if (val.kind != "primitive") throw "valString: Expected a primitive.";
  return val.value + "";
}
function valIdent(val:Parameter) {
  if (val.kind != "identifier") throw "valIdent: Expected an identifier.";
  return val.name;
}
function valSymbol(val:Parameter) {
  if (val.kind != "symbol") throw "valSymbol: Expected a symbol.";
  return val.name;
}
function valBox(val:Parameter) {
  if (val.kind != "box") throw "valBox: Expected a box.";
  return val.element;
}


function replaceCommandElement(commandElement:Element, replacements:Element[]) {
  let parent = commandElement.parentElement
  let html = replacements.map(e => e.outerHTML).join("")
  commandElement.replaceWith("[placeholder]")
  parent!.innerHTML = parent!.innerHTML.replace("[placeholder]", html)
}

function html(html:string) {
  let el = document.createElement("div")
  el.innerHTML = html
  return el.firstElementChild!
}

// --------------------------------------------------------------------------------------
// Builtin operations
// --------------------------------------------------------------------------------------

let builtins : { [key: string]: (scope:Element, ps:Parameter[]) => Element[] } = {
  alert: (_, ps) => {
    window.alert(ps.map(valString).join(""))
    return []
  },
  repeat: (_, ps) => { 
    let n = valNumber(ps[0])
    if (n <= 0) return []
    let block = valBox(ps[1])
    let repeat = html(`<span><i>repeat</i> <b>${n-1}</b> ${block.outerHTML}</span>`)
    return [...block.children, repeat]
  },
  moveTo: (_, ps) => { 
    turtle.x = valNumber(ps[0])
    turtle.y = valNumber(ps[1])
    ctx!.moveTo(turtle.x, turtle.y)
    return []
  },
  left: (_, ps) => { 
    turtle.angle -= valNumber(ps[0])
    if (turtle.angle > 360) turtle.angle -= 360
    if (turtle.angle < 0) turtle.angle += 360
    return []
  },
  right: (_, ps) => {
    turtle.angle += valNumber(ps[0])
    if (turtle.angle > 360) turtle.angle -= 360
    if (turtle.angle < 0) turtle.angle += 360
    return []
  },
  forward: (_, ps) => { 
    let rads = turtle.angle * Math.PI / 180
    ctx!.beginPath()
    ctx!.moveTo(turtle.x, turtle.y)
    turtle.x += valNumber(ps[0]) * Math.cos(rads)
    turtle.y += valNumber(ps[0]) * Math.sin(rads)
    ctx!.lineTo(turtle.x, turtle.y)
    ctx!.stroke()
    return[]
  },
  random: (_, ps) => {
    let v = valSymbol(ps[0])
    let min = valNumber(ps[1])
    let max = valNumber(ps[2])
    let n = Math.round(min + Math.random() * (max - min))
    let box = html(`<span><div class="name">
        <span>${v}</span>
        <div class="box">
          <span><b>${n}</b></span>
        </div>
      </div></span>`)
    return [box]
  },
  input: (scope, ps) => {
    return ps.map(p => {
      let v = ""
      let id = valSymbol(p)
      let firstBox = [...scope.children].find(ch => isBox(ch.children[0]));
      if (firstBox) {
        firstBox.remove()
        v = firstBox.children[0].children[0].outerHTML        
      } else {
        v = "<s>" + window.prompt("Please enter a value:") + "</s>"
      } 
      return html(`<span><div class="name">
        <span>${id}</span>
        <div class="box">
          <span>${v}</span>
        </div>
      </div></span>`)
    })
  } 
}

function lookupBox(scope:Element, name:string) : Element | null {
  for(let i = scope.children.length-1; i >= 0; i--) {
    let ch = scope.children[i]
    let namedBox = ch.children[0]
    if (isNamedWithName(namedBox, name)) return namedBox.children[1] 
  }
  for(let i = 0; i < document.body.children.length; i++) {
    let namedBox =  document.body.children[i]
    if (isNamedWithName(namedBox, name)) return namedBox.children[1] 
  }
  return null;
}

function evaluateCommand(scope:Element, command:Command) {
  let parameters = command.parameters.map(p => {
    if (p.kind == "identifier") {
      let el = lookupBox(scope, p.name)
      if (!el) throw "evaluateCommand: Parameter '" + p.name + "' not found."
      return parseParam(el!.children[0].children[0])
    }        
    else return p 
  })

  if (command.operation in builtins) {
    let builtin = builtins[command.operation]
    let replacement = builtin(scope, parameters)
    replaceCommandElement(command.element, replacement)
    return
  }

  let box = lookupBox(scope, command.operation)
  if (box) {
    let paramBoxes = parameters.map(p => html(`<span><div class="box">${p.element.outerHTML}</div></span>`));
    replaceCommandElement(command.element, paramBoxes.concat([...box.children]))
    return
  } 

  throw "evaluateCommand: Cannot find command '" + command.operation + "'."
}

function evalStep() {
  let evalBoxes = document.getElementsByClassName("selected")
  if (evalBoxes.length < 1) return false
  let commands = parseBox(evalBoxes[0])
  if (commands.length < 1) return false
  evaluateCommand(evalBoxes[0], commands[0])
  return true
}

function evalAll() { 
  while(evalStep()) {}
}


// --------------------------------------------------------------------------------------
// Keyboard and click handlers - no need to modify this
// --------------------------------------------------------------------------------------

function setupHandlers() {
  [...document.getElementsByClassName("box")].forEach(el => {
    let box = el as HTMLElement
    box.contentEditable = "true"
    box.onclick = (e) => {
      [...document.getElementsByClassName("selected")].forEach(e => e.classList.remove("selected"))
      var target : HTMLElement | null = e.target as HTMLElement
      while (target && !target.classList.contains("box")) target = target.parentElement;
      target!.classList.add("selected");
      e.stopPropagation()
      e.preventDefault()
    }
  })
}

function keyboardHandler(e:KeyboardEvent) {
  if (e.altKey && e.key == "e") { evalStep(); e.preventDefault() }
  if (e.altKey && e.key == "r") { evalAll(); e.preventDefault() }
}

window.addEventListener("load", setupHandlers)
window.addEventListener("load", setupCanvas)
window.addEventListener("keydown", keyboardHandler)