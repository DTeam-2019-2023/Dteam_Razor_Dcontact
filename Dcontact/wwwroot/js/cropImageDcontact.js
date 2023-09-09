let $croppi, rawImg;
let width = 240, height = 320;
let widthAvt = 200, heightAvt = 200;
var imgBg = "", imgAvt = "";
function readFileBg(input) {
    if (input.files && input.files[0]) {
        rawImg = "";
        var reader = new FileReader();
        reader.onload = function (e) {
            rawImg = e.target.result;

            croppi = $('.background_croppie').croppie({
                viewport: {
                    width: width,
                    height: height,
                    type: 'square'
                },
                customClass: "viewport_custom",
                boundary: {
                    width: width + 200,
                    height: height + 50
                }
            });
            croppi.croppie('bind', {
                url: rawImg,
            }).then(function () {
                croppi.croppie('setZoom', 0);
            });
        }
        reader.readAsDataURL(input.files[0]);
    } else {
        swal("Sorry - you're browser doesn't support the FileReader API");
    }
}

function readFileAvt(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            rawImg = e.target.result;

            croppi = $('.avatar_croppie').croppie({
                viewport: {
                    width: widthAvt,
                    height: heightAvt,
                    type: 'circle'
                },
                customClass: "viewport_custom",
                boundary: {
                    width: widthAvt + 200,
                    height: heightAvt + 50
                }
            });
            croppi.croppie('bind', {
                url: rawImg,
            }).then(function () {
                croppi.croppie('setZoom', 0);
            });
        }
        reader.readAsDataURL(input.files[0]);
    } else {
        swal("Sorry - you're browser doesn't support the FileReader API");
    }
}

$(document).ready(function () {
    $('#selectBg').on('change', function () {
        readFileBg(this);
        $(".background_croppie").fadeIn();
        $('#btnApplyBg').css("display", "flex");
        $('.background__helper').css('display', 'none');
        $(".overlay__background-box").css('display', 'flex');
    })

    $('#selectAvt').on('change', function () {
        readFileAvt(this);
        $("#btnApplyAvt").css('display', 'flex');
        $('.avatar__helper').css('display', 'none');
    })
    

})


$(document).on('click', '#applyBg', function (ev) {
    croppi.croppie('result', {
        type: 'base64',
        size: 'viewport',
        format: 'jpg'
    }).then(function (resp) {
        imgBg = `${resp}`;
        $('.box__top').css('background-image', `url(${resp})`);
        croppi.croppie('destroy');
        $('#btnApplyBg').css('display', 'none');
        $('.background__helper').css('display', 'flex');
        $('.overlay__background-box').css("display", 'none');
        var tempApply = $('.box__top').attr('idTemplate');
        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=UpdateBackground",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                img: `${resp}`,
                idTemplateApply: tempApply
            },
            success: function (msg) {
                console.log(msg);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed: " + JSON.stringify(e));
            }
        });
    });

});


$(document).on('click', '#applyAvt', function (ev) {
    croppi.croppie('result', {
        type: 'base64',
        size: 'viewport',
        format: 'jpg'
    }).then(function (resp) {
        imgAvt = `${resp}`;
        $('.box__avt-img').attr('src', `${resp}`);
        croppi.croppie('destroy');
        $('.avatar__helper').css('display', 'flex');
        $('#btnApplyAvt').css('display', 'none');
        var tempApply = $('.box__top').attr('idTemplate');

        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=UpdateAvatar",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                img: `${resp}`,
                idTemplateApply: tempApply
            },
            success: function (msg) {
                console.log(msg);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed: " + JSON.stringify(e));
            }
        });
    });
});