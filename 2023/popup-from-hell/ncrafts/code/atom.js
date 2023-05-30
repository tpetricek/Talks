// -------------------------------------------------------------------
// Colorize
// -------------------------------------------------------------------

function colorize(el,clr) {
  for(var i = 0; i < el.children.length; i++) {
    colorize(el.children[i],clr);
  }
  el.style = 'color:' + clr;
}

// -------------------------------------------------------------------
// Basic
// -------------------------------------------------------------------

window.funjs = "alert('hi there')"
atom.commands.add 'atom-text-editor', 'fun:runscript', -> eval window.funjs

// -------------------------------------------------------------------
// Final version
// -------------------------------------------------------------------

window.funjs = `
var colors = '#E40303,#FF8C00,#FFED00,#008026,#24408E,#732982'.split(',');

function colorize(el,clr) {
  for(var i = 0; i < el.children.length; i++) {
    colorize(el.children[i],clr);
  }
  el.style = 'color:' + clr;
}

var lines = document.getElementsByClassName('line');
for(var i=0; i<lines.length; i++) colorize(lines[i], colors[i%colors.length])
`

atom.commands.add 'atom-text-editor', 'fun:runscript', -> eval window.funjs
