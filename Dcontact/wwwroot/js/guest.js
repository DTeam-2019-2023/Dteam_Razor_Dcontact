

$(document).ready(function () {
    $('.card__content--rowItem').on('click', function () {
        var rowid = $(this).attr('idRowContent');
        $.ajax({
            origin: '*',
            type: "get",
            url: "Guest?handler=GetLink",
            //headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                idRowContent: rowid
            },
            success: function (msg) {
                console.log(msg);
                window.open("http://" + msg.url, '_blank')  //open url in another tab
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (e) {
                console.log("Request failed: " + JSON.stringify(e));
            }
        });
    });

    $("#backHome").on("click", function () {
        window.location = '/Dcontact/User/EditDcontact';
    })
})