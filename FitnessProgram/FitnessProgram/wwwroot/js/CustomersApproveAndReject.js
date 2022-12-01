function Approve(id) {
    $.get(`approve/${id}`, () => {
        DeleteRow(id);
    })
}

function Reject(id) {
    $.get(`reject/${id}`, () => {
        DeleteRow(id);
    })
}

function DeleteRow(id) {
    var element = document.querySelector(`[data-id="${id}"]`);
    element.remove();
}