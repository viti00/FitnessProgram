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
                $.get(`/comments/delete/${commentId}`, (status) => {

                    var par = $(`[data-id="${commentId}"]`).parent().parent().parent().parent();

                    par.remove();

                    $.get(`/comments/commentscount/${id}`, (count) => {
                        $('#comments-count').text(count);
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