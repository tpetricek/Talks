import { toString as toString_2, Record } from "./.fable/fable-library.3.2.9/Types.js";
import { record_type, string_type, class_type, int32_type } from "./.fable/fable-library.3.2.9/Reflection.js";
import { Value, Value$reflection, Command$reflection } from "./Domain.fs.js";
import { FSharpMap__Add, FSharpMap__get_Item } from "./.fable/fable-library.3.2.9/Map.js";
import { toString as toString_1, equals, compare, op_Multiply, op_Addition, toNumber } from "./.fable/fable-library.3.2.9/Decimal.js";
import Decimal from "./.fable/fable-library.3.2.9/Decimal.js";
import { singleton } from "./.fable/fable-library.3.2.9/AsyncBuilder.js";
import { sleep } from "./.fable/fable-library.3.2.9/Async.js";
import { head, tryFind, singleton as singleton_1, collect, delay, toList, sortBy } from "./.fable/fable-library.3.2.9/Seq.js";
import { equals as equals_1, compare as compare_1 } from "./.fable/fable-library.3.2.9/Util.js";
import { print, input, awaitObservable } from "./Helpers.fs.js";
import { printf, toText } from "./.fable/fable-library.3.2.9/String.js";

export class State extends Record {
    constructor(Program, Variables) {
        super();
        this.Program = Program;
        this.Variables = Variables;
    }
}

export function State$reflection() {
    return record_type("Basic.Evaluator.State", [], State, () => [["Program", class_type("Microsoft.FSharp.Collections.FSharpMap`2", [int32_type, Command$reflection()])], ["Variables", class_type("Microsoft.FSharp.Collections.FSharpMap`2", [string_type, Value$reflection()])]]);
}

export const rnd = {};

export function getNumber(_arg1) {
    if (_arg1.tag === 1) {
        const n = _arg1.fields[0];
        return n;
    }
    else {
        throw (new Error("Not a number"));
    }
}

export function evalExpr(state, _arg1) {
    let pattern_matching_result, s, v, e_1, l, r, l_1, r_1, l_2, r_2, l_3, r_3, l_4, r_4;
    if (_arg1.tag === 0) {
        pattern_matching_result = 1;
        v = _arg1.fields[0];
    }
    else if (_arg1.tag === 3) {
        if (_arg1.fields[0] === "RND") {
            pattern_matching_result = 2;
        }
        else if (_arg1.fields[0] === "INT") {
            pattern_matching_result = 3;
            e_1 = _arg1.fields[1];
        }
        else {
            pattern_matching_result = 9;
        }
    }
    else if (_arg1.tag === 2) {
        if (_arg1.fields[0] === "*") {
            pattern_matching_result = 5;
            l_1 = _arg1.fields[1];
            r_1 = _arg1.fields[2];
        }
        else if (_arg1.fields[0] === "+") {
            pattern_matching_result = 4;
            l = _arg1.fields[1];
            r = _arg1.fields[2];
        }
        else if (_arg1.fields[0] === "\u003c") {
            pattern_matching_result = 6;
            l_2 = _arg1.fields[1];
            r_2 = _arg1.fields[2];
        }
        else if (_arg1.fields[0] === "=") {
            pattern_matching_result = 8;
            l_4 = _arg1.fields[1];
            r_4 = _arg1.fields[2];
        }
        else if (_arg1.fields[0] === "\u003e") {
            pattern_matching_result = 7;
            l_3 = _arg1.fields[1];
            r_3 = _arg1.fields[2];
        }
        else {
            pattern_matching_result = 9;
        }
    }
    else {
        pattern_matching_result = 0;
        s = _arg1.fields[0];
    }
    switch (pattern_matching_result) {
        case 0: {
            return FSharpMap__get_Item(state.Variables, s);
        }
        case 1: {
            return v;
        }
        case 2: {
            return new Value(1, new Decimal(Math.random()));
        }
        case 3: {
            return new Value(1, new Decimal(~(~toNumber(getNumber(evalExpr(state, e_1))))));
        }
        case 4: {
            return new Value(1, op_Addition(getNumber(evalExpr(state, l)), getNumber(evalExpr(state, r))));
        }
        case 5: {
            return new Value(1, op_Multiply(getNumber(evalExpr(state, l_1)), getNumber(evalExpr(state, r_1))));
        }
        case 6: {
            return new Value(2, compare(getNumber(evalExpr(state, l_2)), getNumber(evalExpr(state, r_2))) < 0);
        }
        case 7: {
            return new Value(2, compare(getNumber(evalExpr(state, l_3)), getNumber(evalExpr(state, r_3))) > 0);
        }
        case 8: {
            return new Value(2, equals(getNumber(evalExpr(state, l_4)), getNumber(evalExpr(state, r_4))));
        }
        case 9: {
            throw (new Error("Match failure: Basic.Domain.Expression"));
        }
    }
}

export function toString(_arg1) {
    switch (_arg1.tag) {
        case 1: {
            const n = _arg1.fields[0];
            return toString_1(n);
        }
        case 2: {
            const b = _arg1.fields[0];
            return toString_2(b);
        }
        default: {
            const s = _arg1.fields[0];
            return s;
        }
    }
}

export function runCommand(state, ln, cmd) {
    return singleton.Delay(() => singleton.Bind(sleep(100), () => {
        let tupledArg_1;
        const statements = () => sortBy((tuple) => tuple[0], toList(delay(() => collect((matchValue) => {
            const activePatternResult2722 = matchValue;
            const v = activePatternResult2722[1];
            const k = activePatternResult2722[0] | 0;
            return singleton_1([k, v]);
        }, state.Program))), {
            Compare: (x, y) => compare_1(x, y),
        });
        const next_2 = (state_1) => singleton.Delay(() => {
            if (ln != null) {
                const ln_1 = ln | 0;
                const next = tryFind((tupledArg) => {
                    const l = tupledArg[0];
                    return compare_1(l, ln_1) > 0;
                }, statements());
                if (next != null) {
                    const next_1 = next;
                    return singleton.ReturnFrom(runCommand(state_1, next_1[0], next_1[1]));
                }
                else {
                    return singleton.Return(state_1);
                }
            }
            else {
                return singleton.Return(state_1);
            }
        });
        switch (cmd.tag) {
            case 5: {
                const var$ = cmd.fields[0];
                return singleton.Bind(awaitObservable(input), (_arg3) => {
                    const inp = _arg3;
                    print(inp);
                    const n = new Decimal(inp);
                    const state_2 = new State(state.Program, FSharpMap__Add(state.Variables, var$, new Value(1, n)));
                    return singleton.ReturnFrom(next_2(state_2));
                });
            }
            case 4: {
                const v_1 = cmd.fields[0];
                const e = cmd.fields[1];
                const state_3 = new State(state.Program, FSharpMap__Add(state.Variables, v_1, evalExpr(state, e)));
                return singleton.ReturnFrom(next_2(state_3));
            }
            case 6: {
                const ln_4 = cmd.fields[1] | 0;
                const cond = cmd.fields[0];
                return equals_1(evalExpr(state, cond), new Value(2, true)) ? singleton.ReturnFrom(runCommand(state, ln_4, FSharpMap__get_Item(state.Program, ln_4))) : singleton.ReturnFrom(next_2(state));
            }
            case 0: {
                const e_1 = cmd.fields[0];
                print(toString(evalExpr(state, e_1)));
                return singleton.ReturnFrom(next_2(state));
            }
            case 1: {
                const l_1 = cmd.fields[0] | 0;
                return singleton.ReturnFrom(runCommand(state, l_1, FSharpMap__get_Item(state.Program, l_1)));
            }
            case 2: {
                return singleton.ReturnFrom((tupledArg_1 = head(statements()), runCommand(state, tupledArg_1[0], tupledArg_1[1])));
            }
            default: {
                return singleton.Combine(singleton.For(statements(), (_arg2) => {
                    const ln_3 = _arg2[0];
                    const cmd_2 = _arg2[1];
                    print(toText(printf("%A %A"))(ln_3)(cmd_2));
                    return singleton.Zero();
                }), singleton.Delay(() => singleton.ReturnFrom(next_2(state))));
            }
        }
    }));
}

export function runInput(state, ln, cmd) {
    return singleton.Delay(() => {
        if (ln != null) {
            const ln_1 = ln | 0;
            return singleton.Return(new State(FSharpMap__Add(state.Program, ln_1, cmd), state.Variables));
        }
        else {
            return singleton.ReturnFrom(runCommand(state, ln, cmd));
        }
    });
}

