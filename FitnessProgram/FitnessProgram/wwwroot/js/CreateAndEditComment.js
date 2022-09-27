function Action() {
    if ($('.comment-edit-btns').text() == "Edit") {
        Edit();
    }
    else {
        Comment();
    }

}

function Comment() {
    var message = $('textarea').val();
    if (message.trim().length >= 2) {
        $('#validator').attr("hidden", true);
        $.get(`/comments/comment/${id}?message=${message}`, (status) => {
            $('textarea').val('');
            $.get(`/api/comments/${id}`, (comment) => {
                $.get(`/comments/commentscount/${id}`, (count) => {

                    var commentForMe = CreateCommentForMe(comment);
                    if (count == 1) {
                        $('#no-comments').remove();
                        $('.card-footer').prepend(CreateDivCommentSection());
                    }

                    $('.comments-section').prepend(commentForMe);
                    $('#comments-count').text(count);

                    var commentForOthers = CreateCommentForOthers(comment);
                    commentsConnection.invoke("CreateComment", commentForOthers, count)
                })

            });
        });
    }
    else {
        $('#validator').removeAttr("hidden");
    }
}

function Edit() {
    var commentId = document.getElementsByClassName("comment-edit-btns")[0].getAttribute("comment-id");

    var message = $("#message").val();

    if (message.trim().length >= 2) {
        $.get(`/comments/edit/${commentId}?message=${message}`, (status) => {
            commentsConnection.invoke("EditComment", message, commentId);
        })
    }
    else {
        $('#validator').removeAttr("hidden");
    }
}

function CreateCommentForMe(comment) {
    var divParent = document.createElement("div");
    divParent.classList.add("d-flex", "flex-start", "mb-4");

    var img = document.createElement("img");
    img.classList.add("rounded-circle", "shadow-1-strong", "me-3")
    img.setAttribute("src", `data:image/gif;base64,${Object.values(comment)[3] != null ? Object.values(comment)[3] :"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"}`);
    img.setAttribute("alt", "avatar");
    img.setAttribute("width", "65");
    img.setAttribute("height", "65");


    var divClassW100 = document.createElement('div');
    divClassW100.classList.add("card", "w-100")

    var divClassCardBoyd = document.createElement("div");
    divClassCardBoyd.classList.add("card-body", "p-4");

    var div = document.createElement('div');

    var h5Username = document.createElement("h5");
    h5Username.textContent = Object.values(comment)[4];

    var pClassSmall = document.createElement('p');
    pClassSmall.classList.add("small");
    pClassSmall.textContent = "Posted on: " + Object.values(comment)[2];

    var id = Object.values(comment)[0];

    var pMessage = document.createElement("p");
    pMessage.setAttribute("comment-message-id", id);
    pMessage.textContent = Object.values(comment)[1];

    var divFloat = document.createElement("div");
    divFloat.classList.add("float-end");

    var btnEdit = document.createElement("button");
    btnEdit.classList.add("fa", "fa-pencil", "btns");
    btnEdit.setAttribute("style", "font-size:24px");
    btnEdit.setAttribute("onclick", `OnEdit(${id})`);

    var btnDelete = document.createElement("button");
    btnDelete.classList.add("fa", "fa-close", "btns");
    btnDelete.setAttribute("style", "font-size:24px;color:red");
    btnDelete.setAttribute("data-id", id);
    btnDelete.setAttribute("onclick", `Delete(${id})`);

    div.appendChild(h5Username);
    div.appendChild(pClassSmall);
    div.appendChild(pMessage);


    divFloat.appendChild(btnEdit);
    divFloat.appendChild(btnDelete);

    divClassCardBoyd.appendChild(divFloat);
    divClassCardBoyd.appendChild(div);

    divClassW100.appendChild(divClassCardBoyd);

    divParent.appendChild(img);
    divParent.appendChild(divClassW100);

    return divParent.outerHTML;
}

function CreateCommentForOthers(comment) {

    var divParent = document.createElement("div");
    divParent.classList.add("d-flex", "flex-start", "mb-4");

    var img = document.createElement("img");

    img.classList.add("rounded-circle", "shadow-1-strong", "me-3")
    img.setAttribute("src", `data:image/gif;base64,${Object.values(comment)[3] != null ? Object.values(comment)[3] : "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"}`);
    img.setAttribute("alt", "avatar");
    img.setAttribute("width", "65");
    img.setAttribute("height", "65");


    var divClassW100 = document.createElement('div');
    divClassW100.classList.add("card", "w-100")

    var divClassCardBoyd = document.createElement("div");
    divClassCardBoyd.classList.add("card-body", "p-4");

    var div = document.createElement('div');

    var h5Username = document.createElement("h5");
    h5Username.textContent = Object.values(comment)[4];

    var pClassSmall = document.createElement('p');
    pClassSmall.classList.add("small");
    pClassSmall.textContent = "Posted on: " + Object.values(comment)[2];

    var id = Object.values(comment)[0];

    var pMessage = document.createElement("p");
    pMessage.setAttribute("comment-message-id", id);
    pMessage.textContent = Object.values(comment)[1];

    var divFloat = document.createElement("div");
    divFloat.classList.add("float-end");

    div.appendChild(h5Username);
    div.appendChild(pClassSmall);
    div.appendChild(pMessage);

    divClassCardBoyd.appendChild(divFloat);
    divClassCardBoyd.appendChild(div);

    divClassW100.appendChild(divClassCardBoyd);

    divParent.appendChild(img);
    divParent.appendChild(divClassW100);

    return divParent.outerHTML;
}

function CreateDivCommentSection() {
    var divCommentSection = document.createElement('div');
    divCommentSection.classList.add("comments-section");

    return divCommentSection;
}