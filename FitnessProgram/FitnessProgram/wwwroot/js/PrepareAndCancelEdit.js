function OnEdit(commentId) {

    var editAndDeleteBtns = document.querySelectorAll("button.btns");
    editAndDeleteBtns.forEach(x => {
            x.setAttribute("disabled", "disabled");
        });

    $('.comment-edit-btns').attr("comment-id", commentId);
    $('.comment-edit-btns').text("Edit");

    var divInputGr = document.createElement('div');
    divInputGr.id = "cancel";
    divInputGr.classList.add("input-group-append");

    var btnCancel = document.createElement("button");
    btnCancel.addEventListener("click", Cancel);
    btnCancel.classList.add("btn", "btn-outline-secondary", "cancel-btn");
    btnCancel.textContent = "X";

    divInputGr.appendChild(btnCancel)

    $("#comment").append(divInputGr);

    $.get(`/comments/getmessage/${commentId}`, (message) => {
        $("#message").val(message);
    })
}

function Cancel() {
    $('#cancel').remove();
    $('.comment-edit-btns').removeAttr("comment-id");
    $('.comment-edit-btns').text("Comment");
    $("#message").val('');
    var editAndDeleteBtns = document.querySelectorAll("button.btns");
    editAndDeleteBtns.forEach(btn => {
            btn.removeAttribute("disabled");
        });
}
