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

//
// NOTE: This is a new helper in this step. It detects a named box.
// You will need to use this in TODO #2 in the 'parseBox' function.
// The second helper additionally checks for specific named and
// you will need it in TODO #4.
//
function isNamed(el) {
  return el.tagName == "DIV" && el.classList.contains("name")
}
function isNamedWithName(el, name) {
  return el.tagName == "DIV" && el.classList.contains("name") && el.children[0].innerHTML == name
}


// --------------------------------------------------------------------------------------
// TODO: Implement parsing of document structure
// --------------------------------------------------------------------------------------

// TODO #1: Copy your 'parseParam' and 'parseBox' from the previous step here!
function parseParam(el) { throw "todo "}
function parseBox(box) { throw "todo" }

// TODO #2: As the new example shows, command boxes can also contain named boxes with
// variable values (like the 'who2' box). We need to skip over those when parsing.
// Modify 'parseBox' so that it ignores any child elements (<span> elements) that contain
// box or named box as their only child (i.e., isNamed(span.children[0]) or isBox(span.children[0]))
// (Note that the <div> is wrapped in <span>. This is not valid HTML, but it makes the
// code structure a bit simpler.)


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

// TODO #4: Copy your 'builtins' from the previous step here!
let builtins = { }


// TODO #7: Do this once you have variables working!
//
// Add 'random' operation to your 'builtins'. Random takes a symbol and
// a range (use valSymbol(ps[0]), valNumber(ps[1]) and valNumber(ps[2])
// to get the parameters). It then generates a random number in the range
// (use Math.round and Math.random) and returns a new named box named using
// the given symbol, containing the random number.
//
// You can construct the result using something like:
// html(`<span><div class="name">
//     <span>[YOUR SYMBOL NAME HERE]</span>
//     <div class="box"><span><b>[YOUR RANDOM NUMBER HERE]</b></span></div>
//   </div></span>`)
//


function lookupBox(scope, name) {
  // TODO #4: When a variable appears in a box, it can refer either to a nested named box
  // inside the current box we are evaluating (scope), or it can refer to a named box in the
  // top-level body element of the page.
  //
  // First, try to find element in 'scope'. To do this, iterate over the children of 
  // 'scope' (<span> tags). Do this from the end (from scope.children.length-1 to 0)!
  //
  // Get the child of the <span> using 'children[0]' and see if it is the right 
  // named box using 'isNamedWithName. If so, we return the box inside the named 
  // box (using 'children[1]' - because 'children[0]' is the box name.) 
  //
  // Second, iterate over 'document.body.children' and try to find a named box there.
  // (Here, the elements are not further wrapped with <span>!)
  throw "lookupBox: Implement this function"
}

function evaluateCommand(scope, command) {
  //
  // TODO #5: Copy your evaluation code from the previous step.
  //
  // TODO #6: If the command invocation has any variables as parameters, we want to
  // replace them with the actual value of the parameter. You can do this using 
  // your 'lookupBox' function! We do not actually replace DOM here - just use
  // 'command.parameters.map(...)' to get a new array of parameters!
  //
  // If the 'p.kind' is "identifier", then you should find the box with the value
  // using 'lookupBox'. The result will be a box, containing <span>, containing
  // the actual value we want to use, so you need to use 'el!.children[0].children[0]'
  // to get the actual parameter element. Use 'parseParam' to turn this into
  // 'Parameter' and return that as the value!
  // 
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