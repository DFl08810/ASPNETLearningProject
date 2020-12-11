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
}
SelectionOrigin.roleSelection = "All";
SelectionOrigin.statusSelection = "All";
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
//sync
$(function () {
    $("#SyncButton").click(function (e) {
        window.location.href = "/Admin/Accounts/Synchronize";
    });
});
//Action
function ValidateAcccount(accountId, action) {
    var id = +accountId;
    console.log(id);
    console.log(action);
    var dataObject = { Id: id };
    CallValidationTarget(dataObject, action);
}
//semi abstracted ajax call for admin controller
function CallValidationTarget(dataObject, target) {
    $.ajax(`/Admin/Accounts/${target}`, {
        type: 'GET',
        //define content type for expected data
        contentType: 'application/x-www-form-urlencoded',
        data: dataObject,
        success: function (data, status, xhr) {
            //remove content of card
            $('#AccountValidationComponent').empty();
            //append partial view
            $('#AccountValidationComponent').append(data);
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log("failure");
        }
    });
}
//gets selected value from sort dropdown and calls semi abstracted ajax func 
$(function () {
    $(".sort-dropdown a").click(function (e) {
        var test = $(this).attr('val');
        var dataObj = { sortMode: test };
        var target = "SortBy";
        CallTarget(dataObj, target);
    });
});
//semi abstracted ajax call for admin controller
function CallTarget(dataObject, target) {
    $.ajax(`/Admin/Accounts/${target}`, {
        type: 'GET',
        //define content type for expected data
        contentType: 'application/x-www-form-urlencoded',
        data: dataObject,
        success: function (data, status, xhr) {
            //remove content of card
            $('#AccountEditForm').empty();
            //append partial view
            $('#AccountEditForm').append(data);
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
    $.ajax('/Admin/Accounts/SearchUser', {
        type: 'GET',
        //define content type for expected data
        contentType: 'application/x-www-form-urlencoded',
        data: { searchQuery: query },
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
        var id = +idSelection;
        console.log(idSelection);
        $.ajax('/Admin/Accounts/Delete', {
            type: 'GET',
            //define content type for expected data
            contentType: 'application/x-www-form-urlencoded',
            data: { Id: id },
            success: function (data, status, xhr) {
                console.log(data, status, xhr);
                let alertBox = `<div class="alert alert-success alert-dismissible fade show" role="alert">
                                          <strong>Success:</strong> ${xhr.responseText}!.
                                          <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                          </button>
                                        </div>`;
            },
            error: function (jqXhr, textStatus, errorMessage) {
                jqXhr.responseText;
                $(".table-card .alert").remove();
                let alertBox = `<div class="alert alert-danger alert-dismissible fade show" role="alert">
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
//# sourceMappingURL=AdminAccountsControl.js.map