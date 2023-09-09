/// <reference path="report.js" />
function copyTextFromElement(elementID) {
    let element = document.getElementById(elementID); //select the element
    let elementText = element.textContent; //get the text content from the element
    copyText(elementText); //use the copyText function below
}

//If you only want to put some Text in the Clipboard just use this function
// and pass the string to copied as the argument.
function copyText(text) {
    navigator.clipboard.writeText(text);
}


$(document).ready(function () {
    $(".header").load('headerlogin.html');
    $('.dteam').load('dteam.html');
    $("#toggleBg").on("click", function () {
        // $(".toggle--backgroundContent").css("display", "flex");
        $(".toggle--backgroundContent").toggle();
    });

    $("#closeModalAvt").on("click", function () {
        $("#modalAvt").hide();
    })

    $("#closeModalBg").on("click", function () {
        //$("#modalBg").hide();
        $("#modalBg").removeClass("show");
    })

    $('.btnCancel').on('click', function () {
        $('.cmnModal').hide();
    })

    $('#toggleAvt').on("click", function () {
        $('.toggle--avatarContent').toggle();
    })

    $('#toggleContent').on("click", function () {
        $('.toggle--contentContent').toggle();
    })

    // Function copies
    $('.btnCopy').on("click", function () {
        $('.copies').show();
        setTimeout(function () {
            $('.copies').hide();
        }, 1000);
    })

    $("#overlayPopup").on('click', function () {
        $('#overlayPopup').fadeOut();
    })

    /* QR CODE */
    $('#toggleQR').on('change', function () {
        var data = $("#linkValue").text();
        if ($(this).is(':checked')) {
            generateQR(data);
            $('.qrLink').show();
        } else {
            $('.qrCode__container').html("");
            $('.qrLink').hide();
        }
    })


    function generateQR(data) {
        var srcQR = "http://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=" + data;
        var img = '<img class="img-qr" src="' + srcQR + '">';
        $('.qrCode__container').html(img);
    }
});