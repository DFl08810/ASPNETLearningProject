//This is utilized by bootstrap dropdowns, it changes the button text by selection
$(function () {

    $(".dropdown-menu a").click(function (e) {

        var buttonId = e.target.parentElement.parentElement.getAttribute('id');
        console.log(buttonId);

        $(`#${buttonId} .btn`).text($(this).text());
        //
    });

});
//gets selected value from sort dropdown and calls semi abstracted ajax func 
$(function () {
    $(".sort-dropdown a").click(function (e) {
        var test = $(this).attr('val');
        var dataObj = { sortMode: test }
        var target = "SortBy";
        CallTarget(dataObj, target);
    });

});
//semi abstracted ajax call for admin controller
function CallTarget(dataObject, target: string) {
    $.ajax(`/Admin/${target}`, {
        type: 'GET',  // http method
        //define content type for expected data
        contentType: 'application/x-www-form-urlencoded',
        data: dataObject,  // data to submit
        success: function (data, status, xhr) {
            //remove content of card
            $('.table-card').empty();
            //append partial view
            $('.table-card').append(data);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });
}


//Searchbar script
$("#SearchButton").click(function (event) {
    //get id of parent container
    var buttonId = event.target.parentElement.parentElement.getAttribute('id');
    //get value of input field
    var query = $(`#${buttonId} input`).val();
    console.log(query);
    //JQuey AJAX call to SearchUser(string searchQuery) method in admin controller
    $.ajax('/Admin/SearchUser', {
        type: 'GET',  // http method
        //define content type for expected data
        contentType: 'application/x-www-form-urlencoded',
        data: { searchQuery: query },  // data to submit
        success: function (data, status, xhr) {
            $('.table-card').empty();
            $('.table-card').append(data);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });
});
