import { Union } from "./.fable/fable-library.3.2.9/Types.js";
import { int32_type, char_type, union_type, bool_type, class_type, string_type } from "./.fable/fable-library.3.2.9/Reflection.js";

export class Value extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["String", "Number", "Bool"];
    }
}

export function Value$reflection() {
    return union_type("Basic.Domain.Value", [], Value, () => [[["Item", string_type]], [["Item", class_type("System.Decimal")]], [["Item", bool_type]]]);
}

export class Expression extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Literal", "Variable", "Binary", "Function"];
    }
}

export function Expression$reflection() {
    return union_type("Basic.Domain.Expression", [], Expression, () => [[["Item", Value$reflection()]], [["Item", string_type]], [["Item1", char_type], ["Item2", Expression$reflection()], ["Item3", Expression$reflection()]], [["Item1", string_type], ["Item2", Expression$reflection()]]]);
}

export class Command extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Print", "Goto", "Run", "List", "Assign", "Input", "If"];
    }
}

export function Command$reflection() {
    return union_type("Basic.Domain.Command", [], Command, () => [[["Item", Expression$reflection()]], [["Item", int32_type]], [], [], [["Item1", string_type], ["Item2", Expression$reflection()]], [["Item", string_type]], [["Item1", Expression$reflection()], ["Item2", int32_type]]]);
}

