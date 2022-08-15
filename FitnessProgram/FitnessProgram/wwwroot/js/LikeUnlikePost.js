function OnLike() {

    if ($('#like-btn').text() == "Like") {

        Like();
    }
    else {
        UnLike();
    }
}

function Like() {
    $.get(`/likes/likepost/${id}`, (status) => {
        $.get(`/api/likes/${id}`, (likes) => {
            $('#likes-count').text(likes);
            $('#like-btn').text('Liked');
        })
    });
}

function UnLike() {
    $.get(`/likes/unlikepost/${id}`, (status) => {
        $.get(`/api/likes/${id}`, (likes) => {
            $('#likes-count').text(likes);
            $('#like-btn').text('Like');
        })
    });
}