﻿var bind = function (e, t) {
    return function () {
        return e.apply(t, arguments)
    }
};
! function (e, t) {
    var n;
    return n = function () {
        function t(t, n, r) {
            this.onEvent = bind(this.onEvent, this);
            var a, i, l;
            a = e(t), i = this, l = {
                root: '',
                script: "/files/filetree",
                folderEvent: "click",
                expandSpeed: 500,
                collapseSpeed: 500,
                expandEasing: "swing",
                collapseEasing: "swing",
                multiFolder: !0,
                loadMessage: "Loading...",
                errorMessage: "Unable to get file tree information",
                multiSelect: !1,
                onlyFolders: !1,
                onlyFiles: !1,
                preventLinkAction: !1
            }, this.jqft = {
                container: a
            }, this.options = e.extend(l, n), this.callback = r, this.data = {}, a.html('<ul class="jqueryFileTree start"><li class="wait">' + this.options.loadMessage + "<li></ul>"), i.showTree(a, escape(this.options.root), '', function () {
                return i._trigger("filetreeinitiated", {})
            }), a.delegate("li a", this.options.folderEvent, i.onEvent)
        }
        return t.prototype.onEvent = function (t) {
            debugger;
            var n, r, a, i, l, s;
            return n = e(t.target), l = this.options, i = this.jqft, r = this, a = this.callback, r.data = {}, r.data.li = n.closest("li"), r.data.type = null != (s = r.data.li.hasClass("directory")) ? s : {
                directory: "file"
            }, r.data.value = n.text(), r.data.rel = n.prop("rel"), r.data.container = i.container, l.preventLinkAction && t.preventDefault(), n.parent().hasClass("directory") ? n.parent().hasClass("collapsed") ? (l.multiFolder || (n.parent().parent().find("UL").slideUp({
                duration: l.collapseSpeed,
                easing: l.collapseEasing
            }), n.parent().parent().find("LI.directory").removeClass("expanded").addClass("collapsed")), n.parent().removeClass("collapsed").addClass("expanded"), n.parent().find("UL").remove(), r.showTree(n.parent(), n.attr("rel"), n.attr("relType"), function () {
                return r._trigger("filetreeexpanded", r.data), null != a
            })) : n.parent().find("UL").slideUp({
                duration: l.collapseSpeed,
                easing: l.collapseEasing,
                start: function () {
                    return r._trigger("filetreecollapse", r.data)
                },
                complete: function () {
                    return n.parent().removeClass("expanded").addClass("collapsed"), r._trigger("filetreecollapsed", r.data), null != a
                }
            }) : (l.multiSelect ? n.parent().find("input").is(":checked") ? (n.parent().find("input").prop("checked", !1), n.parent().removeClass("selected")) : (n.parent().find("input").prop("checked", !0), n.parent().addClass("selected")) : (i.container.find("li").removeClass("selected"), n.parent().addClass("selected")), r._trigger("filetreeclicked", r.data), "function" == typeof a ? a(n.attr("rel")) : void 0)
        }, t.prototype.showTree = function (t, n, relType, r) {
            var a, i, l, s, o, d, p;
            return a = e(t), d = this.options, i = this, a.addClass("wait"), e(".jqueryFileTree.start").remove(), l = {
                dir: n,
                relType: relType,
                onlyFolders: d.onlyFolders,
                onlyFiles: d.onlyFiles,
                multiSelect: d.multiSelect
            }, o = function (t) {
                var l;
                return a.find(".start").html(""), a.removeClass("wait").append(t), d.root === n ? a.find("UL:hidden").show("undefined" != typeof callback && null !== callback) : (void 0 === jQuery.easing[d.expandEasing] && (console.log("Easing library not loaded. Include jQueryUI or 3rd party lib."), d.expandEasing = "swing"), a.find("UL:hidden").slideDown({
                    duration: d.expandSpeed,
                    easing: d.expandEasing,
                    start: function () {
                        return i._trigger("filetreeexpand", i.data)
                    },
                    complete: r
                })), l = e('[rel="' + decodeURIComponent(n) + '"]').parent(), d.multiSelect && l.children("input").is(":checked") && l.find("ul li input").each(function () {
                    return e(this).prop("checked", !0), e(this).parent().addClass("selected")
                }), !1
            }, s = function () {
                return a.find(".start").html(""), a.removeClass("wait").append("<p>" + d.errorMessage + "</p>"), !1
            }, "function" == typeof d.script ? (p = d.script(l), "string" == typeof p || p instanceof jQuery ? o(p) : s()) : e.ajax({
                url: d.script,
                type: "POST",
                dataType: "HTML",
                data: l
            }).done(function (e) {
                return o(e)
            }).fail(function () {
                return s()
            });
        }, t.prototype._trigger = function (e, t) {
            var n;
            return n = this.jqft.container, n.triggerHandler(e, t)
        }, t
    }(), e.fn.extend({
        fileTree: function (t, r) {
            return this.each(function () {
                var a, i;
                return a = e(this), i = a.data("fileTree"), i || a.data("fileTree", i = new n(this, t, r)), "string" == typeof t ? i[option].apply(i) : void 0
            })
        }
    })
}(window.jQuery, window);