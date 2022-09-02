"use strict";

var likesConnection = new signalR.HubConnectionBuilder().withUrl("/likesHub").build();


likesConnection.on("LikePost", function (likes) {
    $('#likes-count').text(likes);
})

likesConnection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});