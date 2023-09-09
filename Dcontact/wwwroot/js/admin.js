$(document).ready(function () {

    // Show sub sidebar
    $(".sidebar__item.dropdown").click(function () {
        $(".sidebar__list-sub").toggleClass("show");
    });

    // Handle show box statistical
    $(".sidebar__item.btn1").click(function () {
        //
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

    $(".statistical__table").on("click", "tbody tr td", function (e) {
        //do some thing
        if ($(this).hasClass('breakText')) {
            $(this).removeClass('breakText');
        } else {
            $(this).addClass('breakText');
        }
    })

    $(".block__table").on("click", "tbody tr td", function (e) {
        //do some thing
        if ($(this).hasClass('breakText')) {
            $(this).removeClass('breakText');
        } else {
            $(this).addClass('breakText');
        }
    })

    $(".report__table").on("click", "tbody tr td", function (e) {
        //do some thing
        if ($(this).hasClass('breakText')) {
            $(this).removeClass('breakText');
        } else {
            $(this).addClass('breakText');
        }
    })

    //$(".report__table tbody tr td").click(function () {
    //    if ($(this).hasClass('breakText')) {
    //        $(this).removeClass('breakText');
    //    } else {
    //        $(this).addClass('breakText');
    //    }
    //});

    // đây là phần unblock and block của dcontact cũ
    // $('.main-lock').on('click', '.btn-lock', function () {
    //     id_User = $(this).parent().attr('id');
    //     console.log($(this).children('p').text());
    //     if ($(this).children('p').text() === 'BLOCK') {
    //         $.post("/Admin/Block_User", { id_user: id_User }).done(function (data) {
    //             //$("#" + idRow).parent().remove();
    //             //$(".btn-lock").html('Unblock');
    //         })
    //         $(this).children('p').html("UNBLOCK")
    //     } else {
    //         $.post("/Admin/Unblock_User", { id_user: id_User }).done(function (data) {
    //             //$("#" + idRow).parent().remove();
    //             //$(".btn-lock").html('Unblock');

    //         })
    //         $(this).children('p').html("BLOCK")
    //     }
    // });

         $('select').on('change', function (e) {
                var optionSelected = $("option:selected", this);
                var valueSelected = this.value;
                var id = $(optionSelected).attr('id');
             $.ajax({
                 type: "POST",
                 url: "Admin/Index?handler=UpdateRole",
                 data: {
                     ID: id,
                     Role: valueSelected
                 },
                 headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                 success: function (msg) {
                     console.log("msg: " + msg);
                 },
                 failure: function (response) {
                     alert(response.responseText);
                 },
                 error: function (e) {
                     console.log("Request Failed:" + JSON.stringify(e));
                 }
             });

         });

    

    $('.report__table-item-btn').on('click', function (e) {

        var IdReport = $(this).parent().parent().attr('id');
        var IdUser = $(this).parent().parent().find('#IdUser').text();
        var text = $(this).text();
        $(this).parent().parent().remove();


        $.ajax({
            type: "POST",
            url: "Admin/Index?handler=HandleReport",
            data: {
                IDUser: IdUser,
                IDReport: IdReport,
                buttonText: text
            },
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            success: function (msg) {
                console.log("msg: " + msg);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed:" + JSON.stringify(e));
            }
        });
    });

 

    $('#tbody_ban').on('click', '.unblock', function () {

        var IdUser = $(this).parent().parent().find('#IdUser').text();
        $(this).parent().parent().remove();

        $.ajax({
            type: "POST",
            url: "Admin/Index?handler=UnBan",
            data: {
                IDUser: IdUser
            },
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            success: function (msg) {
                console.log("msg: " + msg);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed:" + JSON.stringify(e));
            }
        });

    });
    
});

