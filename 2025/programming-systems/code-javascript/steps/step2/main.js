// --------------------------------------------------------------------------------------
// Grammar of the "language"
// --------------------------------------------------------------------------------------

// A box consists of a sequence (array) of commands.
// 
//   type Commands = Command[]

// A command consists of the name of the operation being invoked
// and a sequence (array) of parameters. During evaluation, we replace
// the <span> representing the command with the result, so we also keep
// track of the original HTML element ('element').
//   
//   type Command = 
//     | { operation:string; parameters:Parameter[], element:Element }
//

// Different kinds of parameters. During evaluation, we may need to copy the original
// HTML element, so we also keep this around (for all kinds of parameters) as 'element'
//
//   type Parameter = 
//     | { kind:'identifier', element:Element, name:string }
//     | { kind:'symbol', element:Element, name:string }
//     | { kind:'primitive', element:Element, value:string }
//     | { kind:'box', element:Element }
//   

// --------------------------------------------------------------------------------------
// Helpers that you can use to implement 'parseParam' function.
// --------------------------------------------------------------------------------------

function isPrimitive(el) {
  return el.tagName == "S" || el.tagName == "B"
}
function isIdentifier(el) {
  return el.tagName == "I"
}
function isSymbol(el) {
  return el.tagName == "Q"
}
function isBox(el) {
  return el.tagName == "DIV" && el.classList.contains("box")
}

// --------------------------------------------------------------------------------------
// TODO: Implement parsing of document structure
// --------------------------------------------------------------------------------------

// TODO #1: Copy your 'parseParam' and 'parseBox' from the previous step here!
function parseParam(el) { throw "todo "}
function parseBox(box) { throw "todo" }

// --------------------------------------------------------------------------------------
// Helpers that you can use in evaluation 
// --------------------------------------------------------------------------------------

// The following functions take a 'Parameter' and extract a desired value from it.
// (This can be used in builtins where you get a 'Parameter' but need to get the
// actual value it represents - such as a string or a number.)
function valNumber(val) {
  if (val.kind != "primitive") throw "valNumber: Expected a primitive.";
  return parseInt(val.value);
}
function valString(val) {
  if (val.kind != "primitive") throw "valString: Expected a primitive.";
  return val.value + "";
}
function valIdent(val) {
  if (val.kind != "identifier") throw "valIdent: Expected an identifier.";
  return val.name;
}
function valSymbol(val) {
  if (val.kind != "symbol") throw "valSymbol: Expected a symbol.";
  return val.name;
}
function valBox(val) {
  if (val.kind != "box") throw "valBox: Expected a box.";
  return val.element;
}


// When evaluating a command, we then want to replace the command with the result
// (Evaluation can return multiple results, so the replacement is an array.)
// This is a bit tricky to do using DOM correctly, so this just replaces innerHTML.
function replaceCommandElement(commandElement, replacements) {
  let parent = commandElement.parentElement
  let html = replacements.map(e => e.outerHTML).join("")
  commandElement.replaceWith("[placeholder]")
  parent.innerHTML = parent.innerHTML.replace("[placeholder]", html)
}

// --------------------------------------------------------------------------------------
// TODO: Implement builtins and command evaluation 
// --------------------------------------------------------------------------------------

let builtins = {
  alert: (_, ps) => {
    //
    // TODO #2: Get the string values of all parameters in 'ps' using 'valString',
    // concatenate them into a single string and call 'window.alert' to show a box!
    //
    // NOTE: The 'alert' function doesn't return anything, so we return an empty replacement.
    return []
  },
}


function evaluateCommand(scope, command) {
  //
  // TODO #3: Evaluate the command! See if the operation of the command is in the 
  // builtins dictionary. If so, get the operation and call it with the 'scope' and
  // command parameters as parameters. You should then replace the 'command.element'
  // with the replacement returned from the builtin operation using 'replaceCommandElement'.
  //
  // HINTS:
  // * Check if a dictionary contains a key using "key in dict" and get it using "dict[key]"
  throw "evaluateCommand: Implement this function"
}



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
  // TODO #4: Run 'evalStep' in a loop for as long as it does something
  // (i.e. as long as it keeps returning 'true')
  throw "evalAll: Implement this function"
}


// --------------------------------------------------------------------------------------
// Keyboard and click handlers - no need to modify this
// --------------------------------------------------------------------------------------


// Make all boxes editable and setup 'click' handler which marks the 
// box as selected (and removes selection from all other boxes)
function setupHandlers() {
  [...document.getElementsByClassName("box")].forEach(box => {
    box.contentEditable = "true";
    box.onclick = (e) => {
      [...document.getElementsByClassName("selected")].forEach(e => e.classList.remove("selected"))
      var target = e.target
      while (target && !target.classList.contains("box")) target = target.parentElement;
      target.classList.add("selected");
      e.stopPropagation()
      e.preventDefault()
    }
  })
}

// Keyboard shortcuts. Alt+E runs one evaluation step; Alt+R runs all.
function keyboardHandler(e) {
  if (e.altKey && e.key == "e") { evalStep(); e.preventDefault() }
  if (e.altKey && e.key == "r") { evalAll(); e.preventDefault() }
}

window.addEventListener("load", setupHandlers);
window.addEventListener("keydown", keyboardHandler)