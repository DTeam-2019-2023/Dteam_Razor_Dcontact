var urlRegex = /^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$/;
const rgb2hex = (rgb) => `#${rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/).slice(1).map(n => parseInt(n, 10).toString(16).padStart(2, '0')).join('')}`;

var idRow;
var color;
var bullet;
var font;
var text;
var link;
var code;
var bdate;
var scrolled = 0;
var idTemp;

var currentRow = {
    id: "",
    text: "",
    link: "",
    code: "",
    birth: "",
    font: "",
    color: "",
    bullet: "",
    rowObj: null,
    isUpdate: false,
    updateRow() {
        if (this.rowObj != null) {
            this.id = $(this.rowObj).attr('idrowcontent');
            this.text = $(this.rowObj).children('.item__content').children('label').text();
            this.link = $(this.rowObj).attr('rowlink');
            this.code = $(this.rowObj).attr('rowCode');
            this.birth = $(this.rowObj).attr('rowBirth');
            this.font = $(this.rowObj).children('.item__content').children('label').css('font-family');
            this.color = $("#colorpicker").val();
            //this.color = rgb2hex($(this.rowObj).css('background-color'));
            this.bullet = $(this.rowObj).children('i').attr('class');
            this.isUpdate = true;
            $(this.rowObj).trigger("UpdateRow");
        } else {
            alert("Please choose row");
        }
    },
    setRow(e) {
        this.rowObj = e;
        this.id = $(this.rowObj).attr('idrowcontent');
        this.text = $(this.rowObj).children('.item__content').children('label').text();
        this.link = $(this.rowObj).attr('rowlink');
        this.code = $(this.rowObj).attr('rowCode');
        this.birth = $(this.rowObj).attr('rowBirth');
        this.font = $(this.rowObj).children('.item__content').children('label').css('font-family');
        this.color = rgb2hex($(this.rowObj).css('background-color'));
        this.bullet = $(this.rowObj).children('i').attr('class');
    }

};

var currentTemplate = {
    id: "",
    bg: "",
    color: "",
    avt: "",
    tempObj: null,
    isUpdate: false,
    setTemp(e) {
        this.tempObj = e;
        this.id = $(this.tempObj).attr('idTemplate');
        this.bg = $(this.tempObj).children('div').css('background-image').replace('url(', '').replace(')', '').replace(/\"/gi, "");;
        this.color = $(this.tempObj).children('div').children('div').css('background-color');
        this.avt = $(this.tempObj).children('img').attr('src');
    }
}

function bindingRow() {
    $('#properties-name').val(currentRow.text); /*gan gia tri row sangurl-area*/
    $('#properties-link').val(currentRow.link);
    $(`#text-font option[value="${currentRow.font}"]`).attr("selected", true);
    $('#colorpicker').val(currentRow.color);
    $('#colorPickericon').css('color', currentRow.color);
}

function bindingTemplate() {
    $('.box__top').css('background-image', currentTemplate.bg);
    $('.properties__avatar-box').children('img').attr('src', currentTemplate.avt);
    $('.item').css('background-color', currentTemplate.color);
}

function getRowColor(e, v) {
    $(e).css('background-color', v);
}

$(document).ready(function () {

    $('.item').on('UpdateRow', function () {
        if ($(currentRow.isUpdate)) {
            $.ajax({
                origin: '*',
                type: "post",
                url: "EditDcontact?handler=UpdateRow",
                headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                data: {
                    idContent: currentRow.id,
                    rowText: currentRow.text,
                    rowLink: currentRow.link,
                    rowFont: currentRow.font,
                    rowColor: currentRow.color,
                    rowCode: currentRow.code,
                    rowBirth: currentRow.birth,
                    rowBullet: currentRow.bullet
                },
                success: function (msg) {
                    console.log(msg);
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (e) {
                    console.log("Request failed: " + JSON.stringify(e));
                }
            });
        }

    })

    $('.list').on('click', '.item', function () {
        // REMOVE TEMPLATE ACTION
        if ($('.template__content').css('display') == 'block') {
            $('.template__content').removeClass('show');
            $('.properties__content').addClass('show');
            $('.action__item.template').removeClass('active');
            $('.btn__dn').removeClass('active');
        }

        //REMOVE AVATAR ACTION
        if ($('.avatar__content').css('display') == 'block') {
            $('.avatar__content').removeClass("show");
            $('.properties__content').addClass('show');
            $('.action__item.avatar').removeClass('active');
            $('.btn__dn').removeClass('active');
        }

        //REMOVE BACKGORUND ACTION
        if ($('.background__content').css('display') == 'block') {
            $('.background__content').removeClass("show");
            $('.properties__content').addClass('show');
            $('.action__item.background').removeClass('active');
            $('.btn__dn').removeClass('active');
        }
        $('.action__item.properties').addClass('active');
        currentRow.setRow(this);
        bindingRow();
    });

    $('.btn__set-code').on('click', function () {
        $('.form__set-code').toggleClass('show');
    });

    $('.btn__set-birthday').on('click', function () {
        $('.form__set-birthday').toggleClass('show');
    });


    //chưa làm apply code và age
    $('.btn_apply_setCode').on('click', function () {
        code = $('#properties-code').val();
        if (code < 1000 || code > 9999) {
            // $('.err-code').show();
            $('#properties-code').css('border', '2px solid #e74c3c');
        } else {
            $('#properties-code').attr('code', code);
            $('#properties-code').css('border', '2px solid #2ecc71');
            // $('.err-code').hide();
            //updateRow(idRow);
        }
    });

    $('.btn_apply_setDate').on('click', function () {
        var age = $('#datepicker').val();
        //[ ] bday la kieu date
        if (age < 14 || age > 99) {
            // $('.err-age').show();
            $('#datepicker').css('border', '2px solid #e74c3c');
        } else {
            bdate = '1111-11-1';
            $('#datepicker').css('border', '2px solid #2ecc71');
            // $('.err-age').hide();
        }
    });

    // COLOR

    $('#colorpicker').on('change', function () {
        var colorVal = $("#colorpicker").val();
        $(`.item[idrowcontent="${currentRow.id}"]`).css('background-color', colorVal);
        //getRowColor($(`.item[idrowcontent="${currentRow.id}"]`), colorVal);
        $('#colorPickericon').css('color', colorVal);
        currentRow.updateRow();
    });

    // TITLE
    $('#properties-name').on('keyup', function () {
        var txt = $('#properties-name').val().toString();
        $(`.item[idrowcontent="${currentRow.id}"] div.item__content label`).text(txt);
        if (txt == "") {
            $("#properties-name").css('border-color', 'red');
        } else {
            $("#properties-name").css('border-color', 'var(--border-color)');
        }
    }).on('focusout', function () {
        currentRow.updateRow();
    })

    // LINK
    $('#properties-link').on('change', function () {
        var link = $("#properties-link").val();
        if (urlRegex.test(link) == false) {
            $("#properties-link").css('border-color', 'red');
        } else {
            $("#properties-link").css('border-color', '#2ecc71');
            $(`.item[idrowcontent="${currentRow.id}"]`).attr('rowLink', link);
            currentRow.updateRow();
            alert('check');
        }
    })

    // FONT
    $('#text-font').on('change', function () {
        var fontVal = $('#text-font option:selected').val();
        $(`.item[idrowcontent="${currentRow.id}"] div.item__content label`).css('font-family', fontVal);
        currentRow.updateRow();
    });

    // BULLET
    $('.icon__item').on('click', function () {
        var iconVal = $(this).children('i').attr('class');
        var iconApply = $(`.item[idrowcontent="${currentRow.id}"] > i`).attr('class');
        if (iconVal == iconApply) {
            $(`.item[idrowcontent="${currentRow.id}"] > i`).attr('class', '');
        } else {
            $(`.item[idrowcontent="${currentRow.id}"] > i`).attr('class', iconVal);
        }
        currentRow.updateRow();
    })

    $('.template__list').on('click', '.overlay__template--list-item', function () {
        $('#applyTemp').removeAttr('disabled'); // active apply button
        var idTemplate = $(this).attr('idtemplate');
        var dcontact = $('#dcontact').attr('templateApply', idTemplate);

        //var bg = $(this).css("background-image");
        //$('.box__properties').css('background-image', bg);

        //var clr = $('.template__list-item-row').css("background-color");
        //$('.box__properties .item').css('background-color', clr);

        //var avt = $('.template__list-avt').attr("src");
        //$('.box__avt-img').attr('src', avt.replace(/.*\s?url\([\'\"]?/, '').replace(/[\'\"]?\).*/, ''));

        //$.post("/Dcontact/User/updateImage", { piece: 'updateTemplate', path: src }).done(function (data) {
        //    console.log(data + 'card background has been updated');
        //});
    })

    // Handle Apply Template
    $('#applyTemp').on('click', function () {
        var idTemp = $('#dcontact').attr('templateApply');
        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=ApplyTemplate",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                idTemplateApply: idTemp
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
        })
    })

    //Handle Cancel Template
    $("#cancelTemp").on('click', function () {
        var currentIDTemp = $('#dcontact').attr('idTemplate');

    })

    $('#image-background').on('change', function () {
        $('.modal__background__overlay').addClass('active');
        $('.change__background').addClass('active');
    });

    $('#image-avatar').on('change', function () {
        $('.modal__avatar__overlay').addClass('active');
        $('.change__avatar').addClass('active');
    });

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

    // Handle active action item show box edit
    var btn1 = $(".action__item.template");
    var btn2 = $(".action__item.avatar");
    var btn3 = $(".action__item.background");
    var btn4 = $(".action__item.properties");
    var btn5 = $(".action__item.securities");
    var templateContent = $(".template__content");
    var avatarContent = $(".avatar__content");
    var backgroundContent = $(".background__content");
    var propertiyContent = $(".properties__content");
    $(btn1).click(function () {
        //
        $(this).addClass("active");
        $(btn2).removeClass("active");
        $(btn3).removeClass("active");
        $(btn4).removeClass("active");
        $(btn5).removeClass("active");
        //
        $(templateContent).addClass("show");
        $(avatarContent).removeClass("show");
        $(backgroundContent).removeClass("show");
        $(propertiyContent).removeClass("show");

        $(".btn__dn").addClass("active");
    });
    $(btn2).click(function () {
        //
        $(btn1).removeClass("active");
        $(this).addClass("active");
        $(btn3).removeClass("active");
        $(btn4).removeClass("active");
        $(btn5).removeClass("active");

        $(templateContent).removeClass("show");
        $(avatarContent).addClass("show");
        $(backgroundContent).removeClass("show");
        $(propertiyContent).removeClass("show");

        $(".btn__dn").removeClass("active");
    });
    $(btn3).click(function () {

        $(btn1).removeClass("active");
        $(btn2).removeClass("active");
        $(this).addClass("active");
        $(btn4).removeClass("active");
        $(btn5).removeClass("active");

        $(templateContent).removeClass("show");
        $(avatarContent).removeClass("show");
        $(backgroundContent).addClass("show");
        $(propertiyContent).removeClass("show");

        $(".btn__dn").removeClass("active");
    });
    $(btn4).click(function () {

        $(btn1).removeClass("active");
        $(btn2).removeClass("active");
        $(btn3).removeClass("active");
        $(this).addClass("active");
        $(btn5).removeClass("active");

        $(templateContent).removeClass("show");
        $(avatarContent).removeClass("show");
        $(backgroundContent).removeClass("show");
        $(propertiyContent).addClass("show");

        $(".btn__dn").removeClass("active");
    });
    $(btn5).click(function () {
        $(btn1).removeClass("active");
        $(btn2).removeClass("active");
        $(btn3).removeClass("active");
        $(btn4).removeClass("active");
        $(this).addClass("active");

        $(".btn__dn").removeClass("active");
    });

    // Handle show/close modal create new template
    $(".btn__addNew-template").click(function () {
        $(".modal__overlay").addClass("active");
        $(".template__addnew").addClass("active");
    });
    $(".modal__title .fa-circle-xmark").click(function () {
        $(".modal__overlay").removeClass("active");
        $(".template__addnew").removeClass("active");
    });
    //$(".modal__overlay").click(function () {
    //    $(this).removeClass("active");
    //    $(".template__addnew").removeClass("active");
    //});
    //$(document).on("keydown", function (e) {
    //    if (e.keyCode === 27) {
    //        $(".modal__overlay").removeClass("active");
    //        $(".template__addnew").removeClass("active");
    //    }
    //});
    $(".modal__title .fa-circle-xmark").click(function () {
        $(".modal__avatar__overlay").removeClass("active");
        $(".change__avatar").removeClass("active");
    });
    $(".modal__avatar__overlay").click(function () {
        $(".modal__avatar__overlay").removeClass("active");
        $(".change__avatar").removeClass("active");
    });
    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            $(".modal__avatar__overlay").removeClass("active");
            $(".change__avatar").removeClass("active");
        }
    });

    $(".modal__title .fa-circle-xmark").click(function () {
        $(".modal__background__overlay").removeClass("active");
        $(".change__background").removeClass("active");
    });
    $(".modal__background__overlay").click(function () {
        $(".modal__background__overlay").removeClass("active");
        $(".change__background").removeClass("active");
    });
    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            $(".modal__background__overlay").removeClass("active");
            $(".change__background").removeClass("active");
        }
    });

    // Handle show popup set birthday
    $("#datepicker").datepicker({
        dateFormat: "dd-mm-yy",
        changeMonth: true,
        changeYear: true,
    });
    // $('.btn__set-birthday').click(function () {
    //     $('.form__set-birthday').toggleClass('show');
    // });
    $(".fa-calendar-days").click(function () {
        $(".form__set-birthday-input").focus();
        console.log("first");
    });

    // Handle show/close modal change avatar
    $(".section__btn.btn__change__avatar").click(function () {
        $(".modal__avatar__overlay").addClass("active");
        $(".change__avatar").addClass("active");
    });
    $(".modal__title .fa-circle-xmark").click(function () {
        $(".modal__avatar__overlay").removeClass("active");
        $(".change__avatar").removeClass("active");
    });
    $(".modal__avatar__overlay").click(function () {
        $(".modal__avatar__overlay").removeClass("active");
        $(".change__avatar").removeClass("active");
    });
    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            $(".modal__avatar__overlay").removeClass("active");
            $(".change__avatar").removeClass("active");
        }
    });

    // Handle show/close modal change background
    $(".section__btn.btn__change-background").click(function () {
        $(".modal__background__overlay").addClass("active");
        $(".change__background").addClass("active");
    });
    $(".modal__title .fa-circle-xmark").click(function () {
        $(".modal__background__overlay").removeClass("active");
        $(".change__background").removeClass("active");
    });
    $(".modal__background__overlay").click(function () {
        $(".modal__background__overlay").removeClass("active");
        $(".change__background").removeClass("active");
    });
    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            $(".modal__background__overlay").removeClass("active");
            $(".change__background").removeClass("active");
        }
    });

    // Handle show popup set code
    // $('.btn__set-code').click(function () {
    //     $('.form__set-code').toggleClass('show');
    // });

    // Handle show popup set birthday
    $("#datepicker").datepicker({
        dateFormat: "dd-mm-yy",
        changeMonth: true,
        changeYear: true,
    });
    // $('.btn__set-birthday').click(function () {
    //     $('.form__set-birthday').toggleClass('show');
    // });
    $(".fa-calendar-days").click(function () {
        $(".form__set-birthday-input").focus();
        console.log("first");
    });

    // Handle add row in tab properties
    $(".add__row.plus").click(function () {
        //$(".list").prepend(
        //    `<div class="item"><div class="item__content"></div><i class="fa-regular fa-trash-can"></i></div>`
        //);
        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=AddRow",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            success: function (msg) {
                console.log(msg);
                $(".list").prepend(
                    `<div class="item" idrowcontent=${msg.id} style="background-color: ${msg.bg}">
                        <i class=""></i>
                        <div class="item__content">
                            <label class="item__content--text" style="font-family: ${msg.font}">
                                ${msg.txt}
                            </label>
                        </div>
                        <span><i class="fa fa-regular fa-trash-can"></i></span>
                    </div>`
                );
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed: " + JSON.stringify(e));
            }
        })
    });

    // Handle remove row in tab properties
    $("div.list").on("click", ".fa.fa-regular.fa-trash-can", function () {
        var idRowContent = $(this).parents().parents().attr('idRowContent');
        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=DeleteRow",
            data: {
                idContent: idRowContent
            },
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            success: function (msg) {
                console.log(msg);
                $(`.item[idRowContent=${msg.id}]`).remove();
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed: " + JSON.stringify(e));
            }
        })
    });

    //Handle add new template
    $("#btnApplyNewTemplate").on("click", function () {
        $(".modal__overlay").removeClass("active");
        var txtName = $('#template_name').val();
        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=AddTemplate",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                nameTemp: txtName
            },
            success: function (msg) {
                $('.template__list').prepend(
                    `<div class="overlay__template--list-item" idTemplate="${msg.id}" idBackground="${msg.idBg}">
                        <div class="template__list-item" style="background-image: url('${msg.background}')">
                            <img class="template__list-avt" src="${msg.avatar}" idAvatar="${msg.idAvt}"/>
                            <div class="template__list-item-row"></div>
                        </div>
                     <div class="template__item-name">${msg.name}</div>
                    </div>`
                )
                //$('.box__top').attr('idTemplate', msg.id);
                //$('.box__top').attr('idBackground', msg.idBg);
                //$('.box__top').attr('idAvatar', msg.idAvt);
                //$('.box__top').css('background-image', `url('${msg.background}')`);
                
                console.log(msg);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed: " + JSON.stringify(e));
            }
        })
    })

    //Handle Remove Template
    $('.delTemp').on('click', function () {
        var idTemp = $(this).parents().attr('idTemplate');
        $.ajax({
            type: "POST",
            url: "EditDcontact?handler=DeleteTemplate",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                idTemplate: idTemp
            },
            success: function (msg) {
                console.log(msg);
            },
            failure: function (response) {
                alert('Template is using. Please choose another template before delete !!! ');
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request Failed: " + JSON.stringify(e));
            }
        });
    });

    // Handle color picker
    $('#colorPickericon').click(function () {
        $('#colorpicker').click();
    })

    //Handle scroll in icon list

    var heightIcon = $('.icon__item').outerHeight();
    $('.icon__list-btnDown').on('click', function () {
        scrolled += heightIcon;
        $('.icon__list').animate({
            scrollTop: scrolled
        });
    })

    $('.icon__list-btnUp').on('click', function () {
        scrolled -= heightIcon;
        $('.icon__list').animate({
            scrollTop: scrolled
        });
    })

});
