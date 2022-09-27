function ShowPhoto(i) {
    if (i === 'more') {
        i = 0;
    }

    if (i == 0) {
        prevControl.classList.add("display-none");
    }
    if (i == photosCount - 1) {
        nextControl.classList.add("display-none");
    }

    element.classList.add("d-flex");
    let activeElement = document.querySelector(`[position="${i}"]`);
    activeElement.classList.add("active");
}