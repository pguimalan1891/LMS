
$(document).ready(function () {    
    $("#OfficialReceipt_PaymentModeID").on("change", function () {
        if ($("#OfficialReceipt_PaymentModeID>option:selected").text() != "Cash") {
            $("#OfficialReceipt_BankID").removeAttr('disabled');
            $("#OfficialReceipt_CheckNo").removeAttr('disabled');
        } else {
            $("#OfficialReceipt_PaymentModeID").val(0);
            $("#OfficialReceipt_CheckNo").val("");
            $("#OfficialReceipt_BankID").attr('disabled', 'disabled');
            $("#OfficialReceipt_CheckNo").attr('disabled', 'disabled');
        }
    });
});

function addData(type) {
    if (type == "sundry") {
        var sundyDetails = {};
        var jsonObject = $.ajax({
            url: 'OfficialReceipt/UpdateSundry',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', sundry: " + JSON.stringify(sundyDetails) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"sundry\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();           
        });
    }
}

function CancelAdd() {
    $("#add-display-modal-body").children().remove();
    $("#add-display-modal-footer").children().remove();
    $("#AddTableData").modal('hide');
}

function editData(row, type) {
    currentRowUpdate = row;
    if (type == "sundry") {
        var sundyDetails = {};
        sundyDetails.CMDMAccountTypeID = row.cells[1].id;
        sundyDetails.SundryAmount = row.cells[2].innerHTML;
        var jsonObject = $.ajax({
            url: 'OfficialReceipt/UpdateSundry',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'edit', sundry: " + JSON.stringify(sundyDetails) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $( "#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"sundry\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
}

function updateTable(type) {
    if (type == "sundry") {
        var sundryVal = $("#updateSundry").serializeArray();
        currentRowUpdate.cells[1].id = sundryVal[0].value;
        currentRowUpdate.cells[1].innerHTML = $("#SundryDetails_CMDMAccountTypeID>option:selected").text();
        currentRowUpdate.cells[2].innerHTML = sundryVal[1].value;
        $("#update-display-modal-body").children().remove();
        $("#update-display-modal-footer").children().remove();
        $("#UpdateTableData").modal('hide');
    }
}

function CancelUpdate() {
    $("#update-display-modal-body").children().remove();
    $("#update-display-modal-footer").children().remove();
    $("#UpdateTableData").modal('hide');
}

function addTable(type) {
    var append = "";
    if (type == "sundry") {
        var sundryVal = $("#addSundry").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"sundry\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"sundry\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + sundryVal[0].value + "'>" + $("#SundryDetails_CMDMAccountTypeID>option:selected").text() + "</td>";
        append += "<td>" + sundryVal[1].value + "</td>";       
        append += "</tr>";
        $("#tblAddSundry tbody").find("tr#delete").remove();
        $("#tblAddSundry tbody").append(append);
        $("#add-display-modal-body").children().remove();
        $("#add-display-modal-footer").children().remove();
        $("#AddTableData").modal('hide');
    }
}

function DeleteData(row, type) {
    row.remove();
    if (type == "sundry") {
        if ($("#tblAddSundry tbody tr").length <= 0) {
            $("#tblAddSundry tbody").append("<tr id='delete'><td colspan='3'>Please add an account.</td></tr>");
        }
    }
}

function submitSundryOfficialReceipt() {
    $.validator.unobtrusive.parse($("#submitForm"));
    if (!$("#submitForm").valid()) {
        $("#alertModal").modal();
        return;
    }
    var OfficialReceiptModel = {};
    $.each($("#submitForm")[0], function (rowkey, ctrl) {
        OfficialReceiptModel[ctrl.name] = ctrl.value;
    });
    var sundryAccounts = [];
    $.each($("#tblAddSundry tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Sundry = {};
        Sundry.ID = row.cells[0].id;
        Sundry.CMDMAccountTypeID = row.cells[1].id;
        Sundry.SundryAmount = row.cells[2].innerHTML;
        sundryAccounts[rowKey] = Sundry;
    });
    var jsonObject = $.ajax({
        type: 'POST',
        url: 'OfficialReceipt/SubmitSundry',
        contentType: 'application/json;charset=utf-8',
        data: "{ ORModel: " + JSON.stringify(OfficialReceiptModel) + ",sundry: " + JSON.stringify(sundryAccounts) + " }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            window.location.href = 'Customer?ID=' + PISID;
        } else {
            toastr.error("Updating Failed: Contact Administrator.");
        }
    });
}

function getGUID() {
    var ret;
    var jsonObject = $.ajax({
        url: 'Customer/getGUID',
        type: 'GET',
        async: false,
        contentType: 'application/json;charset=utf-8',
        data: "{ }"
    });
    jsonObject.done(function (data) {
        ret = data;
    });
    return ret;
}