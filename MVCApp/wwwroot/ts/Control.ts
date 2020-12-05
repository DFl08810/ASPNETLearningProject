//This is utilized by bootstrap dropdowns, it changes the button text by selection
$(function () {

    $(".dropdown-menu a").click(function (e) {

        var buttonId = e.target.parentElement.parentElement.getAttribute('id');
        console.log(buttonId);

        $(`#${buttonId} .btn`).text($(this).text());
        //
    });

});

//selection matrix class
class SelectionOrigin {
    static roleSelection: string = "All";
    static statusSelection: string = "All";
}

//this sorts table content based on  role selection
function SortRoles(role) {
    //iterate rows
    $("tr.accounts").each(function (index, tr) {
        //get val attribute from role column
        var roleValue = $("td.role", tr).attr('val');
        //get status for checking
        var statusValue = $("td.status i.table-symbol", tr).attr('val');
        //var y = $("td.status i.table-symbol", tr).attr('val');
        console.log(roleValue);
        //console.log(y);
        //show or hide rows based on selection
        if (role == roleValue && statusValue == SelectionOrigin.statusSelection) {
            $(tr).show();
        }
        else if (role != roleValue && role != 'All') {
            $(tr).hide();
        }
        else if (statusValue == SelectionOrigin.statusSelection || SelectionOrigin.statusSelection == 'All') {
            $(tr).show();
        }
        console.log(index);
        console.log(tr);
    });
}

//this sorts table content based on  role selection
function SortStatus(status) {
    //iterate rows
    $("tr.accounts").each(function (index, tr) {
        //get status value from status column
        var statusValue = $("td.status i.table-symbol", tr).attr('val');
        //get role for checking
        var roleValue = $("td.role", tr).attr('val');

        //console.log(y);
        //show or hide rows based on selection
        console.log('deb');
        console.log(SelectionOrigin.roleSelection);
        if (status == statusValue && roleValue == SelectionOrigin.roleSelection) {
            $(tr).show();
        }
        else if (status != statusValue && status != 'All') {
            $(tr).hide();
        }
        else if (roleValue == SelectionOrigin.roleSelection || SelectionOrigin.roleSelection == 'All') {
            $(tr).show();
        }

        console.log(index);
        console.log(tr);
    });
}


//gets selected value from role dropdown and resolve display rows
$(function () {
    $(".role-dropdown a").click(function (e) {
        var role = $(this).attr('val');
        SelectionOrigin.roleSelection = role;
        SortRoles(role);
    });

});
//gets selected value from status dropdown and resolve display rows
$(function () {
    $(".status-dropdown a").click(function (e) {
        var status = $(this).attr('val');
        SelectionOrigin.statusSelection = status;
        SortStatus(status);
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
            SortStatus(SelectionOrigin.statusSelection);
            SortRoles(SelectionOrigin.roleSelection);
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
            SortStatus(SelectionOrigin.statusSelection);
            SortRoles(SelectionOrigin.roleSelection);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('p').append('Error' + errorMessage);
        }
    });
});



//delete 
$(function () {
    $(".delete-item").click(function (e) {
        var idSelection = $(this).attr('val');
        var id: number = +idSelection;



        console.log(idSelection);
        $.ajax('/Admin/Delete', {
            type: 'GET',  // http method
            //define content type for expected data
            contentType: 'application/x-www-form-urlencoded',
            data: { Id: id },  // data to submit
            success: function (data, status, xhr) {
                console.log(data, status, xhr);
                let alertBox: string = `<div class="alert alert-success alert-dismissible fade show" role="alert">
                                          <strong>Success:</strong> ${xhr.responseText}!.
                                          <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                          </button>
                                        </div>`;
            },
            error: function (jqXhr, textStatus, errorMessage) {
                jqXhr.responseText;
                $(".table-card .alert").remove();
                let alertBox: string = `<div class="alert alert-danger alert-dismissible fade show" role="alert">
                                          <strong>Error:</strong> ${jqXhr.responseText}!.
                                          <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                          </button>
                                        </div>`;

                $('.table-card').prepend(alertBox);
            }
        });
    });

});
