$(function () {

    $(".dropdown-menu a").click(function () {

        $(".dropdown-toggle:first-child").text($(this).text());
        $(".dropdown-toggle:first-child").val($(this).text());

    });

});