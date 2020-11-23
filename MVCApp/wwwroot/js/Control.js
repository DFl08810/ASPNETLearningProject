//This is utilized by bootstrap dropdowns, it changes the button text by selection
$(function () {
    $(".dropdown-menu a").click(function (e) {
        var buttonId = e.target.parentElement.parentElement.getAttribute('id');
        console.log(buttonId);
        $(`#${buttonId} .btn`).text($(this).text());
    });
});
//# sourceMappingURL=Control.js.map