using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Utilities.Html
{
    public static class HtmlUtil
    {
        /// <summary>
        /// HTML数据清洗器
        /// </summary>
        static HtmlSanitizer htmlSanitizer = new HtmlSanitizer();
        static HtmlUtil()
        {
            //htmlSanitizer.AllowedAttributes.Add("class");
        }

        /// <summary>
        /// 清洗HTML 防止XSS
        /// </summary>
        /// <param name="html">HTML内容</param>
        /// <returns></returns>
        public static string SanitizeHtml(string html)
        {
            return htmlSanitizer.Sanitize(html);
        }

        #region White List
        /// <summary>
        /// 添加标签到白名单
        /// 默认白名单标签：
        /// a, abbr, acronym, address, area, article, aside, b, bdi, big, blockquote, br, button, caption, center, 
        /// cite, code, col, colgroup, data, datalist, dd, del, details, dfn, dir, div, dl, dt, em, fieldset, 
        /// figcaption, figure, font, footer, form, h1, h2, h3, h4, h5, h6, header, hr, i, img, input, ins, kbd,
        /// keygen, label, legend, li, main, map, mark, menu, menuitem, meter, nav, ol, optgroup, option, output,
        /// p, pre, progress, q, rp, rt, ruby, s, samp, section, select, small, span, strike, strong, sub, summary,
        /// sup, table, tbody, td, textarea, tfoot, th, thead, time, tr, tt, u, ul, var, wbr
        /// </summary>
        /// <param name="tag">标签</param>
        public static void AddTagsToWhiteList(string tag)
        {
            htmlSanitizer.AllowedTags.Add(tag);
        }

        /// <summary>
        /// 移除白名单标签
        /// 默认白名单标签：
        /// a, abbr, acronym, address, area, article, aside, b, bdi, big, blockquote, br, button, caption, center, 
        /// cite, code, col, colgroup, data, datalist, dd, del, details, dfn, dir, div, dl, dt, em, fieldset, 
        /// figcaption, figure, font, footer, form, h1, h2, h3, h4, h5, h6, header, hr, i, img, input, ins, kbd,
        /// keygen, label, legend, li, main, map, mark, menu, menuitem, meter, nav, ol, optgroup, option, output,
        /// p, pre, progress, q, rp, rt, ruby, s, samp, section, select, small, span, strike, strong, sub, summary,
        /// sup, table, tbody, td, textarea, tfoot, th, thead, time, tr, tt, u, ul, var, wbr
        /// </summary>
        /// <param name="tag">标签</param>
        public static void RemoveTagsToWhiteList(string tag)
        {
            htmlSanitizer.AllowedTags.Remove(tag);
        }

        /// <summary>
        /// 添加白名单属性
        /// 默认白名单属性：
        /// abbr, accept, accept-charset, accesskey, action, align, alt, autocomplete, autosave, axis, bgcolor, 
        /// border, cellpadding, cellspacing, challenge, char, charoff, charset, checked, cite, clear, color, 
        /// cols, colspan, compact, contenteditable, coords, datetime, dir, disabled, draggable, dropzone, 
        /// enctype, for, frame, headers, height, high, href, hreflang, hspace, ismap, keytype, label, lang, 
        /// list, longdesc, low, max, maxlength, media, method, min, multiple, name, nohref, noshade, novalidate,
        /// nowrap, open, optimum, pattern, placeholder, prompt, pubdate, radiogroup, readonly, rel, required, 
        /// rev, reversed, rows, rowspan, rules, scope, selected, shape, size, span, spellcheck, src, start, 
        /// step, style, summary, tabindex, target, title, type, usemap, valign, value, vspace, width, wrap
        /// </summary>
        /// <param name="attribute"></param>
        public static void AddAttributesToWhiteList(string attribute)
        {
            htmlSanitizer.AllowedAttributes.Add(attribute);
        }

        /// <summary>
        /// 移除白名单属性
        /// 默认白名单属性：
        /// abbr, accept, accept-charset, accesskey, action, align, alt, autocomplete, autosave, axis, bgcolor, 
        /// border, cellpadding, cellspacing, challenge, char, charoff, charset, checked, cite, clear, color, 
        /// cols, colspan, compact, contenteditable, coords, datetime, dir, disabled, draggable, dropzone, 
        /// enctype, for, frame, headers, height, high, href, hreflang, hspace, ismap, keytype, label, lang, 
        /// list, longdesc, low, max, maxlength, media, method, min, multiple, name, nohref, noshade, novalidate,
        /// nowrap, open, optimum, pattern, placeholder, prompt, pubdate, radiogroup, readonly, rel, required, 
        /// rev, reversed, rows, rowspan, rules, scope, selected, shape, size, span, spellcheck, src, start, 
        /// step, style, summary, tabindex, target, title, type, usemap, valign, value, vspace, width, wrap
        /// </summary>
        /// <param name="attribute"></param>
        public static void RemoveAttributesToWhiteList(string attribute)
        {
            htmlSanitizer.AllowedAttributes.Remove(attribute);
        }

        /// <summary>
        /// 添加白名单CSS属性
        /// 默认白名单CSS属性：
        /// background, background-attachment, background-clip, background-color, background-image, background-origin,
        /// background-position, background-repeat, background-size, border, border-bottom, border-bottom-color,
        /// border-bottom-left-radius, border-bottom-right-radius, border-bottom-style, border-bottom-width, 
        /// border-collapse, border-color, border-image, border-image-outset, border-image-repeat, border-image-slice,
        /// border-image-source, border-image-width, border-left, border-left-color, border-left-style, 
        /// border-left-width, border-radius, border-right, border-right-color, border-right-style, border-right-width,
        /// border-spacing, border-style, border-top, border-top-color, border-top-left-radius, border-top-right-radius, border-top-style, border-top-width, border-width, bottom, caption-side, clear, clip, color, content,
        /// counter-increment, counter-reset, cursor, direction, display, empty-cells, float, font, font-family, 
        /// font-feature-settings, font-kerning, font-language-override, font-size, font-size-adjust, font-stretch,
        /// font-style, font-synthesis, font-variant, font-variant-alternates, font-variant-caps, 
        /// font-variant-east-asian, font-variant-ligatures, font-variant-numeric, font-variant-position, 
        /// font-weight, height, left, letter-spacing, line-height, list-style, list-style-image, list-style-position,
        /// list-style-type, margin, margin-bottom, margin-left, margin-right, margin-top, max-height, max-width, 
        /// min-height, min-width, opacity, orphans, outline, outline-color, outline-offset, outline-style, 
        /// outline-width, overflow, overflow-wrap, overflow-x, overflow-y, padding, padding-bottom, padding-left,
        /// padding-right, padding-top, page-break-after, page-break-before, page-break-inside, quotes, right, 
        /// table-layout, text-align, text-decoration, text-decoration-color, text-decoration-line, 
        /// text-decoration-skip, text-decoration-style, text-indent, text-transform, top, unicode-bidi,
        /// vertical-align, visibility, white-space, widows, width, word-spacing, z-index
        /// </summary>
        /// <param name="cssProperty"></param>
        public static void AddCssProperty(string cssProperty)
        {
            htmlSanitizer.AllowedCssProperties.Add(cssProperty);
        }
        /// <summary>
        /// 移除白名单CSS属性
        /// 默认白名单CSS属性：
        /// background, background-attachment, background-clip, background-color, background-image, background-origin,
        /// background-position, background-repeat, background-size, border, border-bottom, border-bottom-color,
        /// border-bottom-left-radius, border-bottom-right-radius, border-bottom-style, border-bottom-width, 
        /// border-collapse, border-color, border-image, border-image-outset, border-image-repeat, border-image-slice,
        /// border-image-source, border-image-width, border-left, border-left-color, border-left-style, 
        /// border-left-width, border-radius, border-right, border-right-color, border-right-style, border-right-width,
        /// border-spacing, border-style, border-top, border-top-color, border-top-left-radius, border-top-right-radius, border-top-style, border-top-width, border-width, bottom, caption-side, clear, clip, color, content,
        /// counter-increment, counter-reset, cursor, direction, display, empty-cells, float, font, font-family, 
        /// font-feature-settings, font-kerning, font-language-override, font-size, font-size-adjust, font-stretch,
        /// font-style, font-synthesis, font-variant, font-variant-alternates, font-variant-caps, 
        /// font-variant-east-asian, font-variant-ligatures, font-variant-numeric, font-variant-position, 
        /// font-weight, height, left, letter-spacing, line-height, list-style, list-style-image, list-style-position,
        /// list-style-type, margin, margin-bottom, margin-left, margin-right, margin-top, max-height, max-width, 
        /// min-height, min-width, opacity, orphans, outline, outline-color, outline-offset, outline-style, 
        /// outline-width, overflow, overflow-wrap, overflow-x, overflow-y, padding, padding-bottom, padding-left,
        /// padding-right, padding-top, page-break-after, page-break-before, page-break-inside, quotes, right, 
        /// table-layout, text-align, text-decoration, text-decoration-color, text-decoration-line, 
        /// text-decoration-skip, text-decoration-style, text-indent, text-transform, top, unicode-bidi,
        /// vertical-align, visibility, white-space, widows, width, word-spacing, z-index
        /// </summary>
        /// <param name="cssProperty"></param>
        public static void RemoveCssProperty(string cssProperty)
        {
            htmlSanitizer.AllowedCssProperties.Remove(cssProperty);
        }
        #endregion
    }
}
