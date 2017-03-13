var tblComponent;
var fntblComponent = $("#tbl-UserAccounts thead")

$(document).ready(function () {
    loadComponents("/DTSecurityManager/FetchUserAccounts");
    $('.applyDatePicker').datepicker();    
});

function loadComponents(url) {
    var req = $.ajax({
        type: 'GET',
        async: true,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: {},
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {
        var dataColumns = [];
        
        var thead = "<tr>"
        var colCount = 0;
        dataColumns[colCount] =
            {
                "data": null, "targets": -1, "sortable": false,
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-success btn-minier btn-xs' title='Update User' onClick='EditUser(\"" + data.ID + "\")'><i class='fa fa-edit' aria-hidden='true'></i></a>" +
                        "&nbsp;<a class='btn btn-danger btn-minier btn-xs' title='User Role' onClick='userRoleUserAccount(\"" + data.ID + "\")'><i class='fa fa-user' aria-hidden='true'></i></a>" +
                        "&nbsp;<a class='btn btn-danger btn-minier btn-xs' title='Reset Password' onClick='ResetPassword(\"" + data.ID + "\")'><i class='fa fa-pencil' aria-hidden='true'></i></a>" +
                        "&nbsp;<a class='btn btn-info btn-minier btn-xs' title='Lock User' onClick='LockUser(\"" + data.ID + "\")'><i class='fa fa-lock' aria-hidden='true'></i></a>" +
                        "&nbsp;<a class='btn btn-default btn-minier btn-xs' title='Unlock User' onClick='UnlockUser(\"" + data.ID + "\")'><i class='fa fa-unlock' aria-hidden='true'></i></span></a>"
                }
            };
        colCount += 1;
        thead += "<th style='width:130px'><div class='text-center'><a class='btn btn-success btn-minier btn-xs' title='Edit' onClick='AddUser()'><i class='fa fa-plus' aria-hidden='true'></i><b>&nbsp;Add User</b></a></div></th>";
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {                
                dataColumns[colCount] = { "data": compdatakey, "autowidth": true };                
                thead += "<th>" + compdatakey + "</th>";
                colCount += 1;
            });
            return false;
        });
                
        thead += "</tr>";
        fntblComponent.append(thead);
        tblComponent = $("#tbl-UserAccounts").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "aaSorting": [],
            "sAjaxDataProp": "",
            "ajax": {
                "url": url,
                "datatype": "json",
                "type": "GET"
            },
            "columns": dataColumns
        });
        tblComponent.columns(1).visible(false, false);

    });
}

function AddUser() {    
    $.get("DTSecurityManager/Add_UserAccount", { }, function (data) {
        $("#add-display-modal-body").children().remove();
        $("#add-display-modal-body").append(data);
        $("#add-display-modal-footer").children().remove();
        $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='AddUserInDatabase(\"userAccount\")'>Add User</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
        $('.applyDatePicker').datepicker({ forceParse: false });
        $("#AddTableData").modal();        
    });
}

function EditUser(ID) {
    $.get("DTSecurityManager/Update_UserAccount", { "ID": ID }, function (data) {
        $("#Update-display-modal-body").children().remove();
        $("#Update-display-modal-body").append(data);
        $("#Update-display-modal-footer").children().remove();
        $("#Update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='UpdateUserInDatabase(\"userAccount\")'>Update User</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
        $('.applyDatePicker').datepicker({ forceParse: false });
        $("#UpdateTableData").modal();
    });
}

function userRoleUserAccount(ID) {
    
    $.get("DTSecurityManager/UserRoles", { "ID": ID }, function (data) {
        $("#userRole-display-modal-body").children().remove();
        $("#userRole-display-modal-body").append(data);
        $("#userRole-display-modal-footer").children().remove();
        $("#userRole-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='UpdateUserRoleInDatabase(\"userAccount\")'>Update Role</button><button class='btn btn-danger' onclick='CanceluserRole()'>Cancel</button></div>");
        $('.applyDatePicker').datepicker({ forceParse: false });                      

        $.get("DTSecurityManager/getUserRoles", { "ID": ID }, function (data) {
            $("#tblRoles tbody").children().remove();
            $.each(data.NotGrantedRoles, function (keyRole, Role) {
                $("#tblRoles tbody").append("<tr onclick='GrantRole(this)'><td style='visibility:hidden;position:absolute;'>" + Role.ID + "</td><td>" + Role.Code + "</td><td>" + Role.RoleName + "</td></tr>");
            });            
            $("#tbleGrantedRoles tbody").children().remove();
            $.each(data.GrantedRoles, function (keyRole, Role) {
                $("#tbleGrantedRoles tbody").append("<tr onclick='UnGrantRole(this)'><td style='visibility:hidden;position:absolute;'>" + Role.ID + "</td><td>" + Role.Code + "</td><td>" + Role.RoleName + "</td></tr>");
            });
            
        });      
        $("#UserRoleTableData").modal();
    });
}

function GrantRole(data) {   
    $("#tbleGrantedRoles tbody").append("<tr onclick='UnGrantRole(this)'><td style='visibility:hidden;position:absolute;'>" + data.cells[0].innerHTML + "</td><td>" + data.cells[1].innerHTML + "</td><td>" + data.cells[2].innerHTML + "</td></tr>");
    data.remove();
}

function UnGrantRole(data) {
    $("#tblRoles tbody").append("<tr onclick='GrantRole(this)'><td style='visibility:hidden;position:absolute;'>" + data.cells[0].innerHTML + "</td><td>" + data.cells[1].innerHTML + "</td><td>" + data.cells[2].innerHTML + "</td></tr>");
    data.remove();
}

function UpdateUserRoleInDatabase() {
    var userRoles = [];
    $.each($("#tbleGrantedRoles tbody").children(), function (rowKey, row) {
        var Roles = {};
        Roles.ID = row.cells[0].innerHTML;
        Roles.Code = row.cells[1].innerHTML;
        Roles.RoleName = row.cells[2].innerHTML;
        userRoles[rowKey] = Roles;
    });    
    var userAccountID = $("#userAccount_ID").val();
    var jsonObject = $.ajax({
        url: '/DTSecurityManager/UpdateUserRoles',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        data: "{ userRoles: " + JSON.stringify(userRoles) + ", UserAccountID: '" + userAccountID + "' }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            tblComponent.ajax.reload();
            $("#userRole-display-modal-body").children().remove();
            $("#userRole-display-modal-footer").children().remove();
            $("#UserRoleTableData").modal('hide');
        } else {
            $("#alertMessage").append("Updating Failed: Contact Administrator");
            $("#alertMessageModal").modal();
        }
    });
}

function ResetPassword(ID) {
    $.get("DTSecurityManager/ResetPassword_UserAccount", { "ID": ID }, function (data) {
        $("#Update-display-modal-body").children().remove();
        $("#Update-display-modal-body").append(data);
        $("#Update-display-modal-footer").children().remove();
        $("#Update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='UserResetPassword(\"userAccount\")'>Reset Password</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
        $('.applyDatePicker').datepicker({ forceParse: false });
        $("#UpdateTableData").modal();
    });
}

function LockUser(ID) {
    $.get("DTSecurityManager/UpdateStatus_UserAccount", { "ID": ID }, function (data) {
        $("#Update-display-modal-body").children().remove();
        $("#Update-display-modal-body").append(data);
        $("#Update-display-modal-footer").children().remove();
        $("#Update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateStatus(\"1\")'>Lock User</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
        $('.applyDatePicker').datepicker({ forceParse: false });
        $("#UpdateTableData").modal();
    });
}

function UnlockUser(ID) {
    $.get("DTSecurityManager/UpdateStatus_UserAccount", { "ID": ID }, function (data) {
        $("#Update-display-modal-body").children().remove();
        $("#Update-display-modal-body").append(data);
        $("#Update-display-modal-footer").children().remove();
        $("#Update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateStatus(\"0\")'>Unlock User</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
        $('.applyDatePicker').datepicker({ forceParse: false });
        $("#UpdateTableData").modal();
    });
}
function CancelAdd() {
    $("#add-display-modal-body").children().remove();
    $("#add-display-modal-footer").children().remove();
    $("#AddTableData").modal('hide');
}

function CancelUpdate() {
    $("#Update-display-modal-body").children().remove();
    $("#Update-display-modal-footer").children().remove();
    $("#UpdateTableData").modal('hide');
}

function CanceluserRole() {
    $("#userRole-display-modal-body").children().remove();
    $("#userRole-display-modal-footer").children().remove();
    $("#UserRoleTableData").modal('hide');
}

function closeAlert() {   
    $("#alertModal").modal('hide');
    $("#alertMessageModal").modal('hide');
}

function AddUserInDatabase(data) {
    $("#alertMessage").children().remove();
    $.validator.unobtrusive.parse($("#AddUserAccount"));
    $(".validation").css("color", "red");
    if (!$("#AddUserAccount").valid()) {
        $("#alertModal").modal();
        return;
    }
    if ($("#userAccount_Password").val() != $("#userAccount_ConfirmPassword").val()) {
        $("#alertMessage").append("Password does not match!");
        $("#alertMessageModal").modal();
        return;
    }
    var UserAccount = {};
    var userAccountID = $("#userAccount_ID").val();
    $.each($("#AddUserAccount")[0], function (rowkey, ctrl) {
        UserAccount[ctrl.name] = ctrl.value;
    });
    var jsonObject = $.ajax({
        url: '/DTSecurityManager/AddUserAccount',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        data: "{ userAccountModel: " + JSON.stringify(UserAccount) + " }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            tblComponent.ajax.reload();
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-footer").children().remove();
            $("#AddTableData").modal('hide');
        } else if (data == 2) {            
            $("#alertMessage").append("Updating Failed: User Code Already Exist.");
            $("#alertMessageModal").modal();
        } else {            
            $("#alertMessage").append("Updating Failed: Contact Administrator.");
            $("#alertMessageModal").modal();
        }
    });
}

function UpdateUserInDatabase(data) {
    $.validator.unobtrusive.parse($("#UpdateUserAccount"));
    $(".validation").css("color", "red");
    if (!$("#UpdateUserAccount").valid()) {
        $("#alertModal").modal();
        return;
    }    
    var UserAccount = {};
    var userAccountID = $("#userAccount_ID").val();
    $.each($("#UpdateUserAccount")[0], function (rowkey, ctrl) {
        UserAccount[ctrl.name] = ctrl.value;
    });
    var jsonObject = $.ajax({
        type: 'POST',
        url: '/DTSecurityManager/UpdateUserAccount',
        contentType: 'application/json;charset=utf-8',
        data: "{ userAccountModel: " + JSON.stringify(UserAccount) + " }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            tblComponent.ajax.reload();
            $("#Update-display-modal-body").children().remove();
            $("#Update-display-modal-footer").children().remove();
            $("#UpdateTableData").modal('hide');
        } else if (data == 2) {
            toastr.error("Updating Failed: User Code Already Exist.");
            $("#alertMessage").append("Updating Failed: User Code Already Exist.");
            $("#alertMessageModal").modal();
        } else {            
            $("#alertMessage").append("Updating Failed: Contact Administrator.");
            $("#alertMessageModal").modal();
        }
    });
}

function UserResetPassword() {
    $("#alertMessage").children().remove();
    $.validator.unobtrusive.parse($("#resetPassword"));
    $(".validation").css("color", "red");
    if (!$("#resetPassword").valid()) {
        $("#alertModal").modal();
        return;
    }
    if ($("#userAccount_Password").val() != $("#userAccount_ConfirmPassword").val()) {
        $("#alertMessage").append("Password does not match!");
        $("#alertMessageModal").modal();
        return;
    }
    var UserAccount = {};
    var userAccountID = $("#userAccount_ID").val();
    $.each($("#resetPassword")[0], function (rowkey, ctrl) {
        UserAccount[ctrl.name] = ctrl.value;
    });
    var jsonObject = $.ajax({
        url: '/DTSecurityManager/ResetPasswordUserAccount',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        data: "{ userAccountModel: " + JSON.stringify(UserAccount) + " }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            tblComponent.ajax.reload();
            $("#Update-display-modal-body").children().remove();
            $("#Update-display-modal-footer").children().remove();
            $("#UpdateTableData").modal('hide');
        } else if (data == 2) {
            $("#alertMessage").append("The password you supplied already exist in password history.");
            $("#alertMessageModal").modal();            
        } else {
            $("#alertMessage").append("Updating Failed: Contact Administrator");
            $("#alertMessageModal").modal();            
        }
    });
}

function updateStatus(data) {
    var userAccountID = $("#userAccount_ID").val();
    var jsonObject = $.ajax({
        url: '/DTSecurityManager/UpdateStatusUserAccount',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        data: "{ ID: '" + userAccountID + "', Status: '" + data + "' }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            tblComponent.ajax.reload();
            $("#Update-display-modal-body").children().remove();
            $("#Update-display-modal-footer").children().remove();
            $("#UpdateTableData").modal('hide');
        } else {
            $("#alertMessage").append("Updating Failed: Contact Administrator");
            $("#alertMessageModal").modal();
        }
    });
}