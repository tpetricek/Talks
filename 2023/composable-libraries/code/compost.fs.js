import { Compost_createSvg, EventHandler, HorizontalAlign, VerticalAlign, Derived_Bar, Derived_Column, Derived_Font, Derived_StrokeColor, Derived_FillColor, Shape, Scale, categorical, Value, continuous } from "./core.fs.js";
import { printf, toFail } from "./fable_modules/fable-library.4.1.4/String.js";
import { toList, map, delay, toArray } from "./fable_modules/fable-library.4.1.4/Seq.js";
import { ofArray } from "./fable_modules/fable-library.4.1.4/List.js";
import { defaultOf, equals } from "./fable_modules/fable-library.4.1.4/Util.js";
import { renderTo, createVirtualDomApp, DomNode, DomAttribute } from "./html.fs.js";
import { map as map_1 } from "./fable_modules/fable-library.4.1.4/Array.js";

export function Helpers_formatValue(v) {
    if (v.tag === 1) {
        const v_1 = v.fields[0].fields[0];
        return v_1;
    }
    else {
        const r = v.fields[1];
        const c = v.fields[0].fields[0];
        return [c, r];
    }
}

export function Helpers_parseValue(v) {
    if ((typeof(v)=='number')) {
        return new Value(1, [new continuous(v)]);
    }
    else if (Array.isArray(v)) {
        const a = v;
        if (a.length !== 2) {
            toFail(printf("Cannot parse value: %A. Expected a number or an array with two elements."))(a);
        }
        if (!((typeof(a[1])=='number'))) {
            toFail(printf("Cannot parse value: %A. The second element should be a number."))(a);
        }
        return new Value(0, [new categorical(a[0]), a[1]]);
    }
    else {
        return toFail(printf("Cannot parse value: %A. Expected a number or an array with two elements."))(v);
    }
}

export const scale = {
    continuous(lo, hi) {
        return new Scale(0, [new continuous(lo), new continuous(hi)]);
    },
    categorical(cats) {
        return new Scale(1, [toArray(delay(() => map((c) => (new categorical(c)), cats)))]);
    },
};

export const compost = {
    scaleX(sc, sh) {
        return new Shape(3, [sc, void 0, sh]);
    },
    scaleY(sc_1, sh_1) {
        return new Shape(3, [void 0, sc_1, sh_1]);
    },
    scale(sx, sy, sh_2) {
        return new Shape(3, [sx, sy, sh_2]);
    },
    nestX(lx, hx, s) {
        return new Shape(4, [Helpers_parseValue(lx), Helpers_parseValue(hx), s]);
    },
    nestY(ly, hy, s_1) {
        return new Shape(5, [Helpers_parseValue(ly), Helpers_parseValue(hy), s_1]);
    },
    nest(lx_1, hx_1, ly_1, hy_1, s_2) {
        return new Shape(5, [Helpers_parseValue(ly_1), Helpers_parseValue(hy_1), new Shape(4, [Helpers_parseValue(lx_1), Helpers_parseValue(hx_1), s_2])]);
    },
    overlay(sh_3) {
        return new Shape(9, [ofArray(sh_3)]);
    },
    padding(t, r, b, l, s_3) {
        return new Shape(12, [[t, r, b, l], s_3]);
    },
    fillColor(c, s_4) {
        return Derived_FillColor(c, s_4);
    },
    strokeColor(c_1, s_5) {
        return Derived_StrokeColor(c_1, s_5);
    },
    font(f, c_2, s_6) {
        return Derived_Font(f, c_2, s_6);
    },
    column(xp, yp) {
        return Derived_Column(new categorical(xp), new continuous(yp));
    },
    bar(xp_1, yp_1) {
        return Derived_Bar(new continuous(xp_1), new categorical(yp_1));
    },
    bubble(xp_2, yp_2, w, h) {
        return new Shape(7, [Helpers_parseValue(xp_2), Helpers_parseValue(yp_2), w, h]);
    },
    text(xp_3, yp_3, t_1, s_7, r_1) {
        const r_2 = equals(r_1, defaultOf()) ? 0 : r_1;
        const s_8 = equals(s_7, defaultOf()) ? "" : s_7;
        const va = (s_8.indexOf("baseline") >= 0) ? (new VerticalAlign(0, [])) : ((s_8.indexOf("hanging") >= 0) ? (new VerticalAlign(2, [])) : (new VerticalAlign(1, [])));
        const ha = (s_8.indexOf("start") >= 0) ? (new HorizontalAlign(0, [])) : ((s_8.indexOf("end") >= 0) ? (new HorizontalAlign(2, [])) : (new HorizontalAlign(1, [])));
        return new Shape(1, [Helpers_parseValue(xp_3), Helpers_parseValue(yp_3), va, ha, r_2, t_1]);
    },
    shape(a) {
        return new Shape(8, [toList(delay(() => map((p) => [Helpers_parseValue(p[0]), Helpers_parseValue(p[1])], a)))]);
    },
    line(a_1) {
        return new Shape(6, [toList(delay(() => map((p_1) => [Helpers_parseValue(p_1[0]), Helpers_parseValue(p_1[1])], a_1)))]);
    },
    axes(a_2, s_9) {
        return new Shape(10, [a_2.indexOf("top") >= 0, a_2.indexOf("right") >= 0, a_2.indexOf("bottom") >= 0, a_2.indexOf("left") >= 0, s_9]);
    },
    on(o, s_10) {
        return new Shape(11, [toList(delay(() => map((k) => {
            let f_2;
            const f_1 = o[k];
            f_2 = ((args) => {
                f_1(...args);
            });
            switch (k) {
                case "mousedown":
                    return new EventHandler(2, [(me, tupledArg) => {
                        const x_19 = tupledArg[0];
                        const y = tupledArg[1];
                        f_2([Helpers_formatValue(x_19), Helpers_formatValue(y), me]);
                    }]);
                case "mouseup":
                    return new EventHandler(1, [(me_1, tupledArg_1) => {
                        const x_20 = tupledArg_1[0];
                        const y_1 = tupledArg_1[1];
                        f_2([Helpers_formatValue(x_20), Helpers_formatValue(y_1), me_1]);
                    }]);
                case "mousemove":
                    return new EventHandler(0, [(me_2, tupledArg_2) => {
                        const x_21 = tupledArg_2[0];
                        const y_2 = tupledArg_2[1];
                        f_2([Helpers_formatValue(x_21), Helpers_formatValue(y_2), me_2]);
                    }]);
                case "touchstart":
                    return new EventHandler(4, [(me_3, tupledArg_3) => {
                        const x_22 = tupledArg_3[0];
                        const y_3 = tupledArg_3[1];
                        f_2([Helpers_formatValue(x_22), Helpers_formatValue(y_3), me_3]);
                    }]);
                case "touchmove":
                    return new EventHandler(5, [(me_4, tupledArg_4) => {
                        const x_23 = tupledArg_4[0];
                        const y_4 = tupledArg_4[1];
                        f_2([Helpers_formatValue(x_23), Helpers_formatValue(y_4), me_4]);
                    }]);
                case "click":
                    return new EventHandler(3, [(me_5, tupledArg_5) => {
                        const x_24 = tupledArg_5[0];
                        const y_5 = tupledArg_5[1];
                        f_2([Helpers_formatValue(x_24), Helpers_formatValue(y_5), me_5]);
                    }]);
                case "mouseleave":
                    return new EventHandler(7, [(me_6) => {
                        f_2([me_6]);
                    }]);
                case "touchend":
                    return new EventHandler(6, [(me_7) => {
                        f_2([me_7]);
                    }]);
                default: {
                    const s_11 = k;
                    return toFail(printf("Unsupported event type \'%s\' passed to the \'on\' primitive."))(s_11);
                }
            }
        }, Object.keys(o)))), s_10]);
    },
    svg(w_1, h_1, shape) {
        return Compost_createSvg(false, false, w_1, h_1, shape);
    },
    html(tag, attrs, children) {
        const attrs_1 = toArray(delay(() => map((a_3) => {
            const p_2 = attrs[a_3];
            return ((typeof(p_2)) === "function") ? [a_3, new DomAttribute(0, [(e, h_2) => {
                p_2(...[e, h_2]);
            }])] : [a_3, new DomAttribute(1, [p_2])];
        }, Object.keys(attrs))));
        const children_1 = map_1((c_3) => {
            if ((typeof(c_3)) === "string") {
                return new DomNode(0, [c_3]);
            }
            else {
                return c_3;
            }
        }, children);
        return new DomNode(1, [defaultOf(), tag, attrs_1, children_1]);
    },
    interactive(id, init, update, render) {
        const render_1 = (t_2, s_12) => {
            const el = document.getElementById(id);
            const res = render(t_2, s_12);
            if (equals(res["constructor"], (new DomNode(0, [""]))["constructor"])) {
                return res;
            }
            else {
                return Compost_createSvg(false, false, el.clientWidth, el.clientHeight, res);
            }
        };
        createVirtualDomApp(id, init, render_1, update);
    },
    render(id_1, viz) {
        const el_1 = document.getElementById(id_1);
        const svg = Compost_createSvg(false, false, el_1.clientWidth, el_1.clientHeight, viz);
        renderTo(el_1, svg);
    },
};

