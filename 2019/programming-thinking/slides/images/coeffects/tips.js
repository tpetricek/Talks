var currentTip = null;
var currentTipElement = null;
window.toolTipsStopRelative = false;
window.toolTipsStopFunc = function(t) { return t == "absolute" || t == "relative"; }
window.toolTipsRoot = function(el) { return document.body; }

function hideTip(evt, name, unique) {
    var el = document.getElementById(name);
    el.style.display = "none";
    currentTip = null;
}

function findPos(obj, stopRelative) {
    var curleft = 0;
    var curtop = obj.offsetHeight;
    while (obj) {
        var pos = $(obj).css("position");
        if (stopRelative && window.toolTipsStopFunc(pos)) break;
        curleft += obj.offsetLeft;
        curtop += obj.offsetTop;
        obj = obj.offsetParent;
    };
    return [curleft, curtop];
}

function hideUsingEsc(e) {
    if (!e) { e = event; }
    hideTip(e, currentTipElement, currentTip);
}

function showTip(evt, name, unique, owner, stopRelative) {
    document.onkeydown = hideUsingEsc;
    if (currentTip == unique) return;
    currentTip = unique;
    currentTipElement = name;

    if (stopRelative == null) stopRelative = true;
    var pos = findPos(owner ? owner : (evt.srcElement ? evt.srcElement : evt.target), stopRelative);
    var posx = pos[0];
    var posy = pos[1];

    var el = document.getElementById(name);
    var parent = (document.documentElement == null) ? document.body : document.documentElement;
    el.style.position = "absolute";
    el.style.left = posx + "px";
    el.style.top = posy + "px";
    el.style.display = "block";
}

var tipIndex = 0;

function setupTooltips() {
  $("pre span[title]").each(function() {
    var idx = tipIndex++;
    var tip = "val " + $(this).text() + " : " + $(this).attr("title");
    $("<div class='tip' id='dyat" + idx + "'>" + tip + "</div>").appendTo($(window.toolTipsRoot($(this))));
    $(this)
      .on("mouseenter", function() { showTip(event, "dyat" + idx, idx, null, window.toolTipsStopRelative); })
      .on("mouseleave", function() { hideTip(event, "dyat" + idx, idx); })
      .attr("title", null);
  });
}
