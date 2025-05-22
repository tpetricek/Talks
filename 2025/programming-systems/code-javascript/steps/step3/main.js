// --------------------------------------------------------------------------------------
// Turtle graphics using <canvas> - basic setup
// --------------------------------------------------------------------------------------

var ctx = null
var canvas = null

function setupCanvas() {
  canvas = document.getElementById("out")
  ctx = canvas.getContext("2d")
  let dpr = window.devicePixelRatio || 1
  let rect = canvas.getBoundingClientRect()
  canvas.width = rect.width * dpr
  canvas.height = rect.height * dpr
  ctx.scale(dpr, dpr)
}

var turtle = { x:50, y:50, angle:0 }

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

//
// NOTE: New helper in this step!
//
// Takes a HTML string and returns a DOM object representing the HTML.
// (This is a clever trick - we put the string as innerHTML and then return the child.)
function html(html) {
  let el = document.createElement("div")
  el.innerHTML = html
  return el.firstElementChild
}

// --------------------------------------------------------------------------------------
// TODO: Implement builtins and command evaluation 
// --------------------------------------------------------------------------------------

let builtins = {
  // TODO: #2: Copy your 'alert' bultin here
  alert: (_, ps) => { throw "alert: Copy your implementation here" },

  // EXAMPLE: This one is done for you :-). The 'forward' primitive moves the turtle
  // in the current direction by a specified number of pixels and draws a line.
  // (Use this as an example showing how to use 'valNumber' to access parameters!)
  forward: (_, ps) => { 
    let rads = turtle.angle * Math.PI / 180
    ctx.beginPath()
    ctx.moveTo(turtle.x, turtle.y)
    turtle.x += valNumber(ps[0]) * Math.cos(rads)
    turtle.y += valNumber(ps[0]) * Math.sin(rads)
    ctx.lineTo(turtle.x, turtle.y)
    ctx.stroke()
    return []
  },

  // TODO #3: Implement the following primitives!
  // * moveTo - Change 'turtle.x' and 'turtle.y' and also call 'ctx.moveTo' to move to the new location.
  // * left - Subtract the given number from the angle. Make sure the result is between 0 and 360.
  // * right - Add the given number to the angle. Make sure the result is between 0 and 360.  
  moveTo: (_, ps) => { throw "implement me" },
  left: (_, ps) => { throw "implement me" },
  right: (_, ps) => { throw "implement me" },

  // TODO: #5: Do this once you have 'lambda' demo working!
  //
  // If we have 'repeat n [c1; ...; cn]' we evaluate it by replacing 
  // it with 'c1; ...; cn; repeat (n-1) [c1; ...; cn]'.
  //  
  // You can get 'n' using 'valNumber(ps[0])' and 'block' using 'valBox(ps[1])'.
  // If the number 'n' is 0, then return empty list of replacements (no more work to do).
  // Otherwise, you need to construct a new repeat block with decremented 'n'. Do this using:
  //
  //   let repeat = html(`<span><i>repeat</i> <b>${n-1}</b> ${block.outerHTML}</span>`)
  //
  // Now you need to return the chldren of the block with the new block. A good 
  // JavaScript trick is to use the spread operator: Given 'a=[1,2,3]', 
  // writing '[...a,4,5]' will give you '[1,2,3,4,5]'.
  //
  repeat: (_, ps) => { throw "implement me" }
}


// TODO #4: Copy your implementation of 'evaluateCommand' here!
function evaluateCommand(scope, command) { throw "todo" }


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
  [...document.getElementsByClassName("box")].forEach(box => {
    box.contentEditable = "true"
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

window.addEventListener("load", setupHandlers)
window.addEventListener("load", setupCanvas)
window.addEventListener("keydown", keyboardHandler)