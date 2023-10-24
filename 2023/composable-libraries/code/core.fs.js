import { toString, Record, Union } from "./fable_modules/fable-library.4.1.4/Types.js";
import { bool_type, unit_type, record_type, option_type, lambda_type, array_type, class_type, tuple_type, float64_type, union_type, string_type, int32_type } from "./fable_modules/fable-library.4.1.4/Reflection.js";
import { insert, substring, toFail, split, replace, printf, toText, join } from "./fable_modules/fable-library.4.1.4/String.js";
import { max as max_2, min as min_2, ofArray, singleton as singleton_1, cons, empty, reverse } from "./fable_modules/fable-library.4.1.4/List.js";
import { findIndex, fold, append as append_1, map } from "./fable_modules/fable-library.4.1.4/Array.js";
import { curry3, uncurry2, curry2, defaultOf, comparePrimitives, safeHash, equals, max as max_1, compare, min as min_1, round, int32ToString, disposeSafe, getEnumerator } from "./fable_modules/fable-library.4.1.4/Util.js";
import { op_EqualsBangGreater, h as h_2, text, s as s_6, El_op_Dynamic_Z451691CD, op_EqualsGreater, DomNode_$reflection } from "./html.fs.js";
import { forAll, exists, head, max as max_3, min as min_3, skip, toArray, filter, tryHead, isEmpty, map as map_1, empty as empty_1, collect, singleton, append, toList, delay } from "./fable_modules/fable-library.4.1.4/Seq.js";
import { max, min } from "./fable_modules/fable-library.4.1.4/Double.js";
import { newGuid } from "./fable_modules/fable-library.4.1.4/Guid.js";
import { defaultArg } from "./fable_modules/fable-library.4.1.4/Option.js";
import { rangeDouble } from "./fable_modules/fable-library.4.1.4/Range.js";
import { Array_distinct } from "./fable_modules/fable-library.4.1.4/Seq2.js";
import { FSharpChoice$3 } from "./fable_modules/fable-library.4.1.4/Choice.js";

export class Color extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["RGB", "HTML"];
    }
}

export function Color_$reflection() {
    return union_type("Compost.Color", [], Color, () => [[["Item1", int32_type], ["Item2", int32_type], ["Item3", int32_type]], [["Item", string_type]]]);
}

export class Width extends Union {
    constructor(Item) {
        super();
        this.tag = 0;
        this.fields = [Item];
    }
    cases() {
        return ["Pixels"];
    }
}

export function Width_$reflection() {
    return union_type("Compost.Width", [], Width, () => [[["Item", int32_type]]]);
}

export class FillStyle extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Solid", "LinearGradient"];
    }
}

export function FillStyle_$reflection() {
    return union_type("Compost.FillStyle", [], FillStyle, () => [[["Item", tuple_type(float64_type, Color_$reflection())]], [["Item", class_type("System.Collections.Generic.IEnumerable`1", [tuple_type(float64_type, tuple_type(float64_type, Color_$reflection()))])]]]);
}

export class Number$ extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Integer", "Percentage"];
    }
}

export function Number$_$reflection() {
    return union_type("Compost.Number", [], Number$, () => [[["Item", int32_type]], [["Item", float64_type]]]);
}

export class HorizontalAlign extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Start", "Center", "End"];
    }
}

export function HorizontalAlign_$reflection() {
    return union_type("Compost.HorizontalAlign", [], HorizontalAlign, () => [[], [], []]);
}

export class VerticalAlign extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Baseline", "Middle", "Hanging"];
    }
}

export function VerticalAlign_$reflection() {
    return union_type("Compost.VerticalAlign", [], VerticalAlign, () => [[], [], []]);
}

export class continuous extends Union {
    constructor(Item) {
        super();
        this.tag = 0;
        this.fields = [Item];
    }
    cases() {
        return ["CO"];
    }
}

export function continuous_$reflection(gen0) {
    return union_type("Compost.continuous", [gen0], continuous, () => [[["Item", float64_type]]]);
}

export class categorical extends Union {
    constructor(Item) {
        super();
        this.tag = 0;
        this.fields = [Item];
    }
    cases() {
        return ["CA"];
    }
}

export function categorical_$reflection(gen0) {
    return union_type("Compost.categorical", [gen0], categorical, () => [[["Item", string_type]]]);
}

export class Value extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["CAR", "COV"];
    }
}

export function Value_$reflection(gen0) {
    return union_type("Compost.Value", [gen0], Value, () => [[["Item1", categorical_$reflection(gen0)], ["Item2", float64_type]], [["Item", continuous_$reflection(gen0)]]]);
}

export class Scale extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Continuous", "Categorical"];
    }
}

export function Scale_$reflection(gen0) {
    return union_type("Compost.Scale", [gen0], Scale, () => [[["Item1", continuous_$reflection(gen0)], ["Item2", continuous_$reflection(gen0)]], [["Item", array_type(categorical_$reflection(gen0))]]]);
}

export class Style extends Record {
    constructor(StrokeColor, StrokeWidth, StrokeDashArray, Fill, Animation, Font, Cursor, FormatAxisXLabel, FormatAxisYLabel) {
        super();
        this.StrokeColor = StrokeColor;
        this.StrokeWidth = StrokeWidth;
        this.StrokeDashArray = StrokeDashArray;
        this.Fill = Fill;
        this.Animation = Animation;
        this.Font = Font;
        this.Cursor = Cursor;
        this.FormatAxisXLabel = FormatAxisXLabel;
        this.FormatAxisYLabel = FormatAxisYLabel;
    }
}

export function Style_$reflection() {
    return record_type("Compost.Style", [], Style, () => [["StrokeColor", tuple_type(float64_type, Color_$reflection())], ["StrokeWidth", Width_$reflection()], ["StrokeDashArray", class_type("System.Collections.Generic.IEnumerable`1", [Number$_$reflection()])], ["Fill", FillStyle_$reflection()], ["Animation", option_type(tuple_type(int32_type, string_type, lambda_type(Style_$reflection(), Style_$reflection())))], ["Font", string_type], ["Cursor", string_type], ["FormatAxisXLabel", lambda_type(Scale_$reflection(class_type("Microsoft.FSharp.Core.CompilerServices.MeasureOne")), lambda_type(Value_$reflection(class_type("Microsoft.FSharp.Core.CompilerServices.MeasureOne")), string_type))], ["FormatAxisYLabel", lambda_type(Scale_$reflection(class_type("Microsoft.FSharp.Core.CompilerServices.MeasureOne")), lambda_type(Value_$reflection(class_type("Microsoft.FSharp.Core.CompilerServices.MeasureOne")), string_type))]]);
}

export class EventHandler extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["MouseMove", "MouseUp", "MouseDown", "Click", "TouchStart", "TouchMove", "TouchEnd", "MouseLeave"];
    }
}

export function EventHandler_$reflection(gen0, gen1) {
    return union_type("Compost.EventHandler", [gen0, gen1], EventHandler, () => [[["Item", lambda_type(class_type("Browser.Types.MouseEvent", void 0), lambda_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)), unit_type))]], [["Item", lambda_type(class_type("Browser.Types.MouseEvent", void 0), lambda_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)), unit_type))]], [["Item", lambda_type(class_type("Browser.Types.MouseEvent", void 0), lambda_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)), unit_type))]], [["Item", lambda_type(class_type("Browser.Types.MouseEvent", void 0), lambda_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)), unit_type))]], [["Item", lambda_type(class_type("Browser.Types.TouchEvent", void 0), lambda_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)), unit_type))]], [["Item", lambda_type(class_type("Browser.Types.TouchEvent", void 0), lambda_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)), unit_type))]], [["Item", lambda_type(class_type("Browser.Types.TouchEvent", void 0), unit_type)]], [["Item", lambda_type(class_type("Browser.Types.MouseEvent", void 0), unit_type)]]]);
}

export class Orientation extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Vertical", "Horizontal"];
    }
}

export function Orientation_$reflection() {
    return union_type("Compost.Orientation", [], Orientation, () => [[], []]);
}

export class Shape extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Style", "Text", "AutoScale", "InnerScale", "NestX", "NestY", "Line", "Bubble", "Shape", "Layered", "Axes", "Interactive", "Padding", "Offset"];
    }
}

export function Shape_$reflection(gen0, gen1) {
    return union_type("Compost.Shape", [gen0, gen1], Shape, () => [[["Item1", lambda_type(Style_$reflection(), Style_$reflection())], ["Item2", Shape_$reflection(gen0, gen1)]], [["Item1", Value_$reflection(gen0)], ["Item2", Value_$reflection(gen1)], ["Item3", VerticalAlign_$reflection()], ["Item4", HorizontalAlign_$reflection()], ["Item5", float64_type], ["Item6", string_type]], [["Item1", bool_type], ["Item2", bool_type], ["Item3", Shape_$reflection(gen0, gen1)]], [["Item1", option_type(Scale_$reflection(gen0))], ["Item2", option_type(Scale_$reflection(gen1))], ["Item3", Shape_$reflection(gen0, gen1)]], [["Item1", Value_$reflection(gen0)], ["Item2", Value_$reflection(gen0)], ["Item3", Shape_$reflection(gen0, gen1)]], [["Item1", Value_$reflection(gen1)], ["Item2", Value_$reflection(gen1)], ["Item3", Shape_$reflection(gen0, gen1)]], [["Item", class_type("System.Collections.Generic.IEnumerable`1", [tuple_type(Value_$reflection(gen0), Value_$reflection(gen1))])]], [["Item1", Value_$reflection(gen0)], ["Item2", Value_$reflection(gen1)], ["Item3", float64_type], ["Item4", float64_type]], [["Item", class_type("System.Collections.Generic.IEnumerable`1", [tuple_type(Value_$reflection(gen0), Value_$reflection(gen1))])]], [["Item", class_type("System.Collections.Generic.IEnumerable`1", [Shape_$reflection(gen0, gen1)])]], [["Item1", bool_type], ["Item2", bool_type], ["Item3", bool_type], ["Item4", bool_type], ["Item5", Shape_$reflection(gen0, gen1)]], [["Item1", class_type("System.Collections.Generic.IEnumerable`1", [EventHandler_$reflection(gen0, gen1)])], ["Item2", Shape_$reflection(gen0, gen1)]], [["Item1", tuple_type(float64_type, float64_type, float64_type, float64_type)], ["Item2", Shape_$reflection(gen0, gen1)]], [["Item1", tuple_type(float64_type, float64_type)], ["Item2", Shape_$reflection(gen0, gen1)]]]);
}

export class Svg_StringBuilder {
    constructor() {
        this.strs = empty();
    }
    toString() {
        const x = this;
        return join("", reverse(x.strs));
    }
}

export function Svg_StringBuilder_$reflection() {
    return class_type("Compost.Svg.StringBuilder", void 0, Svg_StringBuilder);
}

export function Svg_StringBuilder_$ctor() {
    return new Svg_StringBuilder();
}

export function Svg_StringBuilder__Append_Z721C83C5(x, s) {
    x.strs = cons(s, x.strs);
}

export class Svg_PathSegment extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["MoveTo", "LineTo"];
    }
}

export function Svg_PathSegment_$reflection() {
    return union_type("Compost.Svg.PathSegment", [], Svg_PathSegment, () => [[["Item", tuple_type(float64_type, float64_type)]], [["Item", tuple_type(float64_type, float64_type)]]]);
}

export class Svg_Svg extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Path", "Ellipse", "Rect", "Text", "Combine", "Empty"];
    }
}

export function Svg_Svg_$reflection() {
    return union_type("Compost.Svg.Svg", [], Svg_Svg, () => [[["Item1", array_type(Svg_PathSegment_$reflection())], ["Item2", string_type]], [["Item1", tuple_type(float64_type, float64_type)], ["Item2", tuple_type(float64_type, float64_type)], ["Item3", string_type]], [["Item1", tuple_type(float64_type, float64_type)], ["Item2", tuple_type(float64_type, float64_type)], ["Item3", string_type]], [["Item1", tuple_type(float64_type, float64_type)], ["Item2", string_type], ["Item3", float64_type], ["Item4", string_type]], [["Item", array_type(Svg_Svg_$reflection())]], []]);
}

export function Svg_mapSvg(f, _arg) {
    if (_arg.tag === 4) {
        const svgs = _arg.fields[0];
        return new Svg_Svg(4, [map((_arg_1) => Svg_mapSvg(f, _arg_1), svgs)]);
    }
    else {
        const svg = _arg;
        return f(svg);
    }
}

export function Svg_formatPath(path) {
    const sb = Svg_StringBuilder_$ctor();
    const enumerator = getEnumerator(path);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const ps = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            if (ps.tag === 1) {
                const y_1 = ps.fields[0][1];
                const x_1 = ps.fields[0][0];
                Svg_StringBuilder__Append_Z721C83C5(sb, ((("L" + x_1.toString()) + " ") + y_1.toString()) + " ");
            }
            else {
                const y = ps.fields[0][1];
                const x = ps.fields[0][0];
                Svg_StringBuilder__Append_Z721C83C5(sb, ((("M" + x.toString()) + " ") + y.toString()) + " ");
            }
        }
    }
    finally {
        disposeSafe(enumerator);
    }
    return toString(sb);
}

export class Svg_RenderingContext extends Record {
    constructor(Definitions) {
        super();
        this.Definitions = Definitions;
    }
}

export function Svg_RenderingContext_$reflection() {
    return record_type("Compost.Svg.RenderingContext", [], Svg_RenderingContext, () => [["Definitions", array_type(DomNode_$reflection())]]);
}

export function Svg_renderSvg(ctx, svg) {
    return delay(() => {
        switch (svg.tag) {
            case 3: {
                const y = svg.fields[0][1];
                const x = svg.fields[0][0];
                const t = svg.fields[1];
                const style = svg.fields[3];
                const rotation = svg.fields[2];
                const attrs = toList(delay(() => append(singleton(op_EqualsGreater("style", style)), delay(() => ((rotation === 0) ? append(singleton(op_EqualsGreater("x", x.toString())), delay(() => singleton(op_EqualsGreater("y", y.toString())))) : append(singleton(op_EqualsGreater("x", "0")), delay(() => append(singleton(op_EqualsGreater("y", "0")), delay(() => singleton(op_EqualsGreater("transform", toText(printf("translate(%f,%f) rotate(%f)"))(x)(y)(rotation))))))))))));
                return singleton(El_op_Dynamic_Z451691CD(s_6, "text")(attrs)(singleton_1(text(t))));
            }
            case 4: {
                const ss = svg.fields[0];
                return collect((s) => Svg_renderSvg(ctx, s), ss);
            }
            case 1: {
                const style_1 = svg.fields[2];
                const ry = svg.fields[1][1];
                const rx = svg.fields[1][0];
                const cy = svg.fields[0][1];
                const cx = svg.fields[0][0];
                const attrs_1 = ofArray([op_EqualsGreater("cx", cx.toString()), op_EqualsGreater("cy", cy.toString()), op_EqualsGreater("rx", rx.toString()), op_EqualsGreater("ry", ry.toString()), op_EqualsGreater("style", style_1)]);
                return singleton(El_op_Dynamic_Z451691CD(s_6, "ellipse")(attrs_1)(empty()));
            }
            case 2: {
                const y2 = svg.fields[1][1];
                const y1 = svg.fields[0][1];
                const x2 = svg.fields[1][0];
                const x1 = svg.fields[0][0];
                const style_2 = svg.fields[2];
                const matchValue = min(x1, x2);
                const t_1 = min(y1, y2);
                const l = matchValue;
                const w = Math.abs(x1 - x2);
                const h = Math.abs(y1 - y2);
                const attrs_2 = ofArray([op_EqualsGreater("x", l.toString()), op_EqualsGreater("y", t_1.toString()), op_EqualsGreater("width", w.toString()), op_EqualsGreater("height", h.toString()), op_EqualsGreater("style", style_2)]);
                return singleton(El_op_Dynamic_Z451691CD(s_6, "rect")(attrs_2)(empty()));
            }
            case 0: {
                const style_3 = svg.fields[1];
                const p = svg.fields[0];
                const attrs_3 = ofArray([op_EqualsGreater("d", Svg_formatPath(p)), op_EqualsGreater("style", style_3)]);
                return singleton(El_op_Dynamic_Z451691CD(s_6, "path")(attrs_3)(empty()));
            }
            default: {
                return empty_1();
            }
        }
    });
}

export function Svg_formatColor(_arg) {
    if (_arg.tag === 1) {
        const clr = _arg.fields[0];
        return clr;
    }
    else {
        const r = _arg.fields[0] | 0;
        const g = _arg.fields[1] | 0;
        const b = _arg.fields[2] | 0;
        return toText(printf("rgb(%d, %d, %d)"))(r)(g)(b);
    }
}

export function Svg_formatNumber(_arg) {
    if (_arg.tag === 1) {
        const p = _arg.fields[0];
        return p.toString() + "%";
    }
    else {
        const n = _arg.fields[0] | 0;
        return int32ToString(n);
    }
}

export function Svg_formatStyle(defs, style) {
    let copyOfStruct, inputRecord, patternInput_1, so, clr, sw, arg_11, matchValue_1, fo, clr_2, arg_19, points, id_1, copyOfStruct_1, arg_16;
    let patternInput;
    const matchValue = style.Animation;
    if (matchValue == null) {
        patternInput = [style, ""];
    }
    else {
        const ms = matchValue[0] | 0;
        const ease = matchValue[1];
        const anim = matchValue[2];
        const id = "anim_" + replace((copyOfStruct = newGuid(), copyOfStruct), "-", "");
        const fromstyle = Svg_formatStyle(defs, new Style(style.StrokeColor, style.StrokeWidth, style.StrokeDashArray, style.Fill, void 0, style.Font, style.Cursor, style.FormatAxisXLabel, style.FormatAxisYLabel));
        const tostyle = Svg_formatStyle(defs, (inputRecord = anim(style), new Style(inputRecord.StrokeColor, inputRecord.StrokeWidth, inputRecord.StrokeDashArray, inputRecord.Fill, void 0, inputRecord.Font, inputRecord.Cursor, inputRecord.FormatAxisXLabel, inputRecord.FormatAxisYLabel)));
        const arg_5 = El_op_Dynamic_Z451691CD(h_2, "style")(empty())(singleton_1(text(toText(printf("@keyframes %s { from { %s } to { %s } }"))(id)(fromstyle)(tostyle))));
        void (defs.push(arg_5));
        patternInput = [anim(style), toText(printf("animation: %s %dms %s; "))(id)(ms)(ease)];
    }
    const style_1 = patternInput[0];
    const anim_1 = patternInput[1];
    return ((((anim_1 + join("", toList(delay(() => map_1((c) => (("cursor:" + c) + ";"), split(style_1.Cursor, [","], void 0, 0)))))) + (("font:" + style_1.Font) + ";")) + ((patternInput_1 = style_1.StrokeColor, (so = patternInput_1[0], (clr = patternInput_1[1], (sw = (style_1.StrokeWidth.fields[0] | 0), (arg_11 = Svg_formatColor(clr), toText(printf("stroke-opacity:%f; stroke-width:%dpx; stroke:%s; "))(so)(sw)(arg_11)))))))) + (isEmpty(style_1.StrokeDashArray) ? "" : (("stroke-dasharray:" + join(",", map_1(Svg_formatNumber, style_1.StrokeDashArray))) + ";"))) + ((matchValue_1 = style_1.Fill, (matchValue_1.tag === 0) ? ((fo = matchValue_1.fields[0][0], (clr_2 = matchValue_1.fields[0][1], (arg_19 = Svg_formatColor(clr_2), toText(printf("fill-opacity:%f; fill:%s; "))(fo)(arg_19))))) : ((points = matchValue_1.fields[0], (id_1 = ("gradient_" + replace((copyOfStruct_1 = newGuid(), copyOfStruct_1), "-", "")), ((arg_16 = El_op_Dynamic_Z451691CD(s_6, "linearGradient")(singleton_1(op_EqualsGreater("id", id_1)))(toList(delay(() => collect((matchValue_2) => {
        const pt = matchValue_2[0];
        const o = matchValue_2[1][0];
        const clr_1 = matchValue_2[1][1];
        return singleton(El_op_Dynamic_Z451691CD(s_6, "stop")(ofArray([op_EqualsGreater("offset", pt.toString() + "%"), op_EqualsGreater("stop-color", Svg_formatColor(clr_1)), op_EqualsGreater("stop-opacity", o.toString())]))(empty()));
    }, points)))), void (defs.push(arg_16))), toText(printf("fill:url(#%s)"))(id_1)))))));
}

export class Scales_ScaledShape extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["ScaledStyle", "ScaledText", "ScaledLine", "ScaledBubble", "ScaledShape", "ScaledLayered", "ScaledInteractive", "ScaledPadding", "ScaledOffset", "ScaledNestX", "ScaledNestY"];
    }
}

export function Scales_ScaledShape_$reflection(gen0, gen1) {
    return union_type("Compost.Scales.ScaledShape", [gen0, gen1], Scales_ScaledShape, () => [[["Item1", lambda_type(Style_$reflection(), Style_$reflection())], ["Item2", Scales_ScaledShape_$reflection(gen0, gen1)]], [["Item1", Value_$reflection(gen0)], ["Item2", Value_$reflection(gen1)], ["Item3", VerticalAlign_$reflection()], ["Item4", HorizontalAlign_$reflection()], ["Item5", float64_type], ["Item6", string_type]], [["Item", array_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)))]], [["Item1", Value_$reflection(gen0)], ["Item2", Value_$reflection(gen1)], ["Item3", float64_type], ["Item4", float64_type]], [["Item", array_type(tuple_type(Value_$reflection(gen0), Value_$reflection(gen1)))]], [["Item", array_type(Scales_ScaledShape_$reflection(gen0, gen1))]], [["Item1", class_type("System.Collections.Generic.IEnumerable`1", [EventHandler_$reflection(gen0, gen1)])], ["Item2", Scale_$reflection(gen0)], ["Item3", Scale_$reflection(gen1)], ["Item4", Scales_ScaledShape_$reflection(gen0, gen1)]], [["Item1", tuple_type(float64_type, float64_type, float64_type, float64_type)], ["Item2", Scale_$reflection(gen0)], ["Item3", Scale_$reflection(gen1)], ["Item4", Scales_ScaledShape_$reflection(gen0, gen1)]], [["Item1", tuple_type(float64_type, float64_type)], ["Item2", Scales_ScaledShape_$reflection(gen0, gen1)]], [["Item1", Value_$reflection(gen0)], ["Item2", Value_$reflection(gen0)], ["Item3", Scale_$reflection(gen0)], ["Item4", Scales_ScaledShape_$reflection(gen0, gen1)]], [["Item1", Value_$reflection(gen1)], ["Item2", Value_$reflection(gen1)], ["Item3", Scale_$reflection(gen1)], ["Item4", Scales_ScaledShape_$reflection(gen0, gen1)]]]);
}

export function Scales_getExtremes(_arg) {
    if (_arg.tag === 1) {
        const vals = _arg.fields[0];
        return [new Value(0, [vals[0], 0]), new Value(0, [vals[vals.length - 1], 1])];
    }
    else {
        const l = _arg.fields[0];
        const h = _arg.fields[1];
        return [new Value(1, [l]), new Value(1, [h])];
    }
}

/**
 * Given a range, return a new aligned range together with the magnitude
 */
export function Scales_calculateMagnitudeAndRange(lo, hi) {
    const magnitude = Math.pow(10, round(Math.log10(hi - lo)));
    const magnitude_1 = magnitude / 2;
    return [magnitude_1, [Math.floor(lo / magnitude_1) * magnitude_1, Math.ceil(hi / magnitude_1) * magnitude_1]];
}

/**
 * Get number of decimal points to show for the given range
 */
export function Scales_decimalPoints(range_, range__1) {
    const range = [range_, range__1];
    const magnitude = Scales_calculateMagnitudeAndRange(range[0], range[1])[0];
    return max(0, Math.ceil(-Math.log10(magnitude)));
}

/**
 * Extend the given range to a nicely adjusted size
 */
export function Scales_adjustRange(range_, range__1) {
    const range = [range_, range__1];
    return Scales_calculateMagnitudeAndRange(range[0], range[1])[1];
}

export function Scales_adjustRangeUnits(l, h) {
    const patternInput = Scales_adjustRange(l, h);
    const l_1 = patternInput[0];
    const h_1 = patternInput[1];
    return [l_1, h_1];
}

export function Scales_toArray(s) {
    return Array.from(s);
}

/**
 * Generate points for a grid. Count specifies how many points to generate
 * (this is minimm - the result will be up to 5x more).
 */
export function Scales_generateSteps(count, k, lo, hi) {
    const patternInput = Scales_calculateMagnitudeAndRange(lo, hi);
    const nlo = patternInput[1][0];
    const nhi = patternInput[1][1];
    const magnitude = patternInput[0];
    const dividers = ofArray([0.2, 0.5, 1, 2, 5, 10, 20, 40, 50, 60, 80, 100]);
    const magnitudes = map_1((d) => (magnitude / d), dividers);
    const step = tryHead(filter((m) => (((hi - lo) / m) >= count), magnitudes));
    const step_1 = defaultArg(step, magnitude / 100);
    return Scales_toArray(delay(() => collect((v) => (((v >= lo) && (v <= hi)) ? singleton(v) : empty_1()), rangeDouble(nlo, step_1 * k, nhi))));
}

export function Scales_generateAxisSteps(s) {
    if (s.tag === 1) {
        const vs = s.fields[0];
        return toArray(delay(() => collect((matchValue) => {
            const s_1 = matchValue.fields[0];
            return singleton(new Value(0, [new categorical(s_1), 0.5]));
        }, vs)));
    }
    else {
        const l = s.fields[0].fields[0];
        const h = s.fields[1].fields[0];
        return map((f) => (new Value(1, [new continuous(f)])), Scales_generateSteps(6, 1, l, h));
    }
}

export function Scales_generateAxisLabels(fmt, s) {
    const sunit = s;
    if (s.tag === 1) {
        const vs = s.fields[0];
        return toArray(delay(() => collect((matchValue) => {
            const v = matchValue;
            const s_1 = matchValue.fields[0];
            return singleton([new Value(0, [new categorical(s_1), 0.5]), fmt(sunit, new Value(0, [new categorical(s_1), 0.5]))]);
        }, vs)));
    }
    else {
        const l = s.fields[0].fields[0];
        const h = s.fields[1].fields[0];
        return map((f) => [new Value(1, [new continuous(f)]), fmt(sunit, new Value(1, [new continuous(f)]))], Scales_generateSteps(6, 2, l, h));
    }
}

export function Scales_unionScales(s1, s2) {
    let matchResult, h1, h2, l1, l2, v1, v2;
    if (s1.tag === 1) {
        if (s2.tag === 1) {
            matchResult = 1;
            v1 = s1.fields[0];
            v2 = s2.fields[0];
        }
        else {
            matchResult = 2;
        }
    }
    else if (s2.tag === 0) {
        matchResult = 0;
        h1 = s1.fields[1];
        h2 = s2.fields[1];
        l1 = s1.fields[0];
        l2 = s2.fields[0];
    }
    else {
        matchResult = 2;
    }
    switch (matchResult) {
        case 0:
            return new Scale(0, [min_1(compare, l1, l2), max_1(compare, h1, h2)]);
        case 1:
            return new Scale(1, [Array_distinct(append_1(v1, v2), {
                Equals: equals,
                GetHashCode: safeHash,
            })]);
        default:
            throw new Error("Cannot union continuous with categorical");
    }
}

export function Scales_calculateShapeScale(vals) {
    const scales = fold((state, value) => {
        let matchResult, v, v_1, vs, x, x_1, xs;
        switch (state.tag) {
            case 1: {
                if (value.tag === 1) {
                    matchResult = 1;
                    v_1 = value.fields[0].fields[0];
                    vs = state.fields[0];
                }
                else {
                    matchResult = 4;
                }
                break;
            }
            case 2: {
                if (value.tag === 0) {
                    matchResult = 3;
                    x_1 = value.fields[0].fields[0];
                    xs = state.fields[0];
                }
                else {
                    matchResult = 4;
                }
                break;
            }
            default:
                if (value.tag === 0) {
                    matchResult = 2;
                    x = value.fields[0].fields[0];
                }
                else {
                    matchResult = 0;
                    v = value.fields[0].fields[0];
                }
        }
        switch (matchResult) {
            case 0:
                return new FSharpChoice$3(1, [singleton_1(v)]);
            case 1:
                return new FSharpChoice$3(1, [cons(v_1, vs)]);
            case 2:
                return new FSharpChoice$3(2, [singleton_1(x)]);
            case 3:
                return new FSharpChoice$3(2, [cons(x_1, xs)]);
            default:
                throw new Error("Values with mismatching scales");
        }
    }, new FSharpChoice$3(0, [void 0]), vals);
    switch (scales.tag) {
        case 1: {
            const vs_1 = scales.fields[0];
            return new Scale(0, [new continuous(min_2(vs_1, {
                Compare: comparePrimitives,
            })), new continuous(max_2(vs_1, {
                Compare: comparePrimitives,
            }))]);
        }
        case 2: {
            const xs_1 = scales.fields[0];
            return new Scale(1, [Array_distinct(toArray(delay(() => map_1((x_4) => (new categorical(x_4)), reverse(xs_1)))), {
                Equals: equals,
                GetHashCode: safeHash,
            })]);
        }
        default:
            throw new Error("No values for calculating a scale");
    }
}

export function Scales_calculateShapeScales(points) {
    const xs = map((tuple) => tuple[0], points);
    const ys = map((tuple_1) => tuple_1[1], points);
    return [Scales_calculateShapeScale(xs), Scales_calculateShapeScale(ys)];
}

export function Scales_calculateScales(style, shape) {
    const calculateScalesStyle = Scales_calculateScales;
    const calculateScales = (shape_2) => Scales_calculateScales(style, shape_2);
    switch (shape.tag) {
        case 4: {
            const shape_5 = shape.fields[2];
            const nx2 = shape.fields[1];
            const nx1 = shape.fields[0];
            const patternInput_1 = calculateScales(shape_5);
            const shape_6 = patternInput_1[1];
            const isy = patternInput_1[0][1];
            const isx = patternInput_1[0][0];
            return [[Scales_calculateShapeScale([nx1, nx2]), isy], new Scales_ScaledShape(9, [nx1, nx2, isx, shape_6])];
        }
        case 5: {
            const shape_7 = shape.fields[2];
            const ny2 = shape.fields[1];
            const ny1 = shape.fields[0];
            const patternInput_2 = calculateScales(shape_7);
            const shape_8 = patternInput_2[1];
            const isy_1 = patternInput_2[0][1];
            const isx_1 = patternInput_2[0][0];
            return [[isx_1, Scales_calculateShapeScale([ny1, ny2])], new Scales_ScaledShape(10, [ny1, ny2, isy_1, shape_8])];
        }
        case 3: {
            const sy = shape.fields[1];
            const sx = shape.fields[0];
            const shape_9 = shape.fields[2];
            const patternInput_3 = calculateScales(shape_9);
            const shape_10 = patternInput_3[1];
            const isy_2 = patternInput_3[0][1];
            const isx_2 = patternInput_3[0][0];
            let sx_2;
            if (sx != null) {
                const sx_1 = sx;
                sx_2 = sx_1;
            }
            else {
                sx_2 = isx_2;
            }
            let sy_2;
            if (sy != null) {
                const sy_1 = sy;
                sy_2 = sy_1;
            }
            else {
                sy_2 = isy_2;
            }
            return [[sx_2, sy_2], shape_10];
        }
        case 2: {
            const shape_11 = shape.fields[2];
            const ay = shape.fields[1];
            const ax = shape.fields[0];
            const patternInput_4 = calculateScales(shape_11);
            const shape_12 = patternInput_4[1];
            const isy_3 = patternInput_4[0][1];
            const isx_3 = patternInput_4[0][0];
            const autoScale = (_arg) => {
                if (_arg.tag === 0) {
                    const l = _arg.fields[0].fields[0];
                    const h = _arg.fields[1].fields[0];
                    const patternInput_5 = Scales_adjustRangeUnits(l, h);
                    const l_1 = patternInput_5[0];
                    const h_1 = patternInput_5[1];
                    return new Scale(0, [new continuous(l_1), new continuous(h_1)]);
                }
                else {
                    const scale = _arg;
                    return scale;
                }
            };
            const scales_1 = [ax ? autoScale(isx_3) : isx_3, ay ? autoScale(isy_3) : isy_3];
            return [scales_1, shape_12];
        }
        case 13: {
            const shape_13 = shape.fields[1];
            const offs = shape.fields[0];
            const patternInput_6 = calculateScales(shape_13);
            const shape_14 = patternInput_6[1];
            const scales_2 = patternInput_6[0];
            return [scales_2, new Scales_ScaledShape(8, [offs, shape_14])];
        }
        case 12: {
            const shape_15 = shape.fields[1];
            const pads = shape.fields[0];
            const patternInput_7 = calculateScales(shape_15);
            const sy_3 = patternInput_7[0][1];
            const sx_3 = patternInput_7[0][0];
            const shape_16 = patternInput_7[1];
            return [[sx_3, sy_3], new Scales_ScaledShape(7, [pads, sx_3, sy_3, shape_16])];
        }
        case 7: {
            const y = shape.fields[1];
            const x = shape.fields[0];
            const ry = shape.fields[3];
            const rx = shape.fields[2];
            const makeSingletonScale = (_arg_1) => {
                if (_arg_1.tag === 0) {
                    const v_1 = _arg_1.fields[0];
                    return new Scale(1, [[v_1]]);
                }
                else {
                    const v = _arg_1.fields[0];
                    return new Scale(0, [v, v]);
                }
            };
            const scales_3 = [makeSingletonScale(x), makeSingletonScale(y)];
            return [scales_3, new Scales_ScaledShape(3, [x, y, rx, ry])];
        }
        case 1: {
            const y_1 = shape.fields[1];
            const x_1 = shape.fields[0];
            const va = shape.fields[2];
            const t = shape.fields[5];
            const r = shape.fields[4];
            const ha = shape.fields[3];
            const makeSingletonScale_1 = (_arg_2) => {
                if (_arg_2.tag === 0) {
                    const v_3 = _arg_2.fields[0];
                    return new Scale(1, [[v_3]]);
                }
                else {
                    const v_2 = _arg_2.fields[0];
                    return new Scale(0, [v_2, v_2]);
                }
            };
            const scales_4 = [makeSingletonScale_1(x_1), makeSingletonScale_1(y_1)];
            return [scales_4, new Scales_ScaledShape(1, [x_1, y_1, va, ha, r, t])];
        }
        case 6: {
            const line = shape.fields[0];
            const line_1 = toArray(line);
            const scales_5 = Scales_calculateShapeScales(line_1);
            return [scales_5, new Scales_ScaledShape(2, [line_1])];
        }
        case 8: {
            const points = shape.fields[0];
            const points_1 = toArray(points);
            const scales_6 = Scales_calculateShapeScales(points_1);
            return [scales_6, new Scales_ScaledShape(4, [points_1])];
        }
        case 10: {
            const showTop = shape.fields[0];
            const showRight = shape.fields[1];
            const showLeft = shape.fields[3];
            const showBottom = shape.fields[2];
            const shape_17 = shape.fields[4];
            const patternInput_8 = calculateScales(shape_17);
            const sy_4 = patternInput_8[0][1];
            const sx_4 = patternInput_8[0][0];
            const origScales = patternInput_8[0];
            const matchValue = Scales_getExtremes(sx_4);
            const matchValue_1 = Scales_getExtremes(sy_4);
            const ly = matchValue_1[0];
            const lx = matchValue[0];
            const hy = matchValue_1[1];
            const hx = matchValue[1];
            const LineStyle = (clr, alpha, width, shape_18) => (new Shape(0, [(s) => (new Style([alpha, new Color(1, [clr])], new Width(width), s.StrokeDashArray, new FillStyle(0, [[1, new Color(1, ["transparent"])]]), s.Animation, s.Font, s.Cursor, s.FormatAxisXLabel, s.FormatAxisYLabel)), shape_18]));
            const FontStyle = (style_2, shape_19) => (new Shape(0, [(s_1) => (new Style([0, new Color(1, ["transparent"])], s_1.StrokeWidth, s_1.StrokeDashArray, new FillStyle(0, [[1, new Color(1, ["black"])]]), s_1.Animation, style_2, s_1.Cursor, s_1.FormatAxisXLabel, s_1.FormatAxisYLabel)), shape_19]));
            const shape_20 = new Shape(9, [toList(delay(() => append(singleton(new Shape(3, [sx_4, sy_4, new Shape(9, [toList(delay(() => append(map_1((x_2) => LineStyle("#e4e4e4", 1, 1, new Shape(6, [[[x_2, ly], [x_2, hy]]])), Scales_generateAxisSteps(sx_4)), delay(() => map_1((y_2) => LineStyle("#e4e4e4", 1, 1, new Shape(6, [[[lx, y_2], [hx, y_2]]])), Scales_generateAxisSteps(sy_4))))))])])), delay(() => append(showTop ? append(singleton(LineStyle("black", 1, 2, new Shape(6, [[[lx, hy], [hx, hy]]]))), delay(() => collect((matchValue_2) => {
                const x_3 = matchValue_2[0];
                const l_2 = matchValue_2[1];
                return singleton(FontStyle("9pt sans-serif", new Shape(13, [[0, -10], new Shape(1, [x_3, hy, new VerticalAlign(0, []), new HorizontalAlign(1, []), 0, l_2])])));
            }, Scales_generateAxisLabels(style.FormatAxisXLabel, sx_4)))) : empty_1(), delay(() => append(showRight ? append(singleton(LineStyle("black", 1, 2, new Shape(6, [[[hx, hy], [hx, ly]]]))), delay(() => collect((matchValue_3) => {
                const y_3 = matchValue_3[0];
                const l_3 = matchValue_3[1];
                return singleton(FontStyle("9pt sans-serif", new Shape(13, [[10, 0], new Shape(1, [hx, y_3, new VerticalAlign(1, []), new HorizontalAlign(0, []), 0, l_3])])));
            }, Scales_generateAxisLabels(style.FormatAxisYLabel, sy_4)))) : empty_1(), delay(() => append(showBottom ? append(singleton(LineStyle("black", 1, 2, new Shape(6, [[[lx, ly], [hx, ly]]]))), delay(() => collect((matchValue_4) => {
                const x_4 = matchValue_4[0];
                const l_4 = matchValue_4[1];
                return singleton(FontStyle("9pt sans-serif", new Shape(13, [[0, 10], new Shape(1, [x_4, ly, new VerticalAlign(2, []), new HorizontalAlign(1, []), 0, l_4])])));
            }, Scales_generateAxisLabels(style.FormatAxisXLabel, sx_4)))) : empty_1(), delay(() => append(showLeft ? append(singleton(LineStyle("black", 1, 2, new Shape(6, [[[lx, hy], [lx, ly]]]))), delay(() => collect((matchValue_5) => {
                const y_4 = matchValue_5[0];
                const l_5 = matchValue_5[1];
                return singleton(FontStyle("9pt sans-serif", new Shape(13, [[-10, 0], new Shape(1, [lx, y_4, new VerticalAlign(1, []), new HorizontalAlign(2, []), 0, l_5])])));
            }, Scales_generateAxisLabels(style.FormatAxisYLabel, sy_4)))) : empty_1(), delay(() => singleton(shape_17)))))))))))))]);
            const padding = [showTop ? 30 : 0, showRight ? 50 : 0, showBottom ? 30 : 0, showLeft ? 50 : 0];
            return calculateScales(new Shape(12, [padding, shape_20]));
        }
        case 9: {
            const shapes = shape.fields[0];
            const shapes_1 = Array.from(shapes);
            const scaled = map(calculateScales, shapes_1);
            const sxs = map((tupledArg) => {
                const sx_5 = tupledArg[0][0];
                return sx_5;
            }, scaled);
            const sys = map((tupledArg_1) => {
                const sy_5 = tupledArg_1[0][1];
                return sy_5;
            }, scaled);
            const scales_7 = [sxs.reduce(Scales_unionScales), sys.reduce(Scales_unionScales)];
            return [scales_7, new Scales_ScaledShape(5, [map((tuple) => tuple[1], scaled)])];
        }
        case 11: {
            const shape_21 = shape.fields[1];
            const f_1 = shape.fields[0];
            const patternInput_10 = calculateScales(shape_21);
            const shape_22 = patternInput_10[1];
            const scales_8 = patternInput_10[0];
            return [scales_8, new Scales_ScaledShape(6, [f_1, scales_8[0], scales_8[1], shape_22])];
        }
        default: {
            const shape_3 = shape.fields[1];
            const f = shape.fields[0];
            const patternInput = calculateScalesStyle(f(style), shape_3);
            const shape_4 = patternInput[1];
            const scales = patternInput[0];
            return [scales, new Scales_ScaledShape(0, [f, shape_4])];
        }
    }
}

export function Projections_projectOne(reversed, tlv, thv, scale, coord) {
    if (scale.tag === 0) {
        if (coord.tag === 0) {
            return toFail(printf("Cannot project categorical value (%A) on a continuous scale (%A)."))(coord)(scale);
        }
        else {
            const shv = scale.fields[1].fields[0];
            const slv = scale.fields[0].fields[0];
            const v_1 = coord.fields[0].fields[0];
            if (reversed) {
                return thv - (((v_1 - slv) / (shv - slv)) * (thv - tlv));
            }
            else {
                return tlv + (((v_1 - slv) / (shv - slv)) * (thv - tlv));
            }
        }
    }
    else if (coord.tag === 1) {
        return toFail(printf("Cannot project continuous value (%A) on a categorical scale (%A)."))(coord)(scale);
    }
    else {
        const f = coord.fields[1];
        const v = coord.fields[0].fields[0];
        const vals = scale.fields[0];
        const size = (thv - tlv) / vals.length;
        const i = findIndex((_arg) => {
            const vv = _arg.fields[0];
            return v === vv;
        }, vals) | 0;
        const i_1 = i + f;
        if (reversed) {
            return thv - (i_1 * size);
        }
        else {
            return tlv + (i_1 * size);
        }
    }
}

export function Projections_projectOneX(a_, a__1) {
    const a = [a_, a__1];
    return (scale) => ((coord) => Projections_projectOne(false, a[0], a[1], scale, coord));
}

export function Projections_projectOneY(a_, a__1) {
    const a = [a_, a__1];
    return (scale) => ((coord) => Projections_projectOne(true, a[0], a[1], scale, coord));
}

export function Projections_projectInvOne(reversed, l, h, s, v) {
    if (s.tag === 1) {
        const cats = s.fields[0];
        const size = (h - l) / cats.length;
        const i = reversed ? Math.floor((h - v) / size) : Math.floor((v - l) / size);
        const f = reversed ? (((h - v) / size) - i) : (((v - l) / size) - i);
        const i_1 = (size < 0) ? (cats.length + i) : i;
        if ((~~i_1 < 0) ? true : (~~i_1 >= cats.length)) {
            return new Value(0, [new categorical("<outside-of-range>"), f]);
        }
        else {
            return new Value(0, [cats[~~i_1], f]);
        }
    }
    else {
        const slv = s.fields[0].fields[0];
        const shv = s.fields[1].fields[0];
        if (reversed) {
            return new Value(1, [new continuous(shv - (((v - l) / (h - l)) * (shv - slv)))]);
        }
        else {
            return new Value(1, [new continuous(slv + (((v - l) / (h - l)) * (shv - slv)))]);
        }
    }
}

export function Projections_projectInv(x1, y1, x2, y2, sx, sy, x, y) {
    return [Projections_projectInvOne(false, x1, x2, sx, x), Projections_projectInvOne(true, y1, y2, sy, y)];
}

export class Drawing_DrawingContext extends Record {
    constructor(Style, Definitions) {
        super();
        this.Style = Style;
        this.Definitions = Definitions;
    }
}

export function Drawing_DrawingContext_$reflection() {
    return record_type("Compost.Drawing.DrawingContext", [], Drawing_DrawingContext, () => [["Style", Style_$reflection()], ["Definitions", array_type(DomNode_$reflection())]]);
}

export function Drawing_hideFill(style) {
    let matchValue, n, f, e;
    return new Style(style.StrokeColor, style.StrokeWidth, style.StrokeDashArray, new FillStyle(0, [[0, new Color(0, [0, 0, 0])]]), (matchValue = style.Animation, (matchValue != null) ? ((n = (matchValue[0] | 0), (f = matchValue[2], (e = matchValue[1], [n, e, (arg) => Drawing_hideFill(f(arg))])))) : void 0), style.Font, style.Cursor, style.FormatAxisXLabel, style.FormatAxisYLabel);
}

export function Drawing_hideStroke(style) {
    let matchValue, n, f, e;
    return new Style([0, style.StrokeColor[1]], style.StrokeWidth, style.StrokeDashArray, style.Fill, (matchValue = style.Animation, (matchValue != null) ? ((n = (matchValue[0] | 0), (f = matchValue[2], (e = matchValue[1], [n, e, (arg) => Drawing_hideStroke(f(arg))])))) : void 0), style.Font, style.Cursor, style.FormatAxisXLabel, style.FormatAxisYLabel);
}

export function Drawing_drawShape(ctx_mut, area__mut, area__1_mut, area__2_mut, area__3_mut, scales__mut, scales__1_mut, shape_mut) {
    Drawing_drawShape:
    while (true) {
        const ctx = ctx_mut, area_ = area__mut, area__1 = area__1_mut, area__2 = area__2_mut, area__3 = area__3_mut, scales_ = scales__mut, scales__1 = scales__1_mut, shape = shape_mut;
        const area = [area_, area__1, area__2, area__3];
        const scales = [scales_, scales__1];
        const area_1 = area;
        const y2 = area_1[3];
        const y1 = area_1[1];
        const x2 = area_1[2];
        const x1 = area_1[0];
        const scales_1 = scales;
        const sy = scales_1[1];
        const sx = scales_1[0];
        const project = (tupledArg) => {
            const vx = tupledArg[0];
            const vy = tupledArg[1];
            return [Projections_projectOneX(x1, x2)(sx)(vx), Projections_projectOneY(y1, y2)(sy)(vy)];
        };
        switch (shape.tag) {
            case 10: {
                const shape_2 = shape.fields[3];
                const p2_1 = shape.fields[1];
                const p1_1 = shape.fields[0];
                const isy = shape.fields[2];
                const y1$0027 = Projections_projectOneY(y1, y2)(sy)(p1_1);
                const y2$0027 = Projections_projectOneY(y1, y2)(sy)(p2_1);
                ctx_mut = ctx;
                area__mut = x1;
                area__1_mut = y1$0027;
                area__2_mut = x2;
                area__3_mut = y2$0027;
                scales__mut = sx;
                scales__1_mut = isy;
                shape_mut = shape_2;
                continue Drawing_drawShape;
            }
            case 8: {
                const shape_3 = shape.fields[1];
                const dy = shape.fields[0][1];
                const dx = shape.fields[0][0];
                ctx_mut = ctx;
                area__mut = (x1 + dx);
                area__1_mut = (y1 + dy);
                area__2_mut = (x2 + dx);
                area__3_mut = (y2 + dy);
                scales__mut = scales_1[0];
                scales__1_mut = scales_1[1];
                shape_mut = shape_3;
                continue Drawing_drawShape;
            }
            case 5: {
                const shapes = shape.fields[0];
                return new Svg_Svg(4, [map((shape_4) => Drawing_drawShape(ctx, area_1[0], area_1[1], area_1[2], area_1[3], scales_1[0], scales_1[1], shape_4), shapes)]);
            }
            case 0: {
                const shape_5 = shape.fields[1];
                const f = shape.fields[0];
                ctx_mut = (new Drawing_DrawingContext(f(ctx.Style), ctx.Definitions));
                area__mut = area_1[0];
                area__1_mut = area_1[1];
                area__2_mut = area_1[2];
                area__3_mut = area_1[3];
                scales__mut = scales_1[0];
                scales__1_mut = scales_1[1];
                shape_mut = shape_5;
                continue Drawing_drawShape;
            }
            case 4: {
                const points = shape.fields[0];
                const path = toArray(delay(() => append(singleton(new Svg_PathSegment(0, [project(points[0])])), delay(() => append(map_1((pt) => (new Svg_PathSegment(1, [project(pt)])), skip(1, points)), delay(() => singleton(new Svg_PathSegment(1, [project(points[0])]))))))));
                return new Svg_Svg(0, [path, Svg_formatStyle(ctx.Definitions, Drawing_hideStroke(ctx.Style))]);
            }
            case 7: {
                const t = shape.fields[0][0];
                const shape_6 = shape.fields[3];
                const r = shape.fields[0][1];
                const l = shape.fields[0][3];
                const isy_1 = shape.fields[2];
                const isx_1 = shape.fields[1];
                const b = shape.fields[0][2];
                const calculateNestedRange = (rev) => ((tupledArg_1) => ((ins) => {
                    const v1 = tupledArg_1[0];
                    const v2 = tupledArg_1[1];
                    return (outs) => {
                        if (ins.tag === 1) {
                            const vals = ins.fields[0];
                            const pos_1 = collect((v) => ofArray([Projections_projectOne(rev, v1, v2, outs, new Value(0, [v, 0])), Projections_projectOne(rev, v1, v2, outs, new Value(0, [v, 1]))]), vals);
                            return [min_3(pos_1, {
                                Compare: comparePrimitives,
                            }), max_3(pos_1, {
                                Compare: comparePrimitives,
                            })];
                        }
                        else {
                            const l_1 = ins.fields[0].fields[0];
                            const h = ins.fields[1].fields[0];
                            const pos = ofArray([Projections_projectOne(rev, v1, v2, outs, new Value(1, [new continuous(l_1)])), Projections_projectOne(rev, v1, v2, outs, new Value(1, [new continuous(h)]))]);
                            return [min_3(pos, {
                                Compare: comparePrimitives,
                            }), max_3(pos, {
                                Compare: comparePrimitives,
                            })];
                        }
                    };
                }));
                const patternInput = calculateNestedRange(false)([x1, x2])(isx_1)(sx);
                const x2$0027_1 = patternInput[1];
                const x1$0027_1 = patternInput[0];
                const patternInput_1 = calculateNestedRange(true)([y1, y2])(isy_1)(sy);
                const y2$0027_1 = patternInput_1[1];
                const y1$0027_1 = patternInput_1[0];
                ctx_mut = ctx;
                area__mut = (x1$0027_1 + l);
                area__1_mut = (y1$0027_1 + t);
                area__2_mut = (x2$0027_1 - r);
                area__3_mut = (y2$0027_1 - b);
                scales__mut = isx_1;
                scales__1_mut = isy_1;
                shape_mut = shape_6;
                continue Drawing_drawShape;
            }
            case 2: {
                const line = shape.fields[0];
                const path_1 = Array.from(delay(() => append(singleton(new Svg_PathSegment(0, [project(head(line))])), delay(() => map_1((pt_1) => (new Svg_PathSegment(1, [project(pt_1)])), skip(1, line))))));
                return new Svg_Svg(0, [path_1, Svg_formatStyle(ctx.Definitions, Drawing_hideFill(ctx.Style))]);
            }
            case 1: {
                const y_4 = shape.fields[1];
                const x_4 = shape.fields[0];
                const va = shape.fields[2];
                const t_1 = shape.fields[5];
                const r_1 = shape.fields[4];
                const ha = shape.fields[3];
                const va_1 = (va.tag === 2) ? "hanging" : ((va.tag === 1) ? "middle" : "baseline");
                const ha_1 = (ha.tag === 1) ? "middle" : ((ha.tag === 2) ? "end" : "start");
                const xy = project([x_4, y_4]);
                return new Svg_Svg(3, [xy, t_1, r_1, toText(printf("alignment-baseline:%s; text-anchor:%s;"))(va_1)(ha_1) + Svg_formatStyle(ctx.Definitions, ctx.Style)]);
            }
            case 3: {
                const y_5 = shape.fields[1];
                const x_5 = shape.fields[0];
                const ry = shape.fields[3];
                const rx = shape.fields[2];
                return new Svg_Svg(1, [project([x_5, y_5]), [rx, ry], Svg_formatStyle(ctx.Definitions, ctx.Style)]);
            }
            case 6: {
                const shape_7 = shape.fields[3];
                const f_1 = shape.fields[0];
                ctx_mut = ctx;
                area__mut = area_1[0];
                area__1_mut = area_1[1];
                area__2_mut = area_1[2];
                area__3_mut = area_1[3];
                scales__mut = scales_1[0];
                scales__1_mut = scales_1[1];
                shape_mut = shape_7;
                continue Drawing_drawShape;
            }
            default: {
                const shape_1 = shape.fields[3];
                const p2 = shape.fields[1];
                const p1 = shape.fields[0];
                const isx = shape.fields[2];
                const x1$0027 = Projections_projectOneX(x1, x2)(sx)(p1);
                const x2$0027 = Projections_projectOneX(x1, x2)(sx)(p2);
                ctx_mut = ctx;
                area__mut = x1$0027;
                area__1_mut = y1;
                area__2_mut = x2$0027;
                area__3_mut = y2;
                scales__mut = isx;
                scales__1_mut = sy;
                shape_mut = shape_1;
                continue Drawing_drawShape;
            }
        }
        break;
    }
}

export class Events_MouseEventKind extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Click", "Move", "Up", "Down"];
    }
}

export function Events_MouseEventKind_$reflection() {
    return union_type("Compost.Events.MouseEventKind", [], Events_MouseEventKind, () => [[], [], [], []]);
}

export class Events_TouchEventKind extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Move", "Start"];
    }
}

export function Events_TouchEventKind_$reflection() {
    return union_type("Compost.Events.TouchEventKind", [], Events_TouchEventKind, () => [[], []]);
}

export class Events_InteractiveEvent extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["MouseEvent", "TouchEvent", "TouchEnd", "MouseLeave"];
    }
}

export function Events_InteractiveEvent_$reflection(gen0, gen1) {
    return union_type("Compost.Events.InteractiveEvent", [gen0, gen1], Events_InteractiveEvent, () => [[["Item1", Events_MouseEventKind_$reflection()], ["Item2", tuple_type(Value_$reflection(gen0), Value_$reflection(gen1))]], [["Item1", Events_TouchEventKind_$reflection()], ["Item2", tuple_type(Value_$reflection(gen0), Value_$reflection(gen1))]], [], []]);
}

export function Events_projectEvent(area_, area__1, area__2, area__3, scales_, scales__1, event) {
    const area = [area_, area__1, area__2, area__3];
    const scales = [scales_, scales__1];
    let matchResult, kind, x, y, kind_1, x_3, y_3;
    switch (event.tag) {
        case 1: {
            if (event.fields[1][0].tag === 1) {
                if (event.fields[1][1].tag === 1) {
                    matchResult = 1;
                    kind_1 = event.fields[0];
                    x_3 = event.fields[1][0].fields[0].fields[0];
                    y_3 = event.fields[1][1].fields[0].fields[0];
                }
                else {
                    matchResult = 2;
                }
            }
            else {
                matchResult = 2;
            }
            break;
        }
        case 2: {
            matchResult = 3;
            break;
        }
        case 3: {
            matchResult = 4;
            break;
        }
        default:
            if (event.fields[1][0].tag === 1) {
                if (event.fields[1][1].tag === 1) {
                    matchResult = 0;
                    kind = event.fields[0];
                    x = event.fields[1][0].fields[0].fields[0];
                    y = event.fields[1][1].fields[0].fields[0];
                }
                else {
                    matchResult = 2;
                }
            }
            else {
                matchResult = 2;
            }
    }
    switch (matchResult) {
        case 0:
            return new Events_InteractiveEvent(0, [kind, Projections_projectInv(area[0], area[1], area[2], area[3], scales[0], scales[1], x, y)]);
        case 1:
            return new Events_InteractiveEvent(1, [kind_1, Projections_projectInv(area[0], area[1], area[2], area[3], scales[0], scales[1], x_3, y_3)]);
        case 2:
            throw new Error("TODO: projectEvent - not continuous");
        case 3:
            return new Events_InteractiveEvent(2, []);
        default:
            return new Events_InteractiveEvent(3, []);
    }
}

export function Events_inScale(s, v) {
    if (s.tag === 1) {
        if (v.tag === 1) {
            throw new Error("inScale: Cannot test if continuous value is in categorical scale");
        }
        else {
            const cats = s.fields[0];
            const v_2 = v.fields[0];
            return exists((y) => equals(v_2, y), cats);
        }
    }
    else if (v.tag === 0) {
        throw new Error("inScale: Cannot test if categorical value is in continuous scale");
    }
    else {
        const h = s.fields[1].fields[0];
        const l = s.fields[0].fields[0];
        const v_1 = v.fields[0].fields[0];
        if (v_1 >= min(l, h)) {
            return v_1 <= max(l, h);
        }
        else {
            return false;
        }
    }
}

export function Events_inScales(sx, sy, event) {
    let matchResult, x, y;
    switch (event.tag) {
        case 2: {
            matchResult = 1;
            break;
        }
        case 0: {
            matchResult = 2;
            x = event.fields[1][0];
            y = event.fields[1][1];
            break;
        }
        case 1: {
            matchResult = 2;
            x = event.fields[1][0];
            y = event.fields[1][1];
            break;
        }
        default:
            matchResult = 0;
    }
    switch (matchResult) {
        case 0:
            return true;
        case 1:
            return true;
        default:
            if (Events_inScale(sx, x)) {
                return Events_inScale(sy, y);
            }
            else {
                return false;
            }
    }
}

export function Events_triggerEvent(area__mut, area__1_mut, area__2_mut, area__3_mut, scales__mut, scales__1_mut, shape_mut, jse_mut, event_mut) {
    Events_triggerEvent:
    while (true) {
        const area_ = area__mut, area__1 = area__1_mut, area__2 = area__2_mut, area__3 = area__3_mut, scales_ = scales__mut, scales__1 = scales__1_mut, shape = shape_mut, jse = jse_mut, event = event_mut;
        const area = [area_, area__1, area__2, area__3];
        const scales = [scales_, scales__1];
        const area_1 = area;
        const y2 = area_1[3];
        const y1 = area_1[1];
        const x2 = area_1[2];
        const x1 = area_1[0];
        const scales_1 = scales;
        const sy = scales_1[1];
        const sx = scales_1[0];
        switch (shape.tag) {
            case 0: {
                const shape_1 = shape.fields[1];
                area__mut = area_1[0];
                area__1_mut = area_1[1];
                area__2_mut = area_1[2];
                area__3_mut = area_1[3];
                scales__mut = scales_1[0];
                scales__1_mut = scales_1[1];
                shape_mut = shape_1;
                jse_mut = jse;
                event_mut = event;
                continue Events_triggerEvent;
                break;
            }
            case 8: {
                const shape_2 = shape.fields[1];
                const dy = shape.fields[0][1];
                const dx = shape.fields[0][0];
                area__mut = (x1 + dx);
                area__1_mut = (y1 + dy);
                area__2_mut = (x2 + dx);
                area__3_mut = (y2 + dy);
                scales__mut = scales_1[0];
                scales__1_mut = scales_1[1];
                shape_mut = shape_2;
                jse_mut = jse;
                event_mut = event;
                continue Events_triggerEvent;
                break;
            }
            case 9: {
                const shape_3 = shape.fields[3];
                const p2 = shape.fields[1];
                const p1 = shape.fields[0];
                const isx = shape.fields[2];
                const x1$0027 = Projections_projectOneX(x1, x2)(sx)(p1);
                const x2$0027 = Projections_projectOneX(x1, x2)(sx)(p2);
                area__mut = x1$0027;
                area__1_mut = y1;
                area__2_mut = x2$0027;
                area__3_mut = y2;
                scales__mut = isx;
                scales__1_mut = sy;
                shape_mut = shape_3;
                jse_mut = jse;
                event_mut = event;
                continue Events_triggerEvent;
                break;
            }
            case 10: {
                const shape_4 = shape.fields[3];
                const p2_1 = shape.fields[1];
                const p1_1 = shape.fields[0];
                const isy = shape.fields[2];
                const y1$0027 = Projections_projectOneY(y1, y2)(sy)(p1_1);
                const y2$0027 = Projections_projectOneY(y1, y2)(sy)(p2_1);
                area__mut = x1;
                area__1_mut = y1$0027;
                area__2_mut = x2;
                area__3_mut = y2$0027;
                scales__mut = sx;
                scales__1_mut = isy;
                shape_mut = shape_4;
                jse_mut = jse;
                event_mut = event;
                continue Events_triggerEvent;
                break;
            }
            case 7: {
                const t = shape.fields[0][0];
                const shape_5 = shape.fields[3];
                const r = shape.fields[0][1];
                const l = shape.fields[0][3];
                const isy_1 = shape.fields[2];
                const isx_1 = shape.fields[1];
                const b = shape.fields[0][2];
                const calculateNestedRange = (rev) => ((tupledArg) => ((ins) => {
                    const v1 = tupledArg[0];
                    const v2 = tupledArg[1];
                    return (outs) => {
                        if (ins.tag === 1) {
                            const vals = ins.fields[0];
                            const pos_1 = collect((v) => ofArray([Projections_projectOne(rev, v1, v2, outs, new Value(0, [v, 0])), Projections_projectOne(rev, v1, v2, outs, new Value(0, [v, 1]))]), vals);
                            return [min_3(pos_1, {
                                Compare: comparePrimitives,
                            }), max_3(pos_1, {
                                Compare: comparePrimitives,
                            })];
                        }
                        else {
                            const l_1 = ins.fields[0].fields[0];
                            const h = ins.fields[1].fields[0];
                            const pos = ofArray([Projections_projectOne(rev, v1, v2, outs, new Value(1, [new continuous(l_1)])), Projections_projectOne(rev, v1, v2, outs, new Value(1, [new continuous(h)]))]);
                            return [min_3(pos, {
                                Compare: comparePrimitives,
                            }), max_3(pos, {
                                Compare: comparePrimitives,
                            })];
                        }
                    };
                }));
                const patternInput = calculateNestedRange(false)([x1, x2])(isx_1)(sx);
                const x2$0027_1 = patternInput[1];
                const x1$0027_1 = patternInput[0];
                const patternInput_1 = calculateNestedRange(true)([y1, y2])(isy_1)(sy);
                const y2$0027_1 = patternInput_1[1];
                const y1$0027_1 = patternInput_1[0];
                area__mut = (x1$0027_1 + l);
                area__1_mut = (y1$0027_1 + t);
                area__2_mut = (x2$0027_1 - r);
                area__3_mut = (y2$0027_1 - b);
                scales__mut = isx_1;
                scales__1_mut = isy_1;
                shape_mut = shape_5;
                jse_mut = jse;
                event_mut = event;
                continue Events_triggerEvent;
                break;
            }
            case 5: {
                const shapes = shape.fields[0];
                for (let idx = 0; idx <= (shapes.length - 1); idx++) {
                    const shape_6 = shapes[idx];
                    Events_triggerEvent(area_1[0], area_1[1], area_1[2], area_1[3], scales_1[0], scales_1[1], shape_6, jse, event);
                }
                break;
            }
            case 6: {
                const sy_1 = shape.fields[2];
                const sx_1 = shape.fields[1];
                const shape_7 = shape.fields[3];
                const handlers = shape.fields[0];
                const localEvent = Events_projectEvent(area_1[0], area_1[1], area_1[2], area_1[3], scales_1[0], scales_1[1], event);
                if (Events_inScales(scales_1[0], scales_1[1], localEvent)) {
                    const enumerator = getEnumerator(handlers);
                    try {
                        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
                            const handler = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
                            let matchResult, f, pt, f_1, pt_1, f_2, f_3;
                            switch (localEvent.tag) {
                                case 1: {
                                    if (localEvent.fields[0].tag === 1) {
                                        if (handler.tag === 4) {
                                            matchResult = 1;
                                            f_1 = curry2(handler.fields[0]);
                                            pt_1 = localEvent.fields[1];
                                        }
                                        else {
                                            matchResult = 4;
                                        }
                                    }
                                    else if (handler.tag === 5) {
                                        matchResult = 1;
                                        f_1 = curry2(handler.fields[0]);
                                        pt_1 = localEvent.fields[1];
                                    }
                                    else {
                                        matchResult = 4;
                                    }
                                    break;
                                }
                                case 2: {
                                    if (handler.tag === 6) {
                                        matchResult = 2;
                                        f_2 = handler.fields[0];
                                    }
                                    else {
                                        matchResult = 4;
                                    }
                                    break;
                                }
                                case 3: {
                                    if (handler.tag === 7) {
                                        matchResult = 3;
                                        f_3 = handler.fields[0];
                                    }
                                    else {
                                        matchResult = 4;
                                    }
                                    break;
                                }
                                default:
                                    switch (localEvent.fields[0].tag) {
                                        case 1: {
                                            if (handler.tag === 0) {
                                                matchResult = 0;
                                                f = curry2(handler.fields[0]);
                                                pt = localEvent.fields[1];
                                            }
                                            else {
                                                matchResult = 4;
                                            }
                                            break;
                                        }
                                        case 2: {
                                            if (handler.tag === 1) {
                                                matchResult = 0;
                                                f = curry2(handler.fields[0]);
                                                pt = localEvent.fields[1];
                                            }
                                            else {
                                                matchResult = 4;
                                            }
                                            break;
                                        }
                                        case 3: {
                                            if (handler.tag === 2) {
                                                matchResult = 0;
                                                f = curry2(handler.fields[0]);
                                                pt = localEvent.fields[1];
                                            }
                                            else {
                                                matchResult = 4;
                                            }
                                            break;
                                        }
                                        default:
                                            if (handler.tag === 3) {
                                                matchResult = 0;
                                                f = curry2(handler.fields[0]);
                                                pt = localEvent.fields[1];
                                            }
                                            else {
                                                matchResult = 4;
                                            }
                                    }
                            }
                            switch (matchResult) {
                                case 0: {
                                    if (!equals(jse, defaultOf())) {
                                        jse.preventDefault();
                                    }
                                    f(jse)(pt);
                                    break;
                                }
                                case 1: {
                                    if (!equals(jse, defaultOf())) {
                                        jse.preventDefault();
                                    }
                                    f_1(jse)(pt_1);
                                    break;
                                }
                                case 2: {
                                    f_2(jse);
                                    break;
                                }
                                case 3: {
                                    f_3(jse);
                                    break;
                                }
                            }
                        }
                    }
                    finally {
                        disposeSafe(enumerator);
                    }
                }
                area__mut = area_1[0];
                area__1_mut = area_1[1];
                area__2_mut = area_1[2];
                area__3_mut = area_1[3];
                scales__mut = scales_1[0];
                scales__1_mut = scales_1[1];
                shape_mut = shape_7;
                jse_mut = jse;
                event_mut = event;
                continue Events_triggerEvent;
                break;
            }
            default:
                0;
        }
        break;
    }
}

export function Derived_StrokeColor(clr, s) {
    return new Shape(0, [(s_1) => (new Style([1, new Color(1, [clr])], s_1.StrokeWidth, s_1.StrokeDashArray, s_1.Fill, s_1.Animation, s_1.Font, s_1.Cursor, s_1.FormatAxisXLabel, s_1.FormatAxisYLabel)), s]);
}

export function Derived_FillColor(clr, s) {
    return new Shape(0, [(s_1) => (new Style(s_1.StrokeColor, s_1.StrokeWidth, s_1.StrokeDashArray, new FillStyle(0, [[1, new Color(1, [clr])]]), s_1.Animation, s_1.Font, s_1.Cursor, s_1.FormatAxisXLabel, s_1.FormatAxisYLabel)), s]);
}

export function Derived_Font(font, clr, s) {
    return new Shape(0, [(s_1) => (new Style([0, new Color(1, [clr])], s_1.StrokeWidth, s_1.StrokeDashArray, new FillStyle(0, [[1, new Color(1, [clr])]]), s_1.Animation, font, s_1.Cursor, s_1.FormatAxisXLabel, s_1.FormatAxisYLabel)), s]);
}

export function Derived_Area(line) {
    return new Shape(8, [delay(() => {
        const line_1 = Array.from(line);
        const matchValue = line_1[0][0];
        const lastX = line_1[line_1.length - 1][0];
        const firstX = matchValue;
        return append(singleton([firstX, new Value(1, [new continuous(0)])]), delay(() => append(line_1, delay(() => append(singleton([lastX, new Value(1, [new continuous(0)])]), delay(() => singleton([firstX, new Value(1, [new continuous(0)])])))))));
    })]);
}

export function Derived_VArea(line) {
    return new Shape(8, [delay(() => {
        const line_1 = Array.from(line);
        const matchValue = line_1[0][1];
        const lastY = line_1[line_1.length - 1][1];
        const firstY = matchValue;
        return append(singleton([new Value(1, [new continuous(0)]), firstY]), delay(() => append(line_1, delay(() => append(singleton([new Value(1, [new continuous(0)]), lastY]), delay(() => singleton([new Value(1, [new continuous(0)]), firstY])))))));
    })]);
}

export function Derived_VShiftedArea(offs, line) {
    return new Shape(8, [delay(() => {
        const line_1 = Array.from(line);
        const matchValue = line_1[0][1];
        const lastY = line_1[line_1.length - 1][1];
        const firstY = matchValue;
        return append(singleton([new Value(1, [new continuous(offs)]), firstY]), delay(() => append(line_1, delay(() => append(singleton([new Value(1, [new continuous(offs)]), lastY]), delay(() => singleton([new Value(1, [new continuous(offs)]), firstY])))))));
    })]);
}

export function Derived_Bar(x, y) {
    return new Shape(8, [delay(() => append(singleton([new Value(1, [x]), new Value(0, [y, 0])]), delay(() => append(singleton([new Value(1, [x]), new Value(0, [y, 1])]), delay(() => append(singleton([new Value(1, [new continuous(0)]), new Value(0, [y, 1])]), delay(() => singleton([new Value(1, [new continuous(0)]), new Value(0, [y, 0])]))))))))]);
}

export function Derived_Column(x, y) {
    return new Shape(8, [delay(() => append(singleton([new Value(0, [x, 0]), new Value(1, [y])]), delay(() => append(singleton([new Value(0, [x, 1]), new Value(1, [y])]), delay(() => append(singleton([new Value(0, [x, 1]), new Value(1, [new continuous(0)])]), delay(() => singleton([new Value(0, [x, 0]), new Value(1, [new continuous(0)])]))))))))]);
}

export function Compost_niceNumber(num, decs) {
    const str = toString(num);
    const dot = str.indexOf(".") | 0;
    const patternInput = (dot === -1) ? [str, ""] : [substring(str, 0, dot), substring(str, dot + 1, min(decs, (str.length - dot) - 1))];
    const before = patternInput[0];
    const after = patternInput[1];
    const after_1 = (after.length < decs) ? (after + (toArray(delay(() => map_1((i) => "0", rangeDouble(1, 1, decs - after.length)))).join(''))) : after;
    let res = before;
    if (before.length > 5) {
        const enumerator = getEnumerator(rangeDouble(before.length - 1, -1, 0));
        try {
            while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
                const i_1 = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]() | 0;
                const j = (before.length - i_1) | 0;
                if ((i_1 !== 0) && ((j % 3) === 0)) {
                    res = insert(res, i_1, ",");
                }
            }
        }
        finally {
            disposeSafe(enumerator);
        }
    }
    if (forAll((y) => ("0" === y), after_1.split(""))) {
        return res;
    }
    else {
        return (res + ".") + after_1;
    }
}

export function Compost_defaultFormat(scale, value) {
    if (value.tag === 1) {
        const v = value.fields[0].fields[0];
        let dec;
        if (scale.tag === 0) {
            const l = scale.fields[0].fields[0];
            const h = scale.fields[1].fields[0];
            dec = Scales_decimalPoints(l, h);
        }
        else {
            dec = 0;
        }
        return Compost_niceNumber(round(v, ~~dec), ~~dec);
    }
    else {
        const s = value.fields[0].fields[0];
        return s;
    }
}

export const Compost_defstyle = new Style([1, new Color(0, [256, 0, 0])], new Width(2), [], new FillStyle(0, [[1, new Color(0, [196, 196, 196])]]), void 0, "10pt sans-serif", "default", Compost_defaultFormat, Compost_defaultFormat);

export function Compost_getRelativeLocation(el, x, y) {
    const getOffset = (parent_mut, tupledArg_mut) => {
        getOffset:
        while (true) {
            const parent = parent_mut, tupledArg = tupledArg_mut;
            const x_1 = tupledArg[0];
            const y_1 = tupledArg[1];
            if (equals(parent, defaultOf())) {
                return [x_1, y_1];
            }
            else {
                parent_mut = parent.offsetParent;
                tupledArg_mut = [x_1 - parent.offsetLeft, y_1 - parent.offsetTop];
                continue getOffset;
            }
            break;
        }
    };
    const getParent = (parent_1_mut) => {
        getParent:
        while (true) {
            const parent_1 = parent_1_mut;
            if ((parent_1.namespaceURI === "http://www.w3.org/2000/svg") && (parent_1.tagName !== "svg")) {
                if (!equals(parent_1.parentElement, defaultOf())) {
                    parent_1_mut = parent_1.parentElement;
                    continue getParent;
                }
                else {
                    parent_1_mut = parent_1.parentNode;
                    continue getParent;
                }
            }
            else if (!equals(parent_1.offsetParent, defaultOf())) {
                return parent_1;
            }
            else if (!equals(parent_1.parentElement, defaultOf())) {
                parent_1_mut = parent_1.parentElement;
                continue getParent;
            }
            else {
                parent_1_mut = parent_1.parentNode;
                continue getParent;
            }
            break;
        }
    };
    return getOffset(getParent(el), [x, y]);
}

export function Compost_createSvg(revX, revY, width, height, viz) {
    const patternInput = Scales_calculateScales(Compost_defstyle, viz);
    const sy = patternInput[0][1];
    const sx = patternInput[0][0];
    const shape = patternInput[1];
    const defs = [];
    const area = [0, 0, width, height];
    const svg = Drawing_drawShape(new Drawing_DrawingContext(Compost_defstyle, defs), area[0], area[1], area[2], area[3], sx, sy, shape);
    const triggerEvent = (e, event) => {
        Events_triggerEvent(area[0], area[1], area[2], area[3], sx, sy, shape, e, event);
    };
    const mouseHandler = (kind, el, evt) => {
        const evt_1 = evt;
        const patternInput_1 = Compost_getRelativeLocation(el, evt_1.pageX, evt_1.pageY);
        const y = patternInput_1[1];
        const x = patternInput_1[0];
        triggerEvent(evt_1, new Events_InteractiveEvent(0, [kind, [new Value(1, [new continuous(x)]), new Value(1, [new continuous(y)])]]));
    };
    const touchHandler = (kind_1, el_1, evt_2) => {
        const evt_3 = evt_2;
        const touch = evt_3.touches[0];
        const patternInput_2 = Compost_getRelativeLocation(el_1, touch.pageX, touch.pageY);
        const y_1 = patternInput_2[1];
        const x_1 = patternInput_2[0];
        triggerEvent(evt_3, new Events_InteractiveEvent(1, [kind_1, [new Value(1, [new continuous(x_1)]), new Value(1, [new continuous(y_1)])]]));
    };
    return El_op_Dynamic_Z451691CD(h_2, "div")(empty())(singleton_1(El_op_Dynamic_Z451691CD(s_6, "svg")(ofArray([op_EqualsGreater("style", "overflow:visible"), op_EqualsGreater("width", int32ToString(~~width)), op_EqualsGreater("height", int32ToString(~~height)), op_EqualsBangGreater("click", uncurry2(curry3(mouseHandler)(new Events_MouseEventKind(0, [])))), op_EqualsBangGreater("mousemove", uncurry2(curry3(mouseHandler)(new Events_MouseEventKind(1, [])))), op_EqualsBangGreater("mousedown", uncurry2(curry3(mouseHandler)(new Events_MouseEventKind(3, [])))), op_EqualsBangGreater("mouseup", uncurry2(curry3(mouseHandler)(new Events_MouseEventKind(2, [])))), op_EqualsBangGreater("mouseleave", (_arg, evt_4) => {
        triggerEvent(evt_4, new Events_InteractiveEvent(3, []));
    }), op_EqualsBangGreater("touchmove", uncurry2(curry3(touchHandler)(new Events_TouchEventKind(0, [])))), op_EqualsBangGreater("touchstart", uncurry2(curry3(touchHandler)(new Events_TouchEventKind(1, [])))), op_EqualsBangGreater("touchend", (_arg_1, evt_5) => {
        triggerEvent(evt_5, new Events_InteractiveEvent(2, []));
    })]))(toList(delay(() => {
        const renderCtx = new Svg_RenderingContext(defs);
        const body = Array.from(Svg_renderSvg(renderCtx, svg));
        return append(defs, delay(() => body));
    })))));
}

