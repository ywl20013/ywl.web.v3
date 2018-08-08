window.getCookie = function (name) {
    if (document.cookie.length > 0) {　　 //先查询cookie是否为空，为空就return ""
        var c_start = document.cookie.indexOf(name + "=");　　 //通过String对象的indexOf()来检查这个cookie是否存在，不存在就为 -1
        if (c_start != -1) {
            c_start = c_start + name.length + 1;　　 //最后这个+1其实就是表示"="号啦，这样就获取到了cookie值的开始位置
            var c_end = document.cookie.indexOf(";", c_start);　 //其实我刚看见indexOf()第二个参数的时候猛然有点晕，后来想起来表示指定的开始索引的位置...这句是为了得到值的结束位置。因为需要考虑是否是最后一项，所以通过";"号是否存在来判断
            if (c_end == -1) c_end = document.cookie.length;
            var value = document.cookie.substring(c_start, c_end);
            value = unescape(value);　 //通过substring()得到了值。想了解unescape()得先知道escape()是做什么的，都是很重要的基础，想了解的可以搜索下，在文章结尾处也会进行讲解cookie编码细节
            return value;
        }
    }
    return "";
};

window.setCookie = function (name, value, days) {
    if (days != null) {
        var exp = new Date();
        exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
    }
    //var path = '; path=/';
    //var expires = days == null ? "" : '; expires=' + exp.toGMTString();
    document.cookie = name + "=" + escape(value) + ";path=/" + (days == null ? "" : ";expires=" + exp.toGMTString());
    //document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    //document.cookie = [name, '=', escape(value), expires, path].join('');
};

window.delCookie = function (name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);

    var cval = getCookie(name);
    if (cval != null)
        //document.cookie = name + "=" + ";expires=" + exp.toGMTString();
        document.cookie = name + "=" + escape(cval) + ";path=/" + ";expires=" + exp.toGMTString();
};

window.getRootPath = function () {
    var pathName = window.location.pathname.substring(1);
    var webName = pathName == '' ? '' : pathName.substring(0, pathName.indexOf('/'));
    if (webName == "") {
        return window.location.protocol + '//' + window.location.host;
    }
    else {
        return window.location.protocol + '//' + window.location.host + '/' + webName;
    }
};

//Object.prototype.clone = function () {
//    var copy = (this instanceof Array) ? [] : {};
//    for (attr in this) {
//        if (!this.hasOwnProperty(attr)) continue;
//        copy[attr] = (typeof this[attr] == "object") ? this[attr].clone() : this[attr];
//    }
//    return copy;
//};

function clone(obj) {
    // Handle the 3 simple types, and null or undefined
    if (null == obj || "object" != typeof obj) return obj;

    // Handle Date
    if (obj instanceof Date) {
        var copy = new Date();
        copy.setTime(obj.getTime());
        return copy;
    }

    // Handle Array
    if (obj instanceof Array) {
        var copy = [];
        for (var i = 0, len = obj.length; i < len; ++i) {
            copy[i] = clone(obj[i]);
        }
        return copy;
    }

    // Handle Object
    if (obj instanceof Object) {
        var copy = {};
        for (var attr in obj) {
            if (obj.hasOwnProperty(attr)) copy[attr] = clone(obj[attr]);
        }
        return copy;
    }

    throw new Error("Unable to copy obj! Its type isn't supported.");
}


var request = {
    queryString: function (val) {
        var _tmp = location.href.split('?');
        var uri = window.location.search;
        if (_tmp.length > 1) uri = _tmp[1];
        //var uri = window.location.search; 
        var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
    },
    queryStrings: function () {
        var _tmp = location.href.split('?');
        var uri = window.location.search;
        if (_tmp.length > 1) uri = _tmp[1];
        var re = /\w*\=([^\&\?]*)/ig;
        var retval = [];
        while ((arr = re.exec(uri)) != null)
            retval.push(arr[0]);
        return retval;
    },
    setQuery: function (val1, val2) {
        var a = this.queryStrings();
        var retval = "";
        var seted = false;
        var re = new RegExp("^" + val1 + "\=([^\&\?]*)$", "ig");
        for (var i = 0; i < a.length; i++) {
            if (re.test(a[i])) {
                seted = true;
                a[i] = val1 + "=" + val2;
            }
        }
        retval = a.join("&");
        return "?" + retval + (seted ? "" : (retval ? "&" : "") + val1 + "=" + val2);
    }
}

/** 
 * 时间对象的格式化; 
 */
Date.prototype.format = function (format) {
	/* 
	 * var date= new Date(Date.parse(strTime.replace(/-/g,"/"))); //转换成Date();
	 * eg:format="YYYY-MM-dd hh:mm:ss"; 
	 */
    var o = {
        "M+": this.getMonth() + 1, // month  
        "d+": this.getDate(), // day  
        "h+": this.getHours(), // hour  
        "m+": this.getMinutes(), // minute  
        "s+": this.getSeconds(), // second  
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter  
        "S": this.getMilliseconds()
        // millisecond  
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

/** 
 * 为string扩展 toDateTimeString 函数
 * @param  {[type]} format [description]
 * @return {[type]}        [description]
 */
String.prototype.toDateTimeString = function (format) {
    if (/\/Date\(\d*\)/.test(this)) {
        var date = eval('new ' + this.replace(/\//g, ''));
        return date.format(format == undefined ? "yyyy-MM-dd hh:mm:ss" : format);
    } else
        return new Date(this.replace(/-/g, '/')).format(format == undefined ? "yyyy-MM-dd hh:mm:ss" : format);
}


var strs = location.pathname.split('/');
if (strs.length >= 1) location.appForld = strs[1];
if (strs.length >= 2) location.mvcController = strs[2];
if (strs.length >= 3) location.mvcControllerAction = strs[3];


var ywl = {
    ajax: function (options) {
        var settings = $.extend({ processUrl: true }, options);
        if (settings.controller) {
            settings.url = location.origin + "/" + location.appForld + "/" + settings.controller + "/";
            if (settings.action) settings.url += settings.action + "/";
        } else if (settings.processUrl && settings.url)
            settings.url = location.origin + "/" + location.appForld + "/" + settings.url;
        $.ajax(settings);
    },
    bootstrap: {
        buttonCheckBox: function (options) {
            var settings = $.extend({}, options);
            return this.each(function () {
                var el = $(this);
                var input = $('input', el);
                if (!el.hasClass('form-group')) el.addClass('form-group');
                if (!el.hasClass('btn-checkbox')) el.addClass('btn-checkbox');
                var btnGroup = $('.btn-group', el);
                if (btnGroup.length == 0) {
                    btnGroup = $('<div class="btn-group" role="group" aria-label="...">').appendTo(el);
                }
                var btns = $('.btn', btnGroup);
                if (btns.length == 0) {
                    var btn = $('<button type="button" class="btn btn-primary" data-value="隐患">是</button>')
                        .on('click', function () {
                            $(this)
                                .addClass('btn-primary')
                                .siblings()
                                .removeClass('btn-primary')
                                .removeClass('btn-default')
                                .addClass('btn-default');
                            input.val($(this).data('value'));
                        })
                        .appendTo(btns);
                    btn = $('<button type="button" class="btn btn-default">否</button>')
                        .on('click', function () {
                            $(this)
                                .addClass('btn-primary')
                                .siblings()
                                .removeClass('btn-primary')
                                .removeClass('btn-default')
                                .addClass('btn-default');
                            input.val($(this).data('value'));
                        })
                        .appendTo(btns);
                }
                else {
                    btns.on('click', function () {
                        $(this)
                            .addClass('btn-primary')
                            .siblings()
                            .removeClass('btn-primary')
                            .removeClass('btn-default')
                            .addClass('btn-default');
                        input.val($(this).data('value'));
                    });
                }
            });
        }
    },
    userpicker: function (options) {
        var settings = $.extend({}, options);
        return this.each(function () {
            var el = $(this);
            var input = $('input', el);
            var input_display = $('input.display', el);
            $('.input-group-addon', el).on('click', function () {
                var picker = window.userpicker;
                if (picker == null) {
                    picker = $('<form class="picker form-inline">').appendTo('.page');
                    window.userpicker = picker;

                    var filter = $('<div class="filter">').appendTo(picker);

                    var depWrap = $('<div class="dep-wrap form-group">').appendTo(filter);
                    var depSelect = $('<select class="dep form-control">').appendTo(depWrap);
                    depSelect.append("<option>选择部门</option>");
                    var groupWrap = $('<div class="dep-wrap form-group">').appendTo(filter);
                    var groupSelect = $('<select class="group form-control">').appendTo(groupWrap);
                    groupSelect.append("<option>选择班组</option>");

                    var userWrap = $('<div class="user-wrap list list-group">').appendTo(picker);
                    var userItemClick = function () {
                        var li = $(this);
                        var list = li.parents('.list');
                        list.addClass('has-select');
                        li.addClass('active').siblings().removeClass('active');

                        var footer = $('.page-footer', picker);

                        var btnOK = $('.btn-primary', footer);
                        btnOK.show();
                        }
                    };

                var footer = $('.page-footer', picker);
                if (footer.length == 0) {
                    footer = $('<div class="page-footer">').appendTo(picker);
                    var btn = $('<button type="button" class="btn btn-primary">确定</button>')
                        .hide()
                        .on('click', function () {
                            var li = $('.user-wrap .list-group-item.active');
                            input.val(li.data('value'));
                            input.hide();
                            if (input_display.length == 0) {
                                input_display = $('<input class="form-control display">').prependTo(el);
                            }
                            input_display.val($('.list-group-item-heading', li).text());
                            picker.hide();
                        })
                        .appendTo(footer);
                    $('<a class="back"><i class="glyphicon glyphicon-chevron-left"></i></a>')
                        .on('click', function () {
                            event.preventDefault();//屏蔽链接的默认动作
                            event.stopPropagation();
                            picker.hide();
                        })
                        .appendTo(footer);

                    //填充部门
                    $.ajax({
                        url: window.webServices + 'Organizations/?columns[0][data]=IsDepartment&columns[0][search][value]=true',
                        success: function (ret) {
                            depSelect.empty();
                            depSelect.append("<option>选择部门</option>");
                            for (var i = 0; i < ret.data.length; i++) {
                                var item = ret.data[i];
                                depSelect.append("<option value='" + item.Id + "'>" + item.Path + "</option>");
                            }
                        }
                    });
                    depSelect.on('change', function () {
                        var depid = $(this).val();
                        $.ajax({
                            url: window.webServices + 'Organizations/?columns[0][data]=IsGroup&columns[0][search][value]=true&columns[1][data]=PId&columns[1][search][value]=' + depid,
                            success: function (ret) {
                                groupSelect.empty();
                                groupSelect.append("<option>选择班组</option>");
                                for (var i = 0; i < ret.data.length; i++) {
                                    var item = ret.data[i];
                                    groupSelect.append("<option value='" + item.Id + "'>" + item.Name + "</option>");
                                }
                            }
                        });
                        $.ajax({
                            url: window.webServices + 'Users/?columns[0][data]=DepId&columns[0][search][value]=' + depid + (settings.params ? "&" + settings.params : ""),
                            success: function (ret) {
                                userWrap.empty();
                                for (var i = 0; i < ret.data.length; i++) {
                                    var item = ret.data[i];
                                    var li = $('<a href="#" class="list-group-item" data-value="' + item.Id + '">')
                                        .on('click', userItemClick).appendTo(userWrap);
                                    $('<h4 class="list-group-item-heading">' + item.Name + '</h4>').appendTo(li);
                                    $('<p class="list-group-item-text">' + '工号：' + item.Account + '<br />部门班组：' + item.OrganizationPath + '</p>').appendTo(li);
                                }
                            }
                        });
                    });
                    groupSelect.on('change', function () {
                        var groupid = $(this).val();
                        $.ajax({
                            url: window.webServices + 'Users/?columns[0][data]=GroupId&columns[0][search][value]=' + groupid + (settings.params ? "&" + settings.params : ""),
                            success: function (ret) {
                                userWrap.empty();
                                for (var i = 0; i < ret.data.length; i++) {
                                    var item = ret.data[i];
                                    var li = $('<a href="#" class="list-group-item" data-value="' + item.Id + '">')
                                        .on('click', userItemClick).appendTo(userWrap);
                                    $('<h4 class="list-group-item-heading">' + item.Name + '</h4>').appendTo(li);
                                    $('<p class="list-group-item-text">' + '工号：' + item.Account + '<br />部门班组：' + item.OrganizationPath + '</p>').appendTo(li);
                                }
                            }
                        });
                    });
                }
                else {
                    picker.show();
                }
            });
        });
    }
};

(function ($) {

    'use strict';
    /// 定义
    $.fn.buttonCheckBox = ywl.bootstrap.buttonCheckBox;
    $.fn.userpicker = ywl.userpicker;

    ///初始化
    $('[data-provider="userpicker"]').userpicker();
})(jQuery);