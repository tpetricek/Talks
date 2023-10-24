import { Value, categorical, Compost_createSvg } from "./core.fs.js";
import { createVirtualDomApp, renderTo } from "./html.fs.js";
import { singleton, collect, delay } from "./fable_modules/fable-library.4.1.4/Seq.js";
import { nonSeeded } from "./fable_modules/fable-library.4.1.4/Random.js";

export function render(viz) {
    const el = document.getElementById("out");
    const svg_1 = Compost_createSvg(false, false, el.clientWidth, el.clientHeight, viz);
    renderTo(el, svg_1);
}

export function renderAnim(init, render_1, update) {
    createVirtualDomApp("out", init, render_1, update);
}

export function svg(shape) {
    const el = document.getElementById("out");
    return Compost_createSvg(false, false, el.clientWidth, el.clientHeight - 50, shape);
}

export function series(d) {
    return Array.from(delay(() => collect((matchValue) => {
        const y = matchValue[1];
        const x = matchValue[0];
        return singleton([x, y]);
    }, d)));
}

export const rnd = nonSeeded();

export function catv(n, s) {
    return new Value(0, [new categorical(s), n]);
}

export function ca(s) {
    return new categorical(s);
}

