function DeletePost() {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this post!",
        icon: "warning",
        buttons: ["Cancel", "Yes, delete it!"],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {

                swal("The post was deleted!", {
                    icon: "success",
                })
                    .then(() => {
                        $.get(`/posts/delete/${id}`, () => {
                            window.location.replace("/Posts/All");
                        });
                    });


            } else {
                swal("The post will not be deleted!");
            }
        });
}