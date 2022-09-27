function Close() {
    element.classList.remove("d-flex");

    let activeElement = document.querySelector(".active");
    activeElement.classList.remove("active");
}