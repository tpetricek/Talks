import { toString, Union } from "./.fable/fable-library.3.2.9/Types.js";
import { union_type, float64_type, char_type, string_type } from "./.fable/fable-library.3.2.9/Reflection.js";
import { reverse } from "./.fable/fable-library.3.2.9/Array.js";
import { ofSeq, contains, empty, tail, head, reverse as reverse_1, isEmpty, cons, singleton } from "./.fable/fable-library.3.2.9/List.js";
import { printf, toFail } from "./.fable/fable-library.3.2.9/String.js";
import { parse } from "./.fable/fable-library.3.2.9/Double.js";
import { equals, stringHash } from "./.fable/fable-library.3.2.9/Util.js";
import { Command, Value, Expression } from "./Domain.fs.js";
import Decimal from "./.fable/fable-library.3.2.9/Decimal.js";

export class Token extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Equals", "Ident", "Operator", "Bracket", "Numeral", "Text"];
    }
}

export function Token$reflection() {
    return union_type("Basic.Parser.Token", [], Token, () => [[], [["Item", string_type]], [["Item", char_type]], [["Item", char_type]], [["Item", float64_type]], [["Item", string_type]]]);
}

export function str(rcl) {
    return reverse(Array.from(rcl)).join('');
}

export function isLetter(c) {
    if ((c >= "A") ? (c <= "Z") : false) {
        return true;
    }
    else {
        return c === "$";
    }
}

export function isOp(c) {
    return "+-*/\u003c\u003e".indexOf(toString(c)) >= 0;
}

export function isBracket(c) {
    return "()".indexOf(toString(c)) >= 0;
}

export function isNumber(c) {
    if (c >= "0") {
        return c <= "9";
    }
    else {
        return false;
    }
}

export function tokenize(toks_mut, _arg1_mut) {
    let cs_6, c_6, cs_4, c_4, cs_2, c_2, cs, c;
    tokenize:
    while (true) {
        const toks = toks_mut, _arg1 = _arg1_mut;
        let pattern_matching_result, c_1, cs_1;
        if (!isEmpty(_arg1)) {
            if ((cs = tail(_arg1), (c = head(_arg1), isLetter(c)))) {
                pattern_matching_result = 0;
                c_1 = head(_arg1);
                cs_1 = tail(_arg1);
            }
            else {
                pattern_matching_result = 1;
            }
        }
        else {
            pattern_matching_result = 1;
        }
        switch (pattern_matching_result) {
            case 0: {
                return ident(toks, singleton(c_1), cs_1);
            }
            case 1: {
                let pattern_matching_result_1, c_3, cs_3;
                if (!isEmpty(_arg1)) {
                    if ((cs_2 = tail(_arg1), (c_2 = head(_arg1), isNumber(c_2)))) {
                        pattern_matching_result_1 = 0;
                        c_3 = head(_arg1);
                        cs_3 = tail(_arg1);
                    }
                    else {
                        pattern_matching_result_1 = 1;
                    }
                }
                else {
                    pattern_matching_result_1 = 1;
                }
                switch (pattern_matching_result_1) {
                    case 0: {
                        return number(toks, singleton(c_3), cs_3);
                    }
                    case 1: {
                        let pattern_matching_result_2, c_5, cs_5;
                        if (!isEmpty(_arg1)) {
                            if ((cs_4 = tail(_arg1), (c_4 = head(_arg1), isOp(c_4)))) {
                                pattern_matching_result_2 = 0;
                                c_5 = head(_arg1);
                                cs_5 = tail(_arg1);
                            }
                            else {
                                pattern_matching_result_2 = 1;
                            }
                        }
                        else {
                            pattern_matching_result_2 = 1;
                        }
                        switch (pattern_matching_result_2) {
                            case 0: {
                                toks_mut = cons(new Token(2, c_5), toks);
                                _arg1_mut = cs_5;
                                continue tokenize;
                            }
                            case 1: {
                                let pattern_matching_result_3, c_7, cs_7;
                                if (!isEmpty(_arg1)) {
                                    if ((cs_6 = tail(_arg1), (c_6 = head(_arg1), isBracket(c_6)))) {
                                        pattern_matching_result_3 = 0;
                                        c_7 = head(_arg1);
                                        cs_7 = tail(_arg1);
                                    }
                                    else {
                                        pattern_matching_result_3 = 1;
                                    }
                                }
                                else {
                                    pattern_matching_result_3 = 1;
                                }
                                switch (pattern_matching_result_3) {
                                    case 0: {
                                        toks_mut = cons(new Token(3, c_7), toks);
                                        _arg1_mut = cs_7;
                                        continue tokenize;
                                    }
                                    case 1: {
                                        if (isEmpty(_arg1)) {
                                            return reverse_1(toks);
                                        }
                                        else if (head(_arg1) === " ") {
                                            const cs_10 = tail(_arg1);
                                            toks_mut = toks;
                                            _arg1_mut = cs_10;
                                            continue tokenize;
                                        }
                                        else if (head(_arg1) === "\"") {
                                            const cs_9 = tail(_arg1);
                                            return strend(toks, empty(), cs_9);
                                        }
                                        else if (head(_arg1) === "=") {
                                            const cs_8 = tail(_arg1);
                                            toks_mut = cons(new Token(0), toks);
                                            _arg1_mut = cs_8;
                                            continue tokenize;
                                        }
                                        else {
                                            const cs_11 = _arg1;
                                            const arg10 = str(reverse_1(cs_11));
                                            return toFail(printf("Cannot tokenize: %s"))(arg10);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        break;
    }
}

export function strend(toks_mut, acc_mut, _arg2_mut) {
    strend:
    while (true) {
        const toks = toks_mut, acc = acc_mut, _arg2 = _arg2_mut;
        if (isEmpty(_arg2)) {
            throw (new Error("End of string not found"));
        }
        else if (head(_arg2) === "\"") {
            const cs = tail(_arg2);
            return tokenize(cons(new Token(5, str(acc)), toks), cs);
        }
        else {
            const c = head(_arg2);
            const cs_1 = tail(_arg2);
            toks_mut = toks;
            acc_mut = cons(c, acc);
            _arg2_mut = cs_1;
            continue strend;
        }
        break;
    }
}

export function ident(toks_mut, acc_mut, _arg3_mut) {
    let cs, c;
    ident:
    while (true) {
        const toks = toks_mut, acc = acc_mut, _arg3 = _arg3_mut;
        let pattern_matching_result, c_1, cs_1;
        if (!isEmpty(_arg3)) {
            if ((cs = tail(_arg3), (c = head(_arg3), isLetter(c)))) {
                pattern_matching_result = 0;
                c_1 = head(_arg3);
                cs_1 = tail(_arg3);
            }
            else {
                pattern_matching_result = 1;
            }
        }
        else {
            pattern_matching_result = 1;
        }
        switch (pattern_matching_result) {
            case 0: {
                toks_mut = toks;
                acc_mut = cons(c_1, acc);
                _arg3_mut = cs_1;
                continue ident;
            }
            case 1: {
                let pattern_matching_result_1, input, input_1;
                if (!isEmpty(_arg3)) {
                    if (head(_arg3) === "$") {
                        pattern_matching_result_1 = 0;
                        input = tail(_arg3);
                    }
                    else {
                        pattern_matching_result_1 = 1;
                        input_1 = _arg3;
                    }
                }
                else {
                    pattern_matching_result_1 = 1;
                    input_1 = _arg3;
                }
                switch (pattern_matching_result_1) {
                    case 0: {
                        return tokenize(cons(new Token(1, str(cons("$", acc))), toks), input);
                    }
                    case 1: {
                        return tokenize(cons(new Token(1, str(acc)), toks), input_1);
                    }
                }
            }
        }
        break;
    }
}

export function number(toks_mut, acc_mut, _arg4_mut) {
    let cs_2, cs, c;
    number:
    while (true) {
        const toks = toks_mut, acc = acc_mut, _arg4 = _arg4_mut;
        let pattern_matching_result, c_1, cs_1;
        if (!isEmpty(_arg4)) {
            if ((cs = tail(_arg4), (c = head(_arg4), isNumber(c)))) {
                pattern_matching_result = 0;
                c_1 = head(_arg4);
                cs_1 = tail(_arg4);
            }
            else {
                pattern_matching_result = 1;
            }
        }
        else {
            pattern_matching_result = 1;
        }
        switch (pattern_matching_result) {
            case 0: {
                toks_mut = toks;
                acc_mut = cons(c_1, acc);
                _arg4_mut = cs_1;
                continue number;
            }
            case 1: {
                let pattern_matching_result_1, cs_3;
                if (!isEmpty(_arg4)) {
                    if (head(_arg4) === ".") {
                        if ((cs_2 = tail(_arg4), !contains(".", acc, {
                            Equals: (x, y) => (x === y),
                            GetHashCode: (x) => stringHash(x),
                        }))) {
                            pattern_matching_result_1 = 0;
                            cs_3 = tail(_arg4);
                        }
                        else {
                            pattern_matching_result_1 = 1;
                        }
                    }
                    else {
                        pattern_matching_result_1 = 1;
                    }
                }
                else {
                    pattern_matching_result_1 = 1;
                }
                switch (pattern_matching_result_1) {
                    case 0: {
                        toks_mut = toks;
                        acc_mut = cons(".", acc);
                        _arg4_mut = cs_3;
                        continue number;
                    }
                    case 1: {
                        const input = _arg4;
                        return tokenize(cons(new Token(4, parse(str(acc))), toks), input);
                    }
                }
            }
        }
        break;
    }
}

export function tokenizeString(s) {
    return tokenize(empty(), ofSeq(s));
}

export function parseBinary(left, _arg1) {
    let pattern_matching_result, o, toks, toks_2, toks_4;
    if (!isEmpty(_arg1)) {
        if (head(_arg1).tag === 2) {
            pattern_matching_result = 0;
            o = head(_arg1).fields[0];
            toks = tail(_arg1);
        }
        else if (head(_arg1).tag === 0) {
            pattern_matching_result = 1;
            toks_2 = tail(_arg1);
        }
        else {
            pattern_matching_result = 2;
            toks_4 = _arg1;
        }
    }
    else {
        pattern_matching_result = 2;
        toks_4 = _arg1;
    }
    switch (pattern_matching_result) {
        case 0: {
            const patternInput = parseExpr(toks);
            const toks_1 = patternInput[1];
            const right = patternInput[0];
            return [new Expression(2, o, left, right), toks_1];
        }
        case 1: {
            const patternInput_1 = parseExpr(toks_2);
            const toks_3 = patternInput_1[1];
            const right_1 = patternInput_1[0];
            return [new Expression(2, "=", left, right_1), toks_3];
        }
        case 2: {
            return [left, toks_4];
        }
    }
}

export function parseExpr(_arg2) {
    let pattern_matching_result, s, toks, n, toks_1, i, toks_2, toks_6, v, toks_7;
    if (!isEmpty(_arg2)) {
        if (head(_arg2).tag === 5) {
            pattern_matching_result = 0;
            s = head(_arg2).fields[0];
            toks = tail(_arg2);
        }
        else if (head(_arg2).tag === 4) {
            pattern_matching_result = 1;
            n = head(_arg2).fields[0];
            toks_1 = tail(_arg2);
        }
        else if (head(_arg2).tag === 1) {
            if (!isEmpty(tail(_arg2))) {
                if (head(tail(_arg2)).tag === 3) {
                    if (head(tail(_arg2)).fields[0] === "(") {
                        pattern_matching_result = 2;
                        i = head(_arg2).fields[0];
                        toks_2 = tail(tail(_arg2));
                    }
                    else {
                        pattern_matching_result = 3;
                        toks_6 = tail(_arg2);
                        v = head(_arg2).fields[0];
                    }
                }
                else {
                    pattern_matching_result = 3;
                    toks_6 = tail(_arg2);
                    v = head(_arg2).fields[0];
                }
            }
            else {
                pattern_matching_result = 3;
                toks_6 = tail(_arg2);
                v = head(_arg2).fields[0];
            }
        }
        else {
            pattern_matching_result = 4;
            toks_7 = _arg2;
        }
    }
    else {
        pattern_matching_result = 4;
        toks_7 = _arg2;
    }
    switch (pattern_matching_result) {
        case 0: {
            return parseBinary(new Expression(0, new Value(0, s)), toks);
        }
        case 1: {
            return parseBinary(new Expression(0, new Value(1, new Decimal(n))), toks_1);
        }
        case 2: {
            const patternInput = parseExpr(toks_2);
            const toks_3 = patternInput[1];
            const arg = patternInput[0];
            let pattern_matching_result_1, toks_4;
            if (!isEmpty(toks_3)) {
                if (head(toks_3).tag === 3) {
                    if (head(toks_3).fields[0] === ")") {
                        pattern_matching_result_1 = 0;
                        toks_4 = tail(toks_3);
                    }
                    else {
                        pattern_matching_result_1 = 1;
                    }
                }
                else {
                    pattern_matching_result_1 = 1;
                }
            }
            else {
                pattern_matching_result_1 = 1;
            }
            switch (pattern_matching_result_1) {
                case 0: {
                    return [new Expression(3, i, arg), toks_4];
                }
                case 1: {
                    const patternInput_1 = parseBinary(arg, toks_3);
                    const toks_5 = patternInput_1[1];
                    const bin = patternInput_1[0];
                    return [new Expression(3, i, bin), toks_5];
                }
            }
        }
        case 3: {
            return parseBinary(new Expression(1, v), toks_6);
        }
        case 4: {
            return toFail(printf("Parsing expr failed. Unexpected: %A"))(toks_7);
        }
    }
}

export function parseInput(toks) {
    let ln, toks_1;
    const patternInput = (!isEmpty(toks)) ? ((head(toks).tag === 4) ? ((ln = head(toks).fields[0], (toks_1 = tail(toks), [~(~ln), toks_1]))) : [void 0, toks]) : [void 0, toks];
    const toks_2 = patternInput[1];
    const line = patternInput[0];
    let pattern_matching_result, lbl, var$, toks_3, toks_5, id, toks_7;
    if (!isEmpty(toks_2)) {
        if (head(toks_2).tag === 1) {
            if (head(toks_2).fields[0] === "RUN") {
                if (!isEmpty(tail(toks_2))) {
                    if (head(tail(toks_2)).tag === 0) {
                        pattern_matching_result = 6;
                        id = head(toks_2).fields[0];
                        toks_7 = tail(tail(toks_2));
                    }
                    else {
                        pattern_matching_result = 7;
                    }
                }
                else {
                    pattern_matching_result = 0;
                }
            }
            else if (head(toks_2).fields[0] === "LIST") {
                if (!isEmpty(tail(toks_2))) {
                    if (head(tail(toks_2)).tag === 0) {
                        pattern_matching_result = 6;
                        id = head(toks_2).fields[0];
                        toks_7 = tail(tail(toks_2));
                    }
                    else {
                        pattern_matching_result = 7;
                    }
                }
                else {
                    pattern_matching_result = 1;
                }
            }
            else if (head(toks_2).fields[0] === "GOTO") {
                if (!isEmpty(tail(toks_2))) {
                    if (head(tail(toks_2)).tag === 4) {
                        if (isEmpty(tail(tail(toks_2)))) {
                            pattern_matching_result = 2;
                            lbl = head(tail(toks_2)).fields[0];
                        }
                        else {
                            pattern_matching_result = 7;
                        }
                    }
                    else if (head(tail(toks_2)).tag === 0) {
                        pattern_matching_result = 6;
                        id = head(toks_2).fields[0];
                        toks_7 = tail(tail(toks_2));
                    }
                    else {
                        pattern_matching_result = 7;
                    }
                }
                else {
                    pattern_matching_result = 7;
                }
            }
            else if (head(toks_2).fields[0] === "INPUT") {
                if (!isEmpty(tail(toks_2))) {
                    if (head(tail(toks_2)).tag === 1) {
                        if (isEmpty(tail(tail(toks_2)))) {
                            pattern_matching_result = 3;
                            var$ = head(tail(toks_2)).fields[0];
                        }
                        else {
                            pattern_matching_result = 7;
                        }
                    }
                    else if (head(tail(toks_2)).tag === 0) {
                        pattern_matching_result = 6;
                        id = head(toks_2).fields[0];
                        toks_7 = tail(tail(toks_2));
                    }
                    else {
                        pattern_matching_result = 7;
                    }
                }
                else {
                    pattern_matching_result = 7;
                }
            }
            else if (head(toks_2).fields[0] === "IF") {
                pattern_matching_result = 4;
                toks_3 = tail(toks_2);
            }
            else if (head(toks_2).fields[0] === "PRINT") {
                pattern_matching_result = 5;
                toks_5 = tail(toks_2);
            }
            else if (!isEmpty(tail(toks_2))) {
                if (head(tail(toks_2)).tag === 0) {
                    pattern_matching_result = 6;
                    id = head(toks_2).fields[0];
                    toks_7 = tail(tail(toks_2));
                }
                else {
                    pattern_matching_result = 7;
                }
            }
            else {
                pattern_matching_result = 7;
            }
        }
        else {
            pattern_matching_result = 7;
        }
    }
    else {
        pattern_matching_result = 7;
    }
    switch (pattern_matching_result) {
        case 0: {
            return [line, new Command(2)];
        }
        case 1: {
            return [line, new Command(3)];
        }
        case 2: {
            return [line, new Command(1, ~(~lbl))];
        }
        case 3: {
            return [line, new Command(5, var$)];
        }
        case 4: {
            const patternInput_1 = parseExpr(toks_3);
            const toks_4 = patternInput_1[1];
            const arg1 = patternInput_1[0];
            let pattern_matching_result_1, ln_1;
            if (!isEmpty(toks_4)) {
                if (head(toks_4).tag === 1) {
                    if (head(toks_4).fields[0] === "GOTO") {
                        if (!isEmpty(tail(toks_4))) {
                            if (head(tail(toks_4)).tag === 4) {
                                if (isEmpty(tail(tail(toks_4)))) {
                                    pattern_matching_result_1 = 0;
                                    ln_1 = head(tail(toks_4)).fields[0];
                                }
                                else {
                                    pattern_matching_result_1 = 1;
                                }
                            }
                            else {
                                pattern_matching_result_1 = 1;
                            }
                        }
                        else {
                            pattern_matching_result_1 = 1;
                        }
                    }
                    else {
                        pattern_matching_result_1 = 1;
                    }
                }
                else {
                    pattern_matching_result_1 = 1;
                }
            }
            else {
                pattern_matching_result_1 = 1;
            }
            switch (pattern_matching_result_1) {
                case 0: {
                    return [line, new Command(6, arg1, ~(~ln_1))];
                }
                case 1: {
                    return toFail(printf("Parsing IF failed. Expected GOTO."));
                }
            }
        }
        case 5: {
            const patternInput_2 = parseExpr(toks_5);
            const toks_6 = patternInput_2[1];
            const arg = patternInput_2[0];
            if (!equals(toks_6, empty())) {
                toFail(printf("Parsing PRINT failed. Unexpected: %A"))(toks_6);
            }
            return [line, new Command(0, arg)];
        }
        case 6: {
            const patternInput_3 = parseExpr(toks_7);
            const toks_8 = patternInput_3[1];
            const arg_1 = patternInput_3[0];
            if (!equals(toks_8, empty())) {
                toFail(printf("Parsing = failed. Unexpected: %A"))(toks_8);
            }
            return [line, new Command(4, id, arg_1)];
        }
        case 7: {
            return toFail(printf("Parsing command failed. Unexpected: %A"))(toks_2);
        }
    }
}

