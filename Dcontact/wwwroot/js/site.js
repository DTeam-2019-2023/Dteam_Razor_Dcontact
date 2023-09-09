// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function ($) {

    $(".profile").on("click", function () {
        $(".header__profile").toggle();
    });

    $("#profile").on("click", function () {
        $(".overlay__profile--container").show();
        $(".header__profile").hide();
    });

    $(".profile--btnClose").on("click", function () {
        $(".overlay__profile--container").hide();
    });

    $("#update").on("click", function () {
        $(".overlay__updatePassword--container").show();
        $(".overlay__profile--container").hide();
    });

    $("#closeUpdate").on("click", function () {
        $(".overlay__updatePassword--container").hide();
        $(".overlay__profile--container").show();
    });

    $('.overlay__profile--container').on('click', function () {
        $(this).hide();
    })

    $('.overlay__updatePassword--container').on('click', function () {
        $(this).hide();
        $(".overlay__profile--container").show();
    })
    $(window).on('scroll', function () {
        //ADD .TIGHT
        // if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
        if ($(window).scrollTop() + $(window).height() > $('._container').outerHeight() + $('header').height()) {
            $('body').addClass('tight');
        } else {
            $('body').removeClass('tight');
        }
    });

    //BACK TO PRESENTATION MODE
    $("html").on("click", "body.tight ._container", function () {
        $('html, body').animate({
            scrollTop: $('._container').outerHeight() - $(window).height()
        }, 500);
    });

    //Popup
    $(".overlay__popup").on("click", function () {
       $(".overlay__popup").hide();
    });
    // ========================

});

