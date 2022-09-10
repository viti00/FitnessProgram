function Approve(id) {
    $.get(`approve/${id}`, (status) => {
        DeleteRow(id);
    })
}

function Reject(id) {
    $.get(`reject/${id}`, (status) => {
        DeleteRow(id);
    })
}

function DeleteRow(id) {
    var element = document.querySelector(`[data-id="${id}"]`);
    element.remove();
}