// ----------------------------------------------------------------------------------------
// Interactive document
// ----------------------------------------------------------------------------------------
//
// * ".ia" elements are shown/hidden depending on their ia-key and ia-mode
// * ".ia-choice" are switching .ia elements based on ia-show and ia-hide
//         and their UI behaviour depends on their ia-ui-kind
//
// ----------------------------------------------------------------------------------------

// Keeps the states of switches
var states = { };

// Is the specified ".ia-choice" selected? (meaning that all things it should show
// are shown and all things it should hide are hidden)
function isSelected(el) {
  var selected = true;
  el.data("ia-show").split(' ').forEach(function(k) { if (k != "") { selected = selected && states[k]; }});
  el.data("ia-hide").split(' ').forEach(function(k) { if (k != "") { selected = selected && !states[k]; }});
  return selected;
}

// Set a property of an element that has hover effect
// (data-ia-ui-prop can be used to change e.g. border color rather than background-color)
function setUiProp(el, v) {
  var prop = el.data("ia-ui-prop");
  el.css(prop == null ? "background-color" : prop, v);
}

// Highlight choice elements for all the currently expanded parts of the document
function updateChoices() {
  $(".ia-choice")
    .filter(function() {
      return $(this).data("ia-ui-kind") != null;
    })
    .each(function() {
      var el = $(this);
      if (isSelected(el)) {
        setUiProp($(this), $(this).data("ia-color").sel);
        setUiProp($(this).children(".ia-readmore"), $(this).data("ia-color").sel);
      } else {
        setUiProp($(this), $(this).data("ia-color").bg);
        setUiProp($(this).children(".ia-readmore"), $(this).data("ia-color").bg);
      }
    });
}

// For iterating over things in a loop
function RoundBuffer(items) {
  var index = 0;
  this.next = function() {
    return items[(index++) % items.length];
  }
}

// Iterate over all the elements that can be used to expand/collapse things (.ia-choice)
// and register mouseover/mouseout events for them. Also add "read more" links to those
// marked with ".ia-ui-morelink" style
function initializeBoxes() {
  var colors = { };
  var selected = null;

  var palettes =
    { box: new RoundBuffer([
        {bg:"#4F361D", sel:"#8C5E33"}, {bg:"#324C27", sel:"#547F42"},
        {bg:"#25444C",sel:"#3E727F"}, {bg:"#4C2D45",sel:"#7F4B74"} ]),
      light: new RoundBuffer([
        {sel:"#4F9589",bg:"#8DD3C7"}, {sel:"#827E9E",bg:"#BEBADA"},
        {sel:"#C27927",bg:"#FDB462"}, {sel:"#7AA530",bg:"#B3DE69"},
        {sel:"#B13628",bg:"#FB8072"}, {sel:"#37688A",bg:"#80B1D3"} ]) };


  $(".ia-choice")
    .filter(function() {
      return $(this).data("ia-ui-kind") != null;
    })
    .on("mouseenter", function() {
      setUiProp($(this), $(this).data("ia-color").sel);
      setUiProp($(this).children(".ia-readmore"), $(this).data("ia-color").sel);
    })
    .on("mouseleave", function() {
      if (isSelected($(this))) return;
      setUiProp($(this), $(this).data("ia-color").bg);
      setUiProp($(this).children(".ia-readmore"), $(this).data("ia-color").bg);
    })
    .each(function() {
      var key = $(this).data("ia-key");
      var uikind = $(this).data("ia-ui-kind");
      if (colors[key] == null) colors[key] = palettes[uikind].next();
      $(this).data("ia-color", colors[key])
      setUiProp($(this), $(this).data("ia-color").bg);
    });

  $(".ia-ui-morelink").each(function() {
    var align = $(this).hasClass("ia-ui-bar-left") ? "left" : "right;"
    $(this).append($("<p class='ia-readmore' data-ia-ui-prop='color' style='font-weight:bold; font-size:11pt; color:" +
      $(this).data("ia-color").bg + "; text-align:" + align + "'><i style='margin-right:2px' class='fa fa-plus'></i> Read more</p>"));
  });
}


// Hide or display the specified element (when toggling content display)
function toggleElement(el, visible) {
  if (visible) {
    var h = el.data("last-height");
    el.css("transform", "translate(0px, 0px) scaleY(1.0) scaleX(1.0)");
    el.css("max-height", ((h==null||h<50) ? 1000 : h) + "px");
    el.css("opacity", "1.0");
    setTimeout(function() { el.css("max-height", "10000px"); }, 200);
  } else {
    el.data("last-height", el.height());
    el.css("transform", "translate(0px, -50%) scaleY(0.0) scaleX(0.5)");
    el.css("max-height", "0px");
    el.css("opacity", "0.0");
  }
}


// Update all elements that have the specified display key
function togglePart(key) {
  $(".ia").each(function() {
    if ($(this).data("ia-key") == key) {
      if ($(this).data("ia-mode") == "long") toggleElement($(this), states[key]);
      if ($(this).data("ia-mode") == "short") toggleElement($(this), !states[key]);
    }
  });
}

$(function() {
  // Everything is collapsed by default
  $(".ia").each(function() { states[$(this).data("ia-key")] = false; });
  for(var key in states) togglePart(key);

  // Register mouseover events and update elements to show current choices
  initializeBoxes();
  updateChoices();

  // Add toggle event handler for all the elements for choosing displayed content
  $(".ia-choice")
  .on("click", function(e) {
    logEvent("ia", "toggle", $(this).data("ia-key"));
    var show = " " + $(this).data("ia-show") + " ";
    var hide = " " + $(this).data("ia-hide") + " ";
    var undoable = $(this).data("ia-undoable");
    var trueOrNot = (undoable && isSelected($(this))) ? false : true;
    for(var key in states) {
      if (show.indexOf(" " + key + " ") >= 0) states[key] = trueOrNot;
      if (hide.indexOf(" " + key + " ") >= 0) states[key] = !trueOrNot;
      togglePart(key);
    }
    updateChoices();
    if (e.target == window.location + "#") e.preventDefault();
  });

  // This is needed for implementing the transitions correctly
  $(".ia").each(function() {
    $(this).css("max-height", $(this).height());
  });
  $(window).resize(function() {
    $(".ia").each(function() {
      if (states[$(this).data("ia-key")])
        $(this).css("max-height", "1000px");
      else
        $(this).data("last-height", 1000);
    });
  });
})

// ----------------------------------------------------------------------------------------
// Handling of slide-show elements
// ----------------------------------------------------------------------------------------

var slides = { };
$(function() {

  // Display one of the slides & enabled disable left/right arrows
  function displaySlide(id) {
    $("#" + id + " .slide").hide();
    $("#" + id + " .slide").eq(slides[id].current).show();

    var la = $("#" + id + " .larrow");
    if (slides[id].current == 0) la.addClass("larrow-disabled");
    else la.removeClass("larrow-disabled");

    var ra = $("#" + id + " .rarrow");
    if (slides[id].current == slides[id].count-1) ra.addClass("rarrow-disabled");
    else ra.removeClass("rarrow-disabled");

    logEvent("ia", "slide", {"id":id, "current":slides[id].current});
  }

  // For each ".ia-slide" element, count how many slides it has & add arrows
  $(".ia-slides").each(function() {
    var id = $(this).attr("id");
    slides[id] = {current:0, count:$("#" + id + " .slide").length };
    $(this).prepend($("<div class='larrow'></div>").on("click", function() {
        if ($(this).hasClass("larrow-disabled")) return;
        slides[id].current = (slides[id].current + slides[id].count - 1) % slides[id].count;
        displaySlide(id);
      })).prepend($("<div class='rarrow'></div>").on("click", function() {
        if ($(this).hasClass("rarrow-disabled")) return;
        slides[id].current = (slides[id].current + 1) % slides[id].count;
        displaySlide(id);
      }));
    displaySlide(id);
  });
});


// ----------------------------------------------------------------------------------------
// Simple celular automata that is shown in the "Stencil computations" section
// ----------------------------------------------------------------------------------------

$(function() {
  var tdSize = $("#rule110btn").data("size") || 2;
  var tdDark = $("#rule110btn").data("dark") || "black";
  var tdLight = $("#rule110btn").data("light") || "white";

  function get(input, i) {
    var j = i < 0 ? i + input.length : (i >= input.length ? i - input.length : i);
    return input[j];
  }

  function stenc(input) {
    var output = new Array(input.length);
    for(var i = 0; i < input.length; i++) {
      var sum = get(input, i) + get(input, i-1) + get(input, i+1);
      output[i] = sum==2 || (sum==1&&get(input, i-1)==0) ? 1 : 0;
    }
    return output;
  }

  var input = Array(250);
  for(var i=0; i<input.length; i++) input[i]=Math.random()<0.95?0:1;
  input[input.length-1] = 1;

  function addRow() {
    var strs = [];
    strs.push("<tr>");
    for(var j=0; j<input.length; j++) {
      strs.push("<td style='width:" + tdSize + "px;height:" + tdSize + "px;background:"+(input[j]==1?tdDark:tdLight)+"'></td>");
    }
    strs.push("</tr>");
    input = stenc(input);
    $("#rule110").append($(strs.join("")));
    if ($("#rule110 tr").length >= 50) $("#rule110 tr").slice(0, 1).remove();
  }

  for(var j=0; j<50; j++) addRow();

  var intId = 0;
  $("#rule110btn").on("click", function() {
    logEvent("ia", "cells", "run");
    if ($(this).html() == "Run") {
      intId = setInterval(addRow, 100);
      $(this).html("Stop");
    } else {
      clearInterval(intId);
      $(this).html("Run");
    }
  })
})

// ----------------------------------------------------------------------------------------
// ----------------------------------------------------------------------------------------

$(function() {
  function reset(el)
  {
    var gr = document.getElementById(el);
    var rows = gr.getElementsByTagName("tr");
    for(var i=0; i<3; i++)
    {
      var row = rows[i].getElementsByTagName("td");
      for(var j=0; j<3; j++) row[j].className = "";
    }
  }
  function highlight(el,x,y,cls)
  {
    if (x<0 || x>2 || y<0 || y>2) return;
    var gr = document.getElementById(el);
    var rows = gr.getElementsByTagName("tr");
    rows[x].getElementsByTagName("td")[y].className=cls;
  }
  function get(el,x,y)
  {
    if (x<0 || x>2 || y<0 || y>2) return null;
    var gr = document.getElementById(el);
    var rows = gr.getElementsByTagName("tr");
    return 1.0 * rows[x].getElementsByTagName("td")[y].innerText;
  }
  function set(el,x,y,v)
  {
    if (x<0 || x>2 || y<0 || y>2) return null;
    var gr = document.getElementById(el);
    var rows = gr.getElementsByTagName("tr");
    rows[x].getElementsByTagName("td")[y].innerText = Math.round(v*10)/10;
  }
  function calculate(x,y)
  {
    reset("grin");
    reset("grout");
    highlight("grout", x, y, "cur");
    highlight("grin", x, y, "cur");
    highlight("grin", x-1, y, "nb");
    highlight("grin", x+1, y, "nb");
    highlight("grin", x, y-1, "nb");
    highlight("grin", x, y+1, "nb");
    var sum = 0;
    var count = 0;
    for(var i=-1; i<=1; i++)
    {
      for(var j=-1; j<=1; j++)
      {
        if (i != 0 && j != 0) continue;
        var v = get("grin", x+i, y+j);
        if (v != null) { sum += v; count++; }
      }
    }
    set("grout",x,y,sum/count);
  }
  var listeTimer = -1;
  function run(x,y)
  {
    calculate(x,y);
    x++;
    if (x==3) { x = 0; y++; }
    if (y<3) {
      listeTimer = setTimeout(function () { run(x,y); }, 1500);
    } else {
      listeTimer = setTimeout(function () { reset("grin"); reset("grout"); }, 1500);
    }
  }
  $("#btn-comonad-demo").on("click", function() {
    logEvent("ia", "comonad", "run");
    if (listeTimer != -1) clearTimeout(listeTimer);
    $("#grout td").text("?");
    run(0,0);
  });
});


// ----------------------------------------------------------------------------------------
// Smooth scroll to anchor
// ----------------------------------------------------------------------------------------

$(function() {
  $('a[href*=#]:not([href=#])').click(function() {
    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
      if (target.length) {
        logEvent("ia", "scroll", this.hash);
        $('html,body').animate({
          scrollTop: target.offset().top
        }, 1000);
        return false;
      }
    }
  });
});

// ----------------------------------------------------------------------------------------
// Playground for dataflow computations based on mouse X & Y position
// ----------------------------------------------------------------------------------------

// Keeps the specified number of past elements and adds new one to the front
// (moving the older ones further to the history)
function PastBuffer(count, initial) {
  var buffer = new Array(count);
  for(var i = 0; i < count; i++) buffer[i] = initial;
  this.get = function() {
    return buffer;
  }
  this.add = function(v) {
    for(var i = count-1; i >= 0; i--) buffer[i] = buffer[i-1];
    buffer[0] = v;
  }
}


// Updates the function in a playground
function setLiveChartFunction(prefix, xl, yl, fnc) {
  window[prefix + "-dfplayground"].setFunction(xl, yl, fnc);
}


// Create a dataflow playground - assumes that there is #<prefix>-drawinspace
function dataflowPlayground(prefix, chartWidth) {
  // Time series that are added to the charts
  var xseries = new TimeSeries();
  var yseries = new TimeSeries();
  var vseries = new TimeSeries();
  // This is the drawing space that user moves over
  var space = $("#" + prefix + "-drawingspace");

  // Is the chart currently running?
  var running = false;
  // Timer that stops the chart when nothing happens
  var timer = -1;

  // Created SmoothieCharts objects
  var chartIn = null;
  var chartOut = null;

  // Current function & buffer for values
  var f = null;
  var xs = null;
  var ys = null;

  // Save this instance in a global variable (for 'setLiveChartFunction')
  window[prefix + "-dfplayground"] = this;

  // Updates the function - called by 'setLiveChartFunction'
  this.setFunction = function(xl, yl, fnc) {
    xs = new PastBuffer(xl, 0);
    ys = new PastBuffer(yl, 0);
    f = fnc;
  }


  // We keep last cursor position and add the points in 'updateSeries' once every 50ms
  var running = -1;
  var cursorX;
  var cursorY;

  var moveOrTouchHandler = function(event) {
    if (running == -1) {
      logEvent("play", "start-live-chart", "");
      running = setInterval(updateSeries, 50); chartIn.start(); chartOut.start();
    }
    if (timer != -1) clearTimeout(timer);
    timer = setTimeout(function() {
      logEvent("play", "stop-live-chart", "");
      chartIn.stop(); chartOut.stop(); clearInterval(running); running = -1;
    }, 2000);

    event.preventDefault();
    var px = event.pageX || event.originalEvent.targetTouches[0].pageX;
    var py = event.pageY || event.originalEvent.targetTouches[0].pageY;
    cursorX = (px - $(this).offset().left) / space.width() * 100;
    cursorY = (py - $(this).offset().top) / space.height() * 100;
  }

  function updateSeries() {
    var t = new Date().getTime();
    xseries.append(t, cursorX);
    yseries.append(t, cursorY);
    if (f != null) {
      xs.add(cursorX);
      ys.add(cursorY);
      vseries.append(t, f(xs.get())(ys.get()));
    }
  };

  space.on("touchmove", moveOrTouchHandler);
  space.on("mousemove", moveOrTouchHandler);

  // Create the chart controls
  chartWidth = chartWidth || ($("#" + prefix + "-input").outerWidth()-6);
  $("#chartIn").attr("width", chartWidth);
  $("#chartOut").attr("width", chartWidth);
  chartIn = new SmoothieChart({ grid:{strokeStyle:'rgba(119,119,119,0.44)',verticalSections:5,borderVisible:false}, labels:{disabled:true}, interpolation:'linear', millisPerPixel:10,enableDpiScaling:false });
  chartIn.addTimeSeries(xseries, { strokeStyle: 'rgba(64, 96, 64, 1)', fillStyle: 'rgba(0, 0, 0, 0)', lineWidth: 4 });
  chartIn.addTimeSeries(yseries, { strokeStyle: 'rgba(64, 64, 96, 1)', fillStyle: 'rgba(0, 0, 0, 0)', lineWidth: 4 });
  chartIn.streamTo(document.getElementById("chartIn"), 0);
  chartIn.stop();

  chartOut = new SmoothieChart({ grid:{strokeStyle:'rgba(119,119,119,0.44)',verticalSections:5,borderVisible:false}, labels:{disabled:true}, interpolation:'linear', millisPerPixel:10,enableDpiScaling:false });
  chartOut.addTimeSeries(vseries, { strokeStyle: 'rgba(255, 128, 128, 1)', fillStyle: 'rgba(0, 0, 0, 0)', lineWidth: 4 });
  chartOut.streamTo(document.getElementById("chartOut"), 0);
  chartOut.stop();
}

// ----------------------------------------------------------------------------------------
// Logging user events
// ----------------------------------------------------------------------------------------

function guid(){
  var d = new Date().getTime();
  if (window.performance && typeof window.performance.now === "function") d += performance.now();
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = (d + Math.random()*16)%16 | 0;
      d = Math.floor(d/16);
      return (c=='x' ? r : (r&0x3|0x8)).toString(16);
  });
}

var ssid = guid();
var logStarted = false;

function logEvent(category, evt, data) {
  if (!logStarted) return;
  var usrid = document.cookie.replace(/(?:(?:^|.*;\s*)coeffusrid\s*\=\s*([^;]*).*$)|^.*$/, "$1");
  if (usrid == "") {
    usrid = guid();
    document.cookie = "coeffusrid=" + usrid;
  }
  var logObj =
    { "user":usrid, "session":ssid,
      "time":(new Date()).toISOString(),
      "category":category, "event":evt, "data": data };
  $.ajax({ type: 'POST', url: "http://coeffectlogs.azurewebsites.net/log",
    data:JSON.stringify(logObj), dataType: "text", success:function(r) { } });
}


// ----------------------------------------------------------------------------------------
// Popup survey form
// ----------------------------------------------------------------------------------------

function closeFeedbackAlert() {
  $('#feedback-alert').css('bottom', '-100px');
  return false;
}

$(function() {
  var setup = false;
  $(window).scroll(function() {
    if (setup) return;
    setup = true;

    setTimeout(function() {
      $('#feedback-alert').css('bottom','-1px')
    }, 5000);
  });
})
