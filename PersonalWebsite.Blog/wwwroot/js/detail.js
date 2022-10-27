layui.use(['layer', 'jquery'], function () {
    var $ = layui.jquery,
        layer = layui.layer;
    detail.Init($);//初始化共用js

});

var detail = {};
detail.Init = function ($) {
    //如果你是采用模版自带的编辑器，你需要开启以下语句来解析。
    $('.detail-body').each(function () {
        var othis = $(this), html = othis.html();
        othis.html(parse(html));
    });

    //相册
    if ($(window).width() > 750) {
        layer.photos({
            photos: '.photos'
            , zIndex: 9999999999
            , anim: -1
            , shift: 1  //0-6的选择，指定弹出图片动画类型，默认随机
        });
    } else {
        $('body').on('click', '.photos img', function () {
            window.open(this.src);
        });
    }

    function parse(content) {
        //支持的html标签
        var html = function (end) {
            return new RegExp('\\n*\\[' + (end || '') + '(pre|hr|div|span|p|table|thead|th|tbody|tr|td|ul|li|ol|li|dl|dt|dd|h2|h3|h4|h5)([\\s\\S]*?)\\]\\n*', 'g');
        };
        content = escape(content || '') //XSS
            .replace(/img\[([^\s]+?)\]/g, function (img) {  //转义图片
                return '<img src="https://todo369.top/FileServer' + img.replace(/(^img\[)|(\]$)/g, '') + '">';
            }).replace(/@(\S+)(\s+?|$)/g, '@<a href="javascript:;" class="fly-aite">$1</a>$2') //转义@
            .replace(/face\[([^\s\[\]]+?)\]/g, function (face) {  //转义表情
                var alt = face.replace(/^face/g, '');
                return '<img alt="' + alt + '" title="' + alt + '" src="' + fly.faces[alt] + '">';
            }).replace(/a\([\s\S]+?\)\[[\s\S]*?\]/g, function (str) { //转义链接
                var href = (str.match(/a\(([\s\S]+?)\)\[/) || [])[1];
                var text = (str.match(/\)\[([\s\S]*?)\]/) || [])[1];
                if (!href) return str;
                var rel = /^(http(s)*:\/\/)\b(?!(\w+\.)*(sentsin.com|layui.com))\b/.test(href.replace(/\s/g, ''));
                return '<a href="' + href + '" target="_blank"' + (rel ? ' rel="nofollow"' : '') + '>' + (text || href) + '</a>';
            }).replace(html(), '\<$1 $2\>').replace(html('/'), '\</$1\>') //转移HTML代码
            .replace(/\n/g, '<br>') //转义换行   
        return content;
    }

    function escape(html) {
        return String(html || '').replace(/&(?!#?[a-zA-Z0-9]+;)/g, '&amp;')
            .replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/'/g, '&#39;').replace(/"/g, '&quot;');
    }
}
