"use strict";

let onlineCount = document.querySelector('.statistical__item-info-number');
let updateCountCallback = function (message) {
    if (!message) return;
    console.log('updateCount = ' + message);
    if (onlineCount) onlineCount.innerText = message;
    else {
        console.log('can find span');

    }
};

function onConnectionError(error) {
    if (error && error.message) console.error(error.message);
}


var connection = new signalR.HubConnectionBuilder().withUrl("/hubAdmin").build();

connection.on('updateCount', updateCountCallback);
connection.on("HandleReport", function (id, name, email) {
    var str = "<tr><td id='IdUser'>" + id + "</td><td>" + name + "</td><td class='block__table-item-link'>" + email + "</td> <td> <span class='block__table-item-btn unblock'> UnBlock<i class='fa-solid fa-lock-open'></i></span></td> </tr>";
    $("#table_ban #tbody_ban").append(str);
    console.log(str);

});
connection.onclose(onConnectionError);

connection.start()
    .then(function () {
        console.log('OnlineCount Connected');
    })
    .catch(function (error) {
        console.error(error.message);
    });


let countConnection = new signalR.HubConnectionBuilder().withUrl('/hubAdmin').build();
