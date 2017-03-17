var currentRowUpdate;

$(document).ready(function () {
    loadAllTables();
    $('.applyDatePicker').datepicker({ forceParse: false });
});

function loadAllTables() {
    event.preventDefault();
    var tbl;
    var append;
    $.post("/MaintenanceAgentProfile/GetAgentFullData", { ID: $("#AgentProfileID").val(), Type: "Address" }, function (data) {
        tbl = $("#tblAddress tbody");
        append = "";
        var isOtherAddressExist = 0;
        $.each(data.AgentAddress, function (rowkey, rowvalue) {
            if (rowvalue.AddressTypeID != "0") {
                isOtherAddressExist = 1;
                append = "<tr>";
                append += "<td id='" + rowvalue.ID + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"address\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                        "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"address\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
                append += "<td id='" + rowvalue.AddressTypeID + "'>" + rowvalue.AddressType + "</td>";
                append += "<td>" + rowvalue.BarangayName + "</td>";
                append += "<td>" + rowvalue.StreetAddress + "</td>";
                append += "<td>" + rowvalue.PostalCode + "</td>";
                append += "<td id='" + rowvalue.CityID + "'>" + rowvalue.City + "</td>";
                append += "<td id='" + rowvalue.ProvinceID + "'>" + rowvalue.Province + "</td>";
                append += "<td>" + rowvalue.PhoneNumber + " | " + rowvalue.MobileNumber + "</td>";
                append += "<td>" + rowvalue.ResidentDate + "</td>";;
                append += "<td id='" + rowvalue.HomeOwnershipID + "'>" + rowvalue.HomeOwnerShip + "</td>";;
                append += "</tr>";
                tbl.append(append);
            }
        });
        if (isOtherAddressExist == 0) {
            tbl.append("<tr id='delete'><td colspan='10'>No Other Addresses.</td></tr>");
        }
    });
}
function addData(type) {
    if (type == "address") {
        var agentAddress = {};
        var jsonObject = $.ajax({
            url: '/MaintenanceAgentProfile/UpdateAddress',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', AgentAddress: " + JSON.stringify(agentAddress) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"address\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();
            UpdateCity("addBox");
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
    if (type == "address") {
        var agentAddress = {};
        agentAddress.AddressTypeID = row.cells[1].id;
        agentAddress.AddressType = row.cells[1].innerHTML;
        agentAddress.BarangayName = row.cells[2].innerHTML;
        agentAddress.StreetAddress = row.cells[3].innerHTML;
        agentAddress.PostalCode = row.cells[4].innerHTML;
        agentAddress.CityID = row.cells[5].id;
        agentAddress.City = row.cells[5].innerHTML;
        agentAddress.ProvinceID = row.cells[6].id;
        agentAddress.Province = row.cells[6].innerHTML;
        var contactNo = row.cells[7].innerHTML;
        var contactNoArray = contactNo.split(" | ");
        agentAddress.PhoneNumber = contactNoArray[0];
        agentAddress.MobileNumber = contactNoArray[1];
        agentAddress.ResidentDate = row.cells[8].innerHTML;
        agentAddress.HomeOwnershipID = row.cells[9].id;
        agentAddress.HomeOwnerShip = row.cells[9].innerHTML;
        var jsonObject = $.ajax({
            url: '/MaintenanceAgentProfile/UpdateAddress',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', AgentAddress: " + JSON.stringify(agentAddress) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $("#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"address\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
}

function CancelUpdate() {
    $("#update-display-modal-body").children().remove();
    $("#update-display-modal-footer").children().remove();
    $("#UpdateTableData").modal('hide');
}

function addTable(type) {
    if (type == "address") {
        $.validator.unobtrusive.parse($("#addAddress"));
        $(".validation").css("color", "red");
        if (!$("#addAddress").valid()) {
            return;
        }
        var addressVal = $("#addAddress").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"address\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"address\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + addressVal[0].value + "'>" + $(".addressTypeAddBox>option:selected").text() + "</td>";
        append += "<td>" + addressVal[3].value + "</td>";
        append += "<td>" + addressVal[4].value + "</td>";
        append += "<td>" + addressVal[5].value + "</td>";
        append += "<td id='" + addressVal[2].value + "'>" + $(".cityAddBox>option:selected").text() + "</td>";
        append += "<td id='" + addressVal[1].value + "'>" + $(".provinceAddBox>option:selected").text() + "</td>";
        append += "<td>" + addressVal[6].value + " | " + addressVal[7].value + "</td>";
        append += "<td>" + addressVal[8].value + "</td>";;
        append += "<td id='" + addressVal[9].value + "'>" + $(".HomeOwnAddBox>option:selected").text() + "</td>";;
        append += "</tr>";
        $("#tblAddress tbody").find("tr#delete").remove();
        $("#tblAddress tbody").append(append);
    }
    $("#add-display-modal-body").children().remove();
    $("#add-display-modal-footer").children().remove();
    $("#AddTableData").modal('hide');
}

function updateTable(type) {
    if (type == "address") {
        $.validator.unobtrusive.parse($("#updateAddress"));
        $(".validation").css("color", "red");
        if (!$("#updateAddress").valid()) {
            return;
        }
        var addressVal = $("#updateAddress").serializeArray();
        currentRowUpdate.cells[1].id = addressVal[0].value;
        currentRowUpdate.cells[1].innerHTML = $(".addressTypeUpdateBox>option:selected").text();
        currentRowUpdate.cells[6].id = addressVal[1].value;
        currentRowUpdate.cells[6].innerHTML = $(".provinceUpdateBox>option:selected").text();
        currentRowUpdate.cells[5].id = addressVal[2].value;
        currentRowUpdate.cells[5].innerHTML = $(".cityUpdateBox>option:selected").text();
        currentRowUpdate.cells[2].innerHTML = addressVal[3].value;
        currentRowUpdate.cells[3].innerHTML = addressVal[4].value;
        currentRowUpdate.cells[4].innerHTML = addressVal[5].value;
        currentRowUpdate.cells[7].innerHTML = addressVal[6].value + " | " + addressVal[7].value;
        currentRowUpdate.cells[8].innerHTML = addressVal[8].value;
        currentRowUpdate.cells[9].id = addressVal[9].value;
        currentRowUpdate.cells[9].innerHTML = $(".HomeOwnUpdateBox>option:selected").text();
    }
    $("#update-display-modal-body").children().remove();
    $("#update-display-modal-footer").children().remove();
    $("#UpdateTableData").modal('hide');
}

function DeleteData(row, type) {
    row.remove();
    if (type == "address") {
        if ($("#tblAddress tbody tr").length <= 0) {
            $("#tblAddress tbody").append("<tr id='delete'><td colspan='10'>No Other Addresses.</td></tr>");
        }
    }
}

function UpdateCity(type) {
    if (type == "Main") {
        $(".cityMainBox").children().remove();
        $.post("/MaintenanceAgentProfile/UpdateCity", { ProvinceID: $(".provinceMainBox>option:selected").val() }, function (data) {
            $.each(data, function (cityID, city) {
                $(".cityMainBox").append("<option value='" + city.CityID + "'>" + city.Description + "</option>")
            });
        });
    }
    if (type == "updateBox") {
        $(".cityUpdateBox").children().remove();
        $.post("/MaintenanceAgentProfile/UpdateCity", { ProvinceID: $(".provinceUpdateBox>option:selected").val() }, function (data) {
            $.each(data, function (cityID, city) {
                $(".cityUpdateBox").append("<option value='" + city.CityID + "'>" + city.Description + "</option>")
            });
        });
    }
    if (type == "addBox") {
        $(".cityAddBox").children().remove();
        $.post("/MaintenanceAgentProfile/UpdateCity", { ProvinceID: $(".provinceAddBox>option:selected").val() }, function (data) {
            $.each(data, function (cityID, city) {
                $(".cityAddBox").append("<option value='" + city.CityID + "'>" + city.Description + "</option>")
            });
        });
    }
}

function addAgentProfileData(type) {
    $.validator.unobtrusive.parse($("#addForm"));
    $(".validation").css("color", "red");
    if (!$("#addForm").valid()) {
        return;
    }
    var AgentProfileModel = {};
    var AgentProfileID = $("#AgentProfileID").val();
    $.each($("#addForm")[0], function (rowkey, ctrl) {
        AgentProfileModel[ctrl.name] = ctrl.value;
    });
    var AgentAddress = [];
    $.each($("#tblAddress tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Address = {};
        Address.ID = row.cells[0].id;
        Address.AgentProfileID = AgentProfileID;
        Address.AddressTypeID = row.cells[1].id;
        Address.AddressType = row.cells[1].innerHTML;
        Address.BarangayName = row.cells[2].innerHTML;
        Address.StreetAddress = row.cells[3].innerHTML;
        Address.PostalCode = row.cells[4].innerHTML;
        Address.CityID = row.cells[5].id;
        Address.City = row.cells[5].innerHTML;
        Address.ProvinceID = row.cells[6].id;
        Address.Province = row.cells[6].innerHTML;
        var contactNo = row.cells[7].innerHTML;
        var contactNoArray = contactNo.split(" | ");
        Address.PhoneNumber = contactNoArray[0];
        Address.MobileNumber = contactNoArray[1];
        Address.ResidentDate = row.cells[8].innerHTML;
        Address.HomeOwnershipID = row.cells[9].id;
        Address.HomeOwnerShip = row.cells[9].innerHTML;
        AgentAddress[rowKey] = Address;
    });
    var jsonObject = $.ajax({
        url: '/MaintenanceAgentProfile/AddAgentData',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        data: "{ AgentProfileModel: " + JSON.stringify(AgentProfileModel) + ", AgentAddress: " + JSON.stringify(AgentAddress) + ",AgentProfileID: '" + AgentProfileID + "' }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {
            toastr.info("Successful Updating.");
            window.location.href = '/AgentProfile?ID=' + AgentProfileID;
        } else {
            toastr.error("Updating Failed: Contact Administrator.");
        }
    });
}

function getGUID() {
    var ret;
    var jsonObject = $.ajax({
        url: '/MaintenanceAgentProfile/getGUID',
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