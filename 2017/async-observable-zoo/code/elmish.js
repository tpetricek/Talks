var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

import { setType } from "fable-core/Symbol";
import _Symbol from "fable-core/Symbol";
import { createObj, Tuple, Array as _Array } from "fable-core/Util";
import { split } from "fable-core/String";
import { append } from "fable-core/Seq";
import { ofArray } from "fable-core/List";
import { patch, diff, h as h_1 } from "virtual-dom";
import _Event from "fable-core/Event";
import { add } from "fable-core/Observable";
export var DomAttribute = function () {
  function DomAttribute(caseName, fields) {
    _classCallCheck(this, DomAttribute);

    this.Case = caseName;
    this.Fields = fields;
  }

  _createClass(DomAttribute, [{
    key: _Symbol.reflection,
    value: function () {
      return {
        type: "Elmish.DomAttribute",
        interfaces: ["FSharpUnion"],
        cases: {
          Attribute: ["string"],
          EventHandler: ["function"],
          Property: ["string"]
        }
      };
    }
  }]);

  return DomAttribute;
}();
setType("Elmish.DomAttribute", DomAttribute);
export var DomNode = function () {
  function DomNode(caseName, fields) {
    _classCallCheck(this, DomNode);

    this.Case = caseName;
    this.Fields = fields;
  }

  _createClass(DomNode, [{
    key: _Symbol.reflection,
    value: function () {
      return {
        type: "Elmish.DomNode",
        interfaces: ["FSharpUnion"],
        cases: {
          Element: ["string", _Array(Tuple(["string", DomAttribute])), _Array(DomNode)],
          Text: ["string"]
        }
      };
    }
  }]);

  return DomNode;
}();
setType("Elmish.DomNode", DomNode);
export function createTree(tag, args, children) {
  var attrs = [];
  var props = [];
  var _iteratorNormalCompletion = true;
  var _didIteratorError = false;
  var _iteratorError = undefined;

  try {
    for (var _iterator = args[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
      var forLoopVar = _step.value;
      var matchValue = [forLoopVar[0], forLoopVar[1]];
      var $var1 = matchValue[0] === "style" ? matchValue[1].Case === "Property" ? [0, matchValue[1].Fields[0]] : matchValue[1].Case === "EventHandler" ? [3, matchValue[1].Fields[0], matchValue[0]] : [0, matchValue[1].Fields[0]] : matchValue[1].Case === "Property" ? [2] : matchValue[1].Case === "EventHandler" ? [3, matchValue[1].Fields[0], matchValue[0]] : [1];

      switch ($var1[0]) {
        case 0:
          var args_1 = split($var1[1], ";").map(function (a) {
            var sep = a.indexOf(":");

            if (sep > 0) {
              return [a.substr(0, sep), a.substr(sep + 1)];
            } else {
              return [a, ""];
            }
          });
          props.push(["style", createObj(args_1)]);
          break;

        case 1:
          var v = matchValue[1].Fields[0];
          attrs.push([matchValue[0], v]);
          break;

        case 2:
          var v_1 = matchValue[1].Fields[0];
          props.push([matchValue[0], v_1]);
          break;

        case 3:
          props.push([$var1[2], $var1[1]]);
          break;
      }
    }
  } catch (err) {
    _didIteratorError = true;
    _iteratorError = err;
  } finally {
    try {
      if (!_iteratorNormalCompletion && _iterator.return) {
        _iterator.return();
      }
    } finally {
      if (_didIteratorError) {
        throw _iteratorError;
      }
    }
  }

  var attrs_1 = createObj(attrs);
  var props_1 = createObj(append(ofArray([["attributes", attrs_1]]), props));
  var elem = h_1(tag, props_1, children);
  return elem;
}
export function render(node) {
  if (node.Case === "Element") {
    return createTree(node.Fields[0], node.Fields[1], node.Fields[2].map(function (node_1) {
      return render(node_1);
    }));
  } else {
    return node.Fields[0];
  }
}
export var Dynamic = function () {
  _createClass(Dynamic, [{
    key: _Symbol.reflection,
    value: function () {
      return {
        type: "Elmish.Dynamic",
        properties: {}
      };
    }
  }]);

  function Dynamic() {
    _classCallCheck(this, Dynamic);
  }

  _createClass(Dynamic, null, [{
    key: "op_Dynamic",
    value: function (d, s) {
      return d[s];
    }
  }]);

  return Dynamic;
}();
setType("Elmish.Dynamic", Dynamic);
export function text(s) {
  return new DomNode("Text", [s]);
}
export function op_EqualsGreater(k, v) {
  if (k === "class") {
    return [k, new DomAttribute("Attribute", [v])];
  } else {
    return [k, new DomAttribute("Property", [v])];
  }
}
export function op_EqualsBangGreater(k, f) {
  return [k, new DomAttribute("EventHandler", [function (o) {
    f(o);
  }])];
}
export var El = function () {
  _createClass(El, [{
    key: _Symbol.reflection,
    value: function () {
      return {
        type: "Elmish.El",
        properties: {}
      };
    }
  }]);

  function El() {
    _classCallCheck(this, El);
  }

  _createClass(El, null, [{
    key: "op_Dynamic",
    value: function (_arg1, n) {
      return function (a) {
        return function (b) {
          return new DomNode("Element", [n, Array.from(a), Array.from(b)]);
        };
      };
    }
  }]);

  return El;
}();
setType("Elmish.El", El);
export var h = new El();
export function app(id, initial, r, u) {
  var event = new _Event();

  var trigger = function trigger(e) {
    event.Trigger(e);
  };

  var container = document.createElement("div");
  document.getElementById(id).appendChild(container);
  var tree = {};
  var state = initial;

  var handleEvent = function handleEvent(evt) {
    if (evt != null) {
      state = u(state)(evt);
    } else {
      state = state;
    }

    var newTree = render(r(trigger)(state));
    var patches = diff(tree, newTree);
    container = patch(container, patches);
    tree = newTree;
  };

  handleEvent(null);
  add(function ($var2) {
    return handleEvent(function (arg0) {
      return arg0;
    }($var2));
  }, event.Publish);
}