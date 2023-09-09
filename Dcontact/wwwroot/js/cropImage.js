
var $uploadCrop,
    tempFilename,
    rawImg,
    imageId,
    colorBackground,
    /* frontDcard,*/      //Variable store image fornt card
    /* backDcard,  */     //Variable store image back card

    urlBackground;

async function doMerge() {
    return new Promise(function (resolve, reject) {
        var data = {
            front: reply_click(),
            back: createImageBackDcard()
        };

        //var back =  createImageBackDcard();
        resolve({
            front: data.front,
            back: data.back
        })
    })
};

const cardData = {
    front: "",
    back: ""
}

const content = {
    contentToggle: false,
    position: "",
    text: "",
    direction: {
        topLeft: {
            x: 15,
            y: 20
        },
        Center: {
            x: 140,
            y: 100
        },
        topRight: {
            x: 270,
            y: 20
        },
    }, getDirection() {
        let direction = {
            x: 0,
            y: 0
        };



        var canvas = document.getElementById("canvas");
        var ctx = canvas.getContext("2d");
        var sizez = ctx.measureText(this.text).width;
        switch (content.position) {
            case "topLeft":
                direction = { ...this.direction.topLeft };
                if ($('#topLeft--avt').css('background-image') != "none") {
                    direction.y += 80;
                }
                break;
            case "center":
                direction = { ...this.direction.Center };
                direction.x = width / 2 - (sizez / 2);
                //direction.y = height / 2 - (sizez / 2);
                if ($('#center--avt').css('background-image') != "none") {
                    direction.y += 60;
                }
                break;
            case "topRight":
                direction = { ...this.direction.topRight };
                if ($('#topRight--avt').css('background-image') != "none") {
                    direction.y += 80;
                }
                direction.x = canvas.width - sizez - 8; // 8 is padding
                break;
        };
        return direction;
    }
} //object set text position

const avatar = {
    avatarToggle: false, //check toggle avatar open or not
    position: "",
    url: "",
    avatarPos: null,
    setAvatar(e) {
        if (this.url === "") {
            /*alert("Url empty");*/
            return;
        }
        if (this.avatarPos != null) {
            $(this.avatarPos).css("background-image", "none");
        }
        this.avatarPos = e;
        $(this.avatarPos).css("background-image", `url(${this.url})`);
    }

}//object set avatar position

const background = {
    url: ""
} //object url background

let croppi; width = 323.52; height = 204.48;
let widthAvt = 70; heightAvt = 70;//equal css
var img1 = "", img2 = "", img3 = "", imgColorBG = "", center, topLeft, topRight;

/* doc va kiem tra file*/
function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            rawImg = e.target.result; // luu anh vao bien rawImg

            $('#modalBg').show(function () {// sao khi cmnModal show len thi se chay ham nay
                croppi = $('#upload-demo').croppie({ // croppie se duoc chen vao tag nay "#upload-demo"
                    viewport: { //set chieu rong chieu cao
                        width: width,
                        height: height,
                        type: 'square'
                    },
                    customClass: "viewport_custom",
                    boundary: { //set khoan bu cua hinh anh duoc crop
                        width: width + 200,
                        height: height + 50
                    }
                });
                /*binding hinh anh thêm setZoom vao modal*/
                croppi.croppie('bind', {
                    url: rawImg,
                }).then(function () {
                    croppi.croppie('setZoom', 0);
                });
            })
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        swal("Sorry - you're browser doesn't support the FileReader API");
    }
}


function readFileAvt(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            rawImg = e.target.result; // luu anh vao bien rawImg
            $('#modalAvt').show(function () {// sao khi cmnModal show len thi se chay ham nay
                croppi = $('#upload-demo-avt').croppie({ // croppie se duoc chen vao tag nay "#upload-demo"
                    viewport: { //set chieu rong chieu cao
                        type: 'circle',
                        width: widthAvt,
                        height: heightAvt
                    },
                    boundary: { //set khoan bu cua hinh anh duoc crop
                        width: widthAvt + 200,
                        height: heightAvt + 50
                    }
                });
                /*binding hinh anh thêm setZoom vao modal*/
                croppi.croppie('bind', {
                    url: rawImg,
                }).then(function () {
                    croppi.croppie('setZoom', 0);
                });
            })
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        swal("Sorry - you're browser doesn't support the FileReader API");
    }
}

/*bat su kien nhap file*/
$('#imgBackground').on('change', function () {
    readFile(this);
});

/*bam nut save*/
$(document).on('click', '#cropImageAvtBtn', function (ev) {
    croppi.croppie('result', {
        type: 'base64',
        size: 'viewport',
        format: 'jpg'
    }).then(function (resp) {
        img1 = `${resp}`;
        $('#topLeft--avt').css('background-image', `url(${resp})`); //image sau khi cat duoc tra ve
        $('.cmnModal').hide(function () {// khi cmnModal hide thi destroy croppie
            croppi.croppie('destroy');
        });
        avatar.url = resp; //set url of img to obj avt
        $('#radioAvt__topLeft').click();
    });
});


/*Avatar*/
$('#imgAvatar').on('change', function () {
    readFileAvt(this);
    avatar.avatarToggle = true; //when file avatar has changed
});
/*bam nut save*/
$(document).on('click', '#cropImageBtn', function (ev) {
    croppi.croppie('result', {
        type: 'base64',
        size: 'viewport',
        format: 'jpg'
    }).then(function (resp) {
        img2 = `${resp}`;
        $('.frontCard__container').css('background-image', `url(${resp})`); //image sau khi cat duoc tra ve
        $('.cmnModal').hide(function () {// khi cmnModal hide thi destroy croppie
            croppi.croppie('destroy');
        });
        $('.frontCard__container').css('background-color', "#ffffff");
        background.url = resp;
    });
});



async function reply_click() {

    if (document.getElementById('radioAvt__center').checked) {
        return await mergeImage(width / 2 - (widthAvt / 2), height / 2 - (heightAvt / 2));
    } else if (document.getElementById('radioAvt__topRight').checked) {
        return await mergeImage(width - (widthAvt) - 15, 10);
    } else return await mergeImage(10, 10);
}

async function getValue(value) {
    cardData.front = await value.front
    cardData.back = await value.back
    await $("#frontcard").val(cardData.front);
    console.log("set up front card finish")
    await $("#backcard").val(cardData.back);
    console.log("set up back card finish")
    sendForm()
}

function sendForm() {
    var form = $("#formdata");
    $(form).submit();
}

// function UpdateDataToSend() {
//    doMerge().then((value) => getValue(value));
//    console.log(cardData.front)
//    console.log(cardData.back)
//    return true;
//}

/*canvas color background*/
function canvasWhiteInit() {
    var canvas = document.getElementById("canvasWhite");
    var ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvasWhite.width, canvasWhite.height); //clear text of canvas
    ctx.save();
    ctx.beginPath();
    ctx.fillStyle = colorBackground;
    ctx.rect(0, 0, canvasWhite.width, canvasWhite.height);
    ctx.fill();
    ctx.fill();
    ctx.restore();
}

/*Function create iamge Back Card*/
async function createImageBackDcard() {
    //document.getElementById("the-link").focus();
    var backDcard = null;
    await html2canvas(document.getElementById('bc'), {
        profile: true,
        useCORS: true
    }).then(function (canvas) {
        var data = canvas.toDataURL('image/png');
        var src = encodeURI(data);
        backDcard = src;
    });
    return backDcard;
}

async function mergeImage(avtX, avtY) {
    //var backDcard = createImageBackDcard();

    //color bg, white bg when have image bg
    var bgImage = $('.frontCard__container').css("background-image")
    if (bgImage == 'none') {
        img2 = "";
        $('#imgBackground').val("");
        colorBackground = $('.frontCard__container').css("backgroundColor");
    } else {
        colorBackground = "white";
    }
    var frontDcard = null;
    canvasWhiteInit();  //create canvas corlor image
    imgColorBG = canvasWhite.toDataURL('image/png');// color image, deafaut "white"
    img3 = canvas.toDataURL('image/png') //content image

    /*Merge Image*/
    if (img1 != "" && img2 != "" && content.text != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img2, x: 0, y: 0 },
            { src: img1, x: avtX, y: avtY },
            { src: img3, x: 0, y: 0 }


        ]).then(b64 => frontDcard = b64);
    }
    else if (img1 != "" && img2 != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img2, x: 0, y: 0 },
            { src: img1, x: avtX, y: avtY }

        ]).then(b64 => frontDcard = b64);
    }
    else if (img1 != "" && content.text != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img1, x: avtX, y: avtY },
            { src: img3, x: 0, y: 0 }
        ]).then(b64 => frontDcard = b64);
    }
    else if (img2 != "" && content.text != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img2, x: 0, y: 0 },
            { src: img3, x: 0, y: 0 }
        ]).then(b64 => frontDcard = b64);
    }
    else if (img1 != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img1, x: avtX, y: avtY },
            { src: img3, x: 0, y: 0 }
        ]).then(b64 => frontDcard = b64);
    }
    else if (img2 != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img2, x: 0, y: 0 }
        ]).then(b64 => frontDcard = b64);
    }
    else if (content.text != "") {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 },
            { src: img3, x: 0, y: 0 }
        ]).then(b64 => frontDcard = b64);
    } else if (imgColorBG != null) {
        await mergeImages([
            { src: imgColorBG, x: 0, y: 0 }
        ]).then(b64 => frontDcard = b64);
    }
    return frontDcard;
}

$(document).ready(function () {
    $('input:radio[name="radioAvt"]').click(function (e) {
        if (avatar.url === '') {
            showError("Firstly, upload file please !!!");
            e.preventDefault();
            return;
        }
    })

    $('input:radio[name="radioAvt"]').change(
        function () {
            if ($(this).is(':checked')) {
                $('.frontCard__container--avt').css('background-image', 'none');
                switch ($(this).attr('id')) {
                    case "radioAvt__topLeft":
                        avatar.setAvatar($('#topLeft--avt'))
                        canvasInit();
                        break;
                    case "radioAvt__topRight":
                        avatar.setAvatar($('#topRight--avt'))
                        canvasInit();
                        break;
                    case "radioAvt__center":
                        avatar.setAvatar($('#center--avt'))
                        canvasInit();
                        break;
                }
                if (content.text != "")
                    canvasInit();
            }
        });

    $(".btnContent").on('click', function () {
       //if not then...
        content.text = $("#input--content__text").val().trim().replace(/\s\s+/g, ' ');
        if (content.text == "") {
            showError("Please, type something ...");
            $('#input--content__text').focus();
            return;
        }
        if (content.text.length > 17) {
            showError("Please, just type content less than 17 characters");
            $('#input--content__text').val("");
            $('#input--content__text').focus();
            return;
        }
        if (content.text != "") { //check content has aldready exsited or not
            $("#radioContent__topLeft").click();
            clearCanvas();
            content.text = $("#input--content__text").val().trim().replace(/\s\s+/g, ' ');
            canvasInit();
            return;
        }
        //$("#radioContent__topLeft").click();
    });

    $('input:radio[name="radioContent"]').click(function (e) {
        if (content.text == "") {
            showError("Please, type something before choose position");
            e.preventDefault();
            return;
        }
    })
    $('input:radio[name="radioContent"]').change(function () {
        if ($(this).is(":checked")) {
            switch ($(this).attr('id')) {
                case "radioContent__topRight":
                    content.position = "topRight"
                    break;
                case "radioContent__topLeft":
                    content.position = "topLeft"
                    break;
                case "radioContent__center":
                    content.position = "center"
                    break;
            }
            canvasInit();
        }
    })

    /*Canvas image content*/
    function canvasInit() {
        var canvas = document.getElementById("canvas");
        var ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height); //clear text of canvas
        ctx.save();
        ctx.beginPath();
        ctx.fillStyle = "rgba(255, 255, 255, 0)";
        ctx.rect(0, 0, canvas.width, canvas.height);
        ctx.fill();
        ctx.font = "16px sans-serif";
        ctx.globalCompositeOperation = "xor";
        ctx.beginPath();
        ctx.fillStyle = "black";
        var direction = content.getDirection(); // get x,y of text
        ctx.fillText(content.text, direction.x, direction.y);
        ctx.fill();
        ctx.restore();
    }



    $("#toggleBg").on("click", function () {
        $('.frontCard__container').css('background-image', 'none'); //remove div
        $("#imgBackground").val("");
        $('#input--color').val("#ffffff");
        colorBackground = $('.frontCard__container').css("backgroundColor", "#fff");
        img2 = "";
    });

    $('#toggleAvt').on("click", function () {
        content.text = "";
        clearCanvas();
        $('.frontCard__container--avt').css('background-image', 'none'); //remove div
        $("#imgAvatar").val("");
        avatar.url = "";
        img1 = "";
        $('input[name=radioAvt]').prop('checked', false);
        canvasInit(); //set position of content when remove avatar
    })

    $('#toggleContent').on("click", function () {
        clearCanvas();
        img3 = "";
        $("#input--content__text").val("");
        $('input[name=radioContent]').prop('checked', false);
    })
    $('#input--color').change(function () {
        if (background.url != "") {
            background.url = "";
            $('.frontCard__container').css('background-image', "none");
            $('.frontCard__container').css('background-color', $(this).val());
            $('#imgBackground').val("");
        } else {
            $('.frontCard__container').css('background-color', $(this).val());
        }
    });


    function clearInputFile(f) {
        if (f.value) {
            try {
                f.value = ''; //for IE11, latest Chrome/Firefox/Opera...
            } catch (err) {
            }
            if (f.value) { //for IE5 ~ IE10
                var form = document.createElement('form'), ref = f.nextSibling;
                form.appendChild(f);
                form.reset();
                ref.parentNode.insertBefore(f, ref);
            }
        }
    }

    function clearCanvas() {
        var canvas = document.getElementById("canvas");
        var ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height); //clear text of canvas
    }

    function showError(content) {
        $("#overlayPopup").show();
        $('#error__popup--content').text(content);
    }
})
