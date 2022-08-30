﻿function Action() {
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
        $('span').attr("hidden", true);
        $.get(`/comments/comment/${id}?message=${message}`, (status) => {
            $('textarea').val('');
            $.get(`/api/comments/${id}`, (comment) => {
                $.get(`/comments/commentscount/${id}`, (count) => {

                    debugger;
                    var divParent = CreateComment(comment);
                    commentsConnection.invoke("CreateComment", divParent, count)
                })

            });
        });
    }
    else {
        $('span').removeAttr("hidden");
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
        $('span').removeAttr("hidden");
    }
}

function CreateComment(comment) {

    var divParent = document.createElement("div");
    divParent.classList.add("d-flex", "flex-start", "mb-4");

    var img = document.createElement("img");

    img.classList.add("rounded-circle", "shadow-1-strong", "me-3")
    img.setAttribute("src", Object.values(comment)[3]);
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

function CreateDivCommentSection() {
    var divCommentSection = document.createElement('div');
    divCommentSection.classList.add("comments-section");

    return divCommentSection;
}