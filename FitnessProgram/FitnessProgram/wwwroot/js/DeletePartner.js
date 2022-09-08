function DeletePartner(id) {
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
                    .then((okay) => {
                        $.get(`/admin/partners/delete/${id}`, (redirect) => {
                            debugger;
                            window.location.replace("https://localhost:7238/Partners/All");
                        });
                    });


            } else {
                swal("The post will not be deleted!");
            }
        });
}