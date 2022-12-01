function Delete(commentId) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this comment!",
        icon: "warning",
        buttons: ["Cancel", "Yes, delete it!"],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.get(`/comments/delete/${commentId}`, () => {
                    $.get(`/comments/commentscount/${id}`, (count) => {
                        commentsConnection.invoke("DeleteComment", commentId, count);
                    });

                });
                swal("The comment was deleted!", {
                    icon: "success",
                });
            } else {
                swal("The comment will not be deleted!");
            }
        });
}


function CreateNoElementsSection() {
    let divNoComments = document.createElement("div");
    divNoComments.setAttribute("id", "no-comments");

    let pFirstElement = document.createElement("p");
    pFirstElement.classList.add("text-center", "text-light");
    pFirstElement.textContent = "There is no comments for this post";

    let pSecondElement = document.createElement("p");
    pSecondElement.classList.add("text-center", "text-light");
    pSecondElement.textContent = "Be the first who comment";

    divNoComments.appendChild(pFirstElement);
    divNoComments.appendChild(pSecondElement);

    return divNoComments;
}