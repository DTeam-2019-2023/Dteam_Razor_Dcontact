const textarea = document.querySelectorAll("textarea");
textarea.forEach((element) => {
    element.addEventListener("keydown", (e) => {
        element.style.height = "40px";
        let scHeight = e.target.scrollHeight;
        element.style.height = `${scHeight}px`;
    });
});

function resetArea(element) {
    element.style.height = "40px";
}
function autoheight() {
    $(".line-disc").each(function () {
        $(this).height(this.scrollHeight);
    });
}
$(document).ready(function () {
    autoheight();
});