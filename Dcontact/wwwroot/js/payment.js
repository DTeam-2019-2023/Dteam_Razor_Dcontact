$(document).ready(function () {
    function showError(mess) {
        $("#paymentPopup").show();
        $(".errorContent").text(mess);
    }

    $(".dteam").load("dteam.html");
    $(".header").load("headerlogin.html");

    $(".btnPayment").click(function (e) {
        var check = true;
        var regexaddress = new RegExp('^[#.0-9a-zA-Z\\s,-]+$');
        var regexphone = new RegExp('^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$');
        var regexnumbercard = new RegExp('^4[0-9]{12}(?:[0-9]{3})?$');
        var regexexpirationdate = new RegExp('^(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)?[0-9]{2}$');
        var regexCVC = new RegExp('^[0-9]{3}$');
        var regexQuantity = new RegExp('^[1-9][0-9]*$');

        var quantity = $('#detailInfo__input--quantity').val();
        var address = $('#userInfo__input--address').val();
        var phone = $('#userInfo__input--numberPhone').val();
        var cost = 30000;
        var cvc = $('#detailInfo__input--cvc').val();
        var numbercard = $('#numberCard__input').val();
        var expiration = $('#detailInfo__input--expiration').val();

        //if ((!regexphone.test(phone))
        //    || (!regexaddress.test(address))
        //    || (!regexnumbercard.test(numbercard))
        //    || (!regexexpirationdate.test(expiration))
        //    || (!regexCVC.test(cvc)) || (!regexQuantity.test(quantity))) {
        //    check = false;
        //    $('.iconValidation').show();
        //} 

        if (!regexphone.test(phone)) {
            check = false;
            $('#iconValidation-phone').show();
        } else {
            $('#iconValidation-phone').hide();
        }
        if (!regexaddress.test(address)) {
            check = false;
            $('#iconValidation-address').show();
        } else {
            $('#iconValidation-address').hide();
        }
        if (!regexnumbercard.test(numbercard)) {
            check = false;
            $('#iconValidation-numbercard').show();
        } else {
            $('#iconValidation-numbercard').hide();
        }
        if (!regexexpirationdate.test(expiration)) {
            check = false;
            $('#iconValidation-expiration').show();
        } else {
            $('#iconValidation-expiration').hide();
        }
        if (!regexCVC.test(cvc)) {
            check = false;
            $('#iconValidation-cvc').show();
        } else {
            $('#iconValidation-cvc').hide();
        }
        if (!regexQuantity.test(quantity)) {
            check = false;
            $('#iconValidation-quantity').show();
        } else {
            $('#iconValidation-quantity').hide();
        }

        if (check) {
            $('#cost').text(cost);
            $('#quantity').text(quantity);
            $('#total').text(cost * quantity);
            $('#address').text(address);
            $('#phone').text(phone);
            $(".overlay__popupPayment").show();
            e.preventDefault();
        }
        return false;
    });

    $(".overlay__popupPayment").click(function () {
        $(".overlay__popupPayment").fadeOut();
    });

    $("#popupPayment__btn--cancel").click(function (e) {
        $(".overlay__popupPayment").fadeOut();
        e.preventDefault();
    });

    $("#paymentPopup").click(function () {
        $("#paymentPopup").fadeOut();
    });

    function getData() {
       

    }

});