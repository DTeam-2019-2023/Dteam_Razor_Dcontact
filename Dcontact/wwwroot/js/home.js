$(document).ready(function () {
    // Show sub sidebar
    $(".sidebar__item.dropdown").click(function () {
        $(".sidebar__list-sub").toggleClass("show");
    });

    // Handle show box statistical
    $(".sidebar__item.btn1").click(function () {
        //
        $(".noaction").addClass("hide");
        //
        $(".bxstatistical").addClass("show");
        $(".bxreport").removeClass("show");
        $(".bxblock").removeClass("show");
        //
        $(this).addClass("active");
        $(".sidebar__item-sub.btn2").removeClass("active");
        $(".sidebar__item-sub.btn3").removeClass("active");
    });

    // Handle show box report
    $(".sidebar__item-sub.btn2").click(function () {
        //
        $(".noaction").addClass("hide");
        //
        $(".bxstatistical").removeClass("show");
        $(".bxreport").addClass("show");
        $(".bxblock").removeClass("show");
        //
        $(".sidebar__item.btn1").removeClass("active");
        $(this).addClass("active");
        $(".sidebar__item-sub.btn3").removeClass("active");
    });

    // Handle show box block
    $(".sidebar__item-sub.btn3").click(function () {
        //
        $(".noaction").addClass("hide");
        //
        $(".bxstatistical").removeClass("show");
        $(".bxreport").removeClass("show");
        $(".bxblock").addClass("show");
        //
        $(".sidebar__item.btn1").removeClass("active");
        $(".sidebar__item-sub.btn2").removeClass("active");
        $(this).addClass("active");
    });

    // Handle click scroll into view
    $(".scrolldown .scrolldown__title").click(function () {
        setTimeout(function () {
            document.getElementById("section01").scrollIntoView({
                behavior: "smooth",
                block: "center",
            });
        }, 400);
    });
    $(".scrolldown .fa-solid.fa-arrow-down-long").click(function () {
        setTimeout(function () {
            document.getElementById("section01").scrollIntoView({
                behavior: "smooth",
                block: "center",
            });
        }, 400);
    });

    // Handle scroll down
    $(window).scroll(function () {
        var distanceScroll = $("html, body").scrollTop();
        // Header
        if (distanceScroll > 100) {
            $("header").addClass("show");
        } else {
            $("header").removeClass("show");
        }
        // Scroll To Top
        if (distanceScroll > 300) {
            $(".scrollTop").addClass("scroll");
        } else {
            $(".scrollTop").removeClass("scroll");
        }
    });

    // Handle scroll to top
    $(".scrollTop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 400);
        return false;
    });
});