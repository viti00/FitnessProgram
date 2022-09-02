var bestResultId = document.querySelector("#delete-bestresult").getAttribute("data-id");

function DeleteBestResult() {
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
                        $.get(`/admin/bestresults/delete/${bestResultId}`, (redirect) => {
                            debugger;
                            window.location.replace("https://localhost:7238/BestResults/All");
                        });
                    });


            } else {
                swal("The post will not be deleted!");
            }
        });
}