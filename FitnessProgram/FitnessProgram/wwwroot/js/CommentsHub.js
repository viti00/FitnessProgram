"use strict";

var commentsConnection = new signalR.HubConnectionBuilder().withUrl("/commentsHub").build();


commentsConnection.on("Comment", function (divParent, count) {
    if (count == 1) {
        $('#no-comments').remove();
        $('.card-footer').prepend(CreateDivCommentSection());
    }

    $('.comments-section').prepend(divParent);
    $('#comments-count').text(count);
});

commentsConnection.on("Edit", function (message, commentId) {
    document.querySelector(`[comment-message-id="${commentId}"]`).textContent = message;
    $('#validator').attr("hidden", true);
    Cancel();
});

commentsConnection.on("Delete", function (commentId, count) {

    $(`[data-id="${commentId}"]`).parent().parent().parent().parent().remove();
    $('#comments-count').text(count);

    if (count < 1) {
        $('.comments-section').remove();
        $('.card-footer').prepend(CreateNoElementsSection());
    }
});

commentsConnection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

