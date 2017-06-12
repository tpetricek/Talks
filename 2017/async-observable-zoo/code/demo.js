var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

import { setType } from "fable-core/Symbol";
import _Symbol from "fable-core/Symbol";
import { compareRecords, equalsRecords, Tuple, makeGeneric, compareUnions, equalsUnions } from "fable-core/Util";
import { ofArray } from "fable-core/List";
import List from "fable-core/List";
import { app, Dynamic, op_EqualsBangGreater, op_EqualsGreater, text, h, El } from "./elmish";
import { newGuid } from "fable-core/String";
export var Update = function () {
  function Update(caseName, fields) {
    _classCallCheck(this, Update);

    this.Case = caseName;
    this.Fields = fields;
  }

  _createClass(Update, [{
    key: _Symbol.reflection,
    value: function () {
      return {
        type: "Demo.Update",
        interfaces: ["FSharpUnion", "System.IEquatable", "System.IComparable"],
        cases: {
          Create: [],
          Input: ["string"],
          Remove: ["string"]
        }
      };
    }
  }, {
    key: "Equals",
    value: function (other) {
      return equalsUnions(this, other);
    }
  }, {
    key: "CompareTo",
    value: function (other) {
      return compareUnions(this, other);
    }
  }]);

  return Update;
}();
setType("Demo.Update", Update);
export var Model = function () {
  function Model(items, input) {
    _classCallCheck(this, Model);

    this.Items = items;
    this.Input = input;
  }

  _createClass(Model, [{
    key: _Symbol.reflection,
    value: function () {
      return {
        type: "Demo.Model",
        interfaces: ["FSharpRecord", "System.IEquatable", "System.IComparable"],
        properties: {
          Items: makeGeneric(List, {
            T: Tuple(["string", "string"])
          }),
          Input: "string"
        }
      };
    }
  }, {
    key: "Equals",
    value: function (other) {
      return equalsRecords(this, other);
    }
  }, {
    key: "CompareTo",
    value: function (other) {
      return compareRecords(this, other);
    }
  }]);

  return Model;
}();
setType("Demo.Model", Model);
export function update(state, action) {
  return state;
}
export function render(trigger, state) {
  return function (arg0) {
    return function (arg1) {
      return El.op_Dynamic(arg0, arg1);
    };
  }(h)("div")(new List())(ofArray([function (arg0_1) {
    return function (arg1_1) {
      return El.op_Dynamic(arg0_1, arg1_1);
    };
  }(h)("ul")(new List())(ofArray([function (arg0_2) {
    return function (arg1_2) {
      return El.op_Dynamic(arg0_2, arg1_2);
    };
  }(h)("li")(new List())(ofArray([text("Fake work #1"), function (arg0_3) {
    return function (arg1_3) {
      return El.op_Dynamic(arg0_3, arg1_3);
    };
  }(h)("a")(ofArray([op_EqualsGreater("href", "#")]))(ofArray([function (arg0_4) {
    return function (arg1_4) {
      return El.op_Dynamic(arg0_4, arg1_4);
    };
  }(h)("span")(new List())(ofArray([text("X")]))]))])), function (arg0_5) {
    return function (arg1_5) {
      return El.op_Dynamic(arg0_5, arg1_5);
    };
  }(h)("li")(new List())(ofArray([text("Fake work #2"), function (arg0_6) {
    return function (arg1_6) {
      return El.op_Dynamic(arg0_6, arg1_6);
    };
  }(h)("a")(ofArray([op_EqualsGreater("href", "#")]))(ofArray([function (arg0_7) {
    return function (arg1_7) {
      return El.op_Dynamic(arg0_7, arg1_7);
    };
  }(h)("span")(new List())(ofArray([text("X")]))]))]))])), function (arg0_8) {
    return function (arg1_8) {
      return El.op_Dynamic(arg0_8, arg1_8);
    };
  }(h)("input")(ofArray([op_EqualsGreater("value", state.Input), op_EqualsBangGreater("oninput", function (d) {
    trigger(new Update("Input", [function (arg0_9) {
      return function (arg1_9) {
        return Dynamic.op_Dynamic(arg0_9, arg1_9);
      };
    }(function (arg0_10) {
      return function (arg1_10) {
        return Dynamic.op_Dynamic(arg0_10, arg1_10);
      };
    }(d)("target"))("value")]));
  })]))(new List()), function (arg0_11) {
    return function (arg1_11) {
      return El.op_Dynamic(arg0_11, arg1_11);
    };
  }(h)("button")(ofArray([op_EqualsBangGreater("onclick", function (_arg1) {
    trigger(new Update("Create", []));
  })]))(ofArray([text("Add")]))]));
}
export var initial = function () {
  var Input = "";
  return new Model(ofArray([[newGuid(), "First work item"], [newGuid(), "Second work item"]]), Input);
}();
app("todo", initial, function (trigger) {
  return function (state) {
    return render(trigger, state);
  };
}, function (state_1) {
  return function (action) {
    return update(state_1, action);
  };
});