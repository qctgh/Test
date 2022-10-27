layui.use(['jquery', 'layer', 'util'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        util = layui.util;
    util.fixbar();
    //导航控制
    master.start($);

    $(function () {
        var txt = "<span class='txt'></span>";
        $('body').append(txt);
        var txted = $(".txt");
        txted.css({
            position: "absolute",
            color: "#008ed4"
        });
        var Animated = function (x) {
            x.stop().animate({
                top: "-=80px",
                opacity: '1'
            }, 500, function () {
                $(this).animate({
                    opacity: "0"
                }, 500);
            });
        };
        $(document).on("click", function (e) {
            var attr = ["自由", "民主", "富强", "希望", "和平", "积极", "向上", "进取"];
            var mathText = attr[Math.floor(Math.random() * attr.length)];
            //获取鼠标的位置
            var x = e.pageX - 32 + "px";
            var y = e.pageY - 18 + "px";
            txted.text(mathText);
            txted.css({
                "left": x,
                "top": y
            });
            Animated(txted);
        });
    });
});
var slider = 0;
var pathname = window.location.pathname.replace('Read', 'Article');
var master = {};
master.start = function ($) {
    $('#nav li').hover(function () {
        $(this).addClass('current');
    }, function () {
        var href = $(this).find('a').attr("href");
        if (pathname.indexOf(href) == -1) {
            $(this).removeClass('current');
        }
    });
    selectNav();
    function selectNav() {
        var navobjs = $("#nav a");
        $.each(navobjs, function () {
            var href = $(this).attr("href");
            if (pathname.indexOf(href) != -1) {
                $(this).parent().addClass('current');
            }
        });
    };
    $('.phone-menu').on('click', function () {
        $('#nav').toggle(500);
    });
    $(".blog-user img").hover(function () {
        var tips = layer.tips('点击退出', '.blog-user', {
            tips: [3, '#009688'],
        });
    }, function () {
        layer.closeAll('tips');
    });


};
