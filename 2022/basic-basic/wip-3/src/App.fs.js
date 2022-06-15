import { Command, Expression, Value } from "./Domain.fs.js";
import { ofArray } from "./.fable/fable-library.3.2.9/List.js";
import { fromParts } from "./.fable/fable-library.3.2.9/Decimal.js";
import { singleton } from "./.fable/fable-library.3.2.9/AsyncBuilder.js";
import { input, awaitObservable } from "./Helpers.fs.js";
import { tokenizeString, parseInput } from "./Parser.fs.js";
import { State, runInput } from "./Evaluator.fs.js";
import { startImmediate } from "./.fable/fable-library.3.2.9/Async.js";
import { empty } from "./.fable/fable-library.3.2.9/Map.js";

export const hello = ofArray([[10, new Command(0, new Expression(0, new Value(0, "HELLO WORLD")))], [20, new Command(1, 10)], [void 0, new Command(2)]]);

export const guess = ofArray([[10, new Command(4, "Q", new Expression(2, "+", new Expression(3, "INT", new Expression(2, "*", new Expression(3, "RND", new Expression(0, new Value(1, fromParts(1, 0, 0, false, 0)))), new Expression(0, new Value(1, fromParts(100, 0, 0, false, 0))))), new Expression(0, new Value(1, fromParts(1, 0, 0, false, 0)))))], [20, new Command(4, "N", new Expression(0, new Value(1, fromParts(0, 0, 0, false, 0))))], [30, new Command(0, new Expression(0, new Value(0, "GUESS A NUMBER BETWEEN 1 AND 100!")))], [40, new Command(5, "G")], [50, new Command(6, new Expression(2, "=", new Expression(1, "G"), new Expression(1, "Q")), 130)], [60, new Command(4, "N", new Expression(2, "+", new Expression(1, "N"), new Expression(0, new Value(1, fromParts(1, 0, 0, false, 0)))))], [70, new Command(6, new Expression(2, "=", new Expression(1, "N"), new Expression(0, new Value(1, fromParts(7, 0, 0, false, 0)))), 120)], [80, new Command(6, new Expression(2, "\u003c", new Expression(1, "G"), new Expression(1, "Q")), 100)], [90, new Command(6, new Expression(2, "\u003e", new Expression(1, "G"), new Expression(1, "Q")), 110)], [100, new Command(0, new Expression(0, new Value(0, "NOT ENOUGH! TRY AGAIN")))], [101, new Command(1, 30)], [110, new Command(0, new Expression(0, new Value(0, "TOO MUCH! TRY AGAIN")))], [111, new Command(1, 30)], [120, new Command(0, new Expression(0, new Value(0, "YOU LOST!")))], [121, new Command(1, 150)], [130, new Command(0, new Expression(0, new Value(0, "YOU WON!")))], [150, new Command(0, new Expression(0, new Value(0, "THANKS FOR PLAYING")))], [void 0, new Command(3)]]);

export const lines = ofArray(["10 Q=1+INT(100*RND(1))", "20 N=0", "30 PRINT \"GUESS A NUMBER BETWEEN 1 AND 100!\"", "40 INPUT G", "50 IF G=Q GOTO 130", "60 N=N+1", "70 IF N=7 GOTO 120", "80 IF G\u003cQ GOTO 100", "90 IF G\u003eQ GOTO 110", "100 PRINT \"NOT ENOUGH! TRY AGAIN\"", "101 GOTO 30", "110 PRINT \"TOO MUCH! TRY AGAIN\"", "111 GOTO 30", "120 PRINT \"YOU LOST!\"", "121 GOTO 150", "130 PRINT \"YOU WON!\"", "150 PRINT \"THANKS FOR PLAYING\""]);

export function loop(state) {
    return singleton.Delay(() => singleton.Bind(awaitObservable(input), (_arg1) => {
        const cmd = _arg1;
        const cmd_1 = parseInput(tokenizeString(cmd.split("")));
        return singleton.Bind(runInput(state, cmd_1[0], cmd_1[1]), (_arg2) => {
            const state_1 = _arg2;
            return singleton.ReturnFrom(loop(state_1));
        });
    }));
}

startImmediate(singleton.Delay(() => {
    let state = new State(empty(), empty());
    return singleton.Combine(singleton.For(lines, (_arg1) => {
        const line = _arg1;
        const cmd = parseInput(tokenizeString(line.split("")));
        return singleton.Bind(runInput(state, cmd[0], cmd[1]), (_arg2) => {
            const newState = _arg2;
            state = newState;
            return singleton.Zero();
        });
    }), singleton.Delay(() => singleton.ReturnFrom(loop(state))));
}));

