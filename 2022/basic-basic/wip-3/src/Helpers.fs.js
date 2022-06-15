import { fromContinuations } from "./.fable/fable-library.3.2.9/Async.js";
import { subscribe } from "./.fable/fable-library.3.2.9/Observable.js";
import { value } from "./.fable/fable-library.3.2.9/Option.js";
import Event$ from "./.fable/fable-library.3.2.9/Event.js";

export function awaitObservable(obs) {
    return fromContinuations((tupledArg) => {
        const cont = tupledArg[0];
        let sub = void 0;
        sub = subscribe((v) => {
            value(sub).Dispose();
            cont(v);
        }, obs);
    });
}

export const input = (() => {
    const inputEl = document.getElementById("in");
    const inputEvt = new Event$();
    inputEl.onkeypress = ((ke) => {
        if (ke.code === "Enter") {
            inputEvt.Trigger(inputEl.value);
            inputEl.value = "";
        }
    });
    return inputEvt.Publish;
})();

export function print(s) {
    document.getElementById("out").innerText = ((document.getElementById("out").innerText + s) + "\n");
}

