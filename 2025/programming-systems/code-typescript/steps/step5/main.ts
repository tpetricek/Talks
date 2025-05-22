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

// A box consists of a sequence (array) of commands.
type Commands = Command[]

// A command consists of the name of the operation being invoked
// and a sequence (array) of parameters. During evaluation, we replace
// the <span> representing the command with the result, so we also keep
// track of the original HTML element ('element').
type Command = 
  | { operation:string; parameters:Parameter[], element:Element }

// Different kinds of parameters. During evaluation, we may need to copy the original
// HTML element, so we also keep this around (for all kinds of parameters) as 'element'
type Parameter = 
  | { kind:'identifier', element:Element, name:string }
  | { kind:'symbol', element:Element, name:string }
  | { kind:'primitive', element:Element, value:string }
  | { kind:'box', element:Element }


// --------------------------------------------------------------------------------------
// Helpers that you can use to implement 'parseParam' function.
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
function isNamed(el:Element) {
  return el.tagName == "DIV" && el.classList.contains("name")
}
function isNamedWithName(el:Element, name:string) {
  return el.tagName == "DIV" && el.classList.contains("name") && el.children[0].innerHTML == name;
}

// --------------------------------------------------------------------------------------
// Parsing of document structure
// --------------------------------------------------------------------------------------

// TODO #1: Copy your 'parseParam' and 'parseBox' from the previous step here!

function parseParam(el:Element) : Parameter { throw "todo "}
function parseBox(box:Element) : Commands { throw "todo" }


// --------------------------------------------------------------------------------------
// Helpers that you can use in evaluation 
// --------------------------------------------------------------------------------------

// The following functions take a 'Parameter' and extract a desired value from it.
// (This can be used in builtins where you get a 'Parameter' but need to get the
// actual value it represents - such as a string or a number.)
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

// When evaluating a command, we then want to replace the command with the result
// (Evaluation can return multiple results, so the replacement is an array.)
// This is a bit tricky to do using DOM correctly, so this just replaces innerHTML.
function replaceCommandElement(commandElement:Element, replacements:Element[]) {
  let parent = commandElement.parentElement
  let html = replacements.map(e => e.outerHTML).join("")
  commandElement.replaceWith("[placeholder]")
  parent!.innerHTML = parent!.innerHTML.replace("[placeholder]", html)
}

// Takes a HTML string and returns a DOM object representing the HTML.
// (This is a clever trick - we put the string as innerHTML and then return the child.)
function html(html:string) {
  let el = document.createElement("div")
  el.innerHTML = html
  return el.firstElementChild!
}

// --------------------------------------------------------------------------------------
// Builtins and command evaluation 
// --------------------------------------------------------------------------------------

// TODO #2: Copy your 'builtins', 'lookupBox' and 'evaluateCommand' from the previous step here!

let builtins : { [key: string]: (scope:Element, ps:Parameter[]) => Element[] } = { }
function lookupBox(scope:Element, name:string) : Element | null { throw "todo" }
function evaluateCommand(scope:Element, command:Command) { throw "todo" }

// TODO #4: Add the 'input' builtin (but first implement TODO #3!)
//
// The operation is clever. It first looks in the current 'scope' to see
// if there is a box (not named). If so, it removes it and uses its value.
// If there is no box, it uses 'window.prompt' to get a value from the user!
// It can be used with multiple symbols, so you'll need to iterate over them.
//
// To find an unnamed box, go over 'scope.children'. This gives you <span>
// elements and if isBox(span.children[0]) then 'span.children[0]' is your box.
// Its value as HTML will be 'span.children[0].children[0].outerHTML'.
// Remove the box from DOM using 'span.remove()'
// Otherwise, construct a value `<s>INPUT FROM THE USER</s>`.
// You will then need to return the same html as from 'random'.



// TODO #3: Modify 'evaluateCommand' to support calling other boxes.
// If the 'command.operation' is not in 'builtins', then try to find its 
// definition in a box using 'lookupBox'! If this succeeds, we want to replace
// the call with the children of the box we found. 
//
// But what if the call has parameters? We add the parameters as unnamed boxes
// before the commands to run. The 'input' operation will then look for those.
// So, turn each parameter into:
//
//   html(`<span><div class="box">${p.element.outerHTML}</div></span>`)
//
// And then concatenate all the parameter boxes with all the children of the 
// found box and use 'replaceCommandElement' to put them in place of the call.



function evalStep() {
  // Find the selected box (if there is one) and parse it
  let evalBoxes = document.getElementsByClassName("selected")
  if (evalBoxes.length < 1) return false

  // Parse the first box we found and run the first command in it
  // (we pass the box itself as a 'scope' element - this is needed later)
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

// Make all boxes editable and setup 'click' handler which marks the 
// box as selected (and removes selection from all other boxes)
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

// Keyboard shortcuts. Alt+E runs one evaluation step; Alt+R runs all.
function keyboardHandler(e:KeyboardEvent) {
  if (e.altKey && e.key == "e") { evalStep(); e.preventDefault() }
  if (e.altKey && e.key == "r") { evalAll(); e.preventDefault() }
}

window.addEventListener("load", setupHandlers)
window.addEventListener("load", setupCanvas)
window.addEventListener("keydown", keyboardHandler)