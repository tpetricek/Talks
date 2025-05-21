// --------------------------------------------------------------------------------------
// Grammar of the "language"
// --------------------------------------------------------------------------------------

//
// A box consists of a sequence (array) of commands.
//
type Commands = Command[]

//
// A command consists of the name of the operation being invoked
// and a sequence (array) of parameters. During evaluation, we replace
// the <span> representing the command with the result, so we also keep
// track of the original HTML element ('element').
//   
type Command = 
  | { operation:string; parameters:Parameter[], element:Element }

//
// Different kinds of parameters. During evaluation, we may need to copy the original
// HTML element, so we also keep this around (for all kinds of parameters) as 'element'
//
type Parameter = 
  // Identifier represents variable access, we keep the variable name
  | { kind:'identifier', element:Element, name:string }

  // Symbol is a variable name that will be passed as argument when calling
  // (if we have 'let foo = 4', we do not want to replace 'foo' with its value
  // but pass it as a symbol to the primitive operation representing 'let')
  | { kind:'symbol', element:Element, name:string }

  // Primitives can be either numbers <b> or strings <s>. 
  // We convert strings to numbers on the fly if we need a number.
  | { kind:'primitive', element:Element, value:string }

  // Represents a nested box - think of this as a code block.
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
function isBox(nd:Node) {
  let el = nd as Element
  return el.tagName == "DIV" && el.classList.contains("box")
}

// --------------------------------------------------------------------------------------
// TODO: Implement parsing of document structure
// --------------------------------------------------------------------------------------

function parseParam(el:Element) : Parameter {
  // TODO #1: Given a HTML element representing a parameter (one of the special tags
  // inside a <span> representing a command), this should return the right kind of 
  // 'Parameter' based on 'el'. Return something like:  
  //
  //   return { kind:"primitive", value:"42", element:el }
  // 
  console.error(el)
  throw "parseParam: Expected <i>, <q>, <s>, <b> or a <div>."
}

function parseBox(box:Element) : Commands { 
  // TODO #2: Given a <div class="box"> with <span> elements as children, this
  // should iterate over the <span> elements and return an array of commands.
  //
  // HINTS:
  // * Access individual <span> elements using 'box.children'
  // * Each <span> element will then have its operation and parameters as 'el.children'
  // * The first child of <span> should be <i> with the operation name
  // * The subsequent children should be parsed as parameters using 'parseParam'
  let commands : Commands = [] 
  throw "parseBox: Implement this function!"
  return commands
}


function evalStep() {
  // Find the selected box (if there is one) and parse it
  let evalBoxes = document.getElementsByClassName("selected")
  if (evalBoxes.length < 1) return false;
  let commands = parseBox(evalBoxes[0])

  // Display the parsed structure as JSON using the 'debug' output box
  document.getElementById("debug")!.innerHTML = JSON.stringify(commands, null, 2)
  return true;
}

function evalAll() { 
  // We will implement this later. 
}


// --------------------------------------------------------------------------------------
// Keyboard and click handlers - no need to modify this
// --------------------------------------------------------------------------------------


// Make all boxes editable and setup 'click' handler which marks the 
// box as selected (and removes selection from all other boxes)
function setupHandlers() {
  [...document.getElementsByClassName("box")].forEach(el => {
    let box = el as HTMLElement;
    box.contentEditable = "true";
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

window.addEventListener("load", setupHandlers);
window.addEventListener("keydown", keyboardHandler)