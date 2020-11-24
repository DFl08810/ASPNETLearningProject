//This is utilized by bootstrap dropdowns, it changes the button text by selection
$(function () {
    $(".dropdown-menu a").click(function (e) {
        var buttonId = e.target.parentElement.parentElement.getAttribute('id');
        console.log(buttonId);
        $(`#${buttonId} .btn`).text($(this).text());
    });
});
//Searchbar script
$("#SearchButton").click(function (event) {
    //get id of parent container
    var buttonId = event.target.parentElement.parentElement.getAttribute('id');
    //get value of input field
    var query = $(`#${buttonId} input`).val();
    console.log(query);
    //JQuey AJAX call to SearchUser(string searchQuery) method in admin controller
    $.ajax('/Admin/SearchUser', {
        type: 'GET',
        //define content type for expected data
        contentType: 'application/x-www-form-urlencoded',
        data: { searchQuery: query },
        success: function (data, status, xhr) {
            $('.table-card').empty();
            $('.table-card').append(data);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });
});
//# sourceMappingURL=Control.js.map