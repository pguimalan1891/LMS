var currentRowUpdate;

$(document).ready(function () {    
    $('.applyDatePicker').datepicker({ forceParse: false });
    UpdateCity("Main");
});

function editData(row, type) {
    currentRowUpdate = row;
    if (type == "spouse") {
        var custEmployment = {};
        custEmployment.BusinessTypeID = row.cells[1].id;
        custEmployment.BusinessType = row.cells[1].innerHTML;
        custEmployment.EmployerName = row.cells[2].innerHTML;
        custEmployment.NatureOfBusinessID = row.cells[3].id;
        custEmployment.NatureOfBusiness = row.cells[3].innerHTML;
        custEmployment.Income = row.cells[4].innerHTML;
        custEmployment.Contact_No = row.cells[5].innerHTML;
        var active = row.cells[6].innerHTML;
        var activeArray = new Array();
        activeArray = active.split(" to ");
        custEmployment.FromDate = activeArray[0];
        if (activeArray[1] == "Present")
            custEmployment.ToDate = "";
        else
            custEmployment.ToDate = activeArray[1];
        var jsonObject = $.ajax({
            url: '/Customer/UpdateEmployment',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', custEmployment: " + JSON.stringify(custEmployment) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $("#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"spouse\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
    if (type == "address") {
        var custAddress = {};
        custAddress.AddressTypeID = row.cells[1].id;
        custAddress.AddressType = row.cells[1].innerHTML;
        custAddress.BarangayName = row.cells[2].innerHTML;
        custAddress.StreetAddress = row.cells[3].innerHTML;
        custAddress.PostalCode = row.cells[4].innerHTML;
        custAddress.CityID = row.cells[5].id;
        custAddress.City = row.cells[5].innerHTML;
        custAddress.ProvinceID = row.cells[6].id;
        custAddress.Province = row.cells[6].innerHTML;
        var contactNo = row.cells[7].innerHTML;
        var contactNoArray = contactNo.split(" | ");
        custAddress.PhoneNumber = contactNoArray[0];
        custAddress.MobileNumber = contactNoArray[1];
        custAddress.ResidentDate = row.cells[8].innerHTML;
        custAddress.HomeOwnershipID = row.cells[9].id;
        custAddress.HomeOwnerShip = row.cells[9].innerHTML;
        var jsonObject = $.ajax({
            url: '/Customer/UpdateAddress',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', custAddress: " + JSON.stringify(custAddress) + " }"
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
    if (type == "dependents") {
        var custDependents = {};
        var FullName = row.cells[1].id.split("|");
        custDependents.LastName = FullName[0];
        custDependents.FirstName = FullName[1];
        custDependents.MiddleName = FullName[2];
        custDependents.GenderID = row.cells[2].id;
        custDependents.Gender = row.cells[2].innerHTML;
        custDependents.BirthDate = row.cells[3].innerHTML;
        custDependents.RelationshipTypeID = row.cells[4].id;
        custDependents.RelationshipType = row.cells[4].innerHTML;
        var FullAddress = row.cells[5].id.split("|");
        custDependents.StreetAddress = FullAddress[0];
        custDependents.CityID = FullAddress[1];
        custDependents.ProvinceID = FullAddress[2];
        custDependents.SchoolAddress = row.cells[6].innerHTML;
        custDependents.ContactNo = row.cells[7].innerHTML;
        var jsonObject = $.ajax({
            url: '/Customer/UpdateDependent',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', custDependents: " + JSON.stringify(custDependents) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $("#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"dependents\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
    if (type == "employment") {
        var custEmployment = {};
        custEmployment.BusinessTypeID = row.cells[1].id;
        custEmployment.BusinessType = row.cells[1].innerHTML;
        custEmployment.EmployerName = row.cells[2].innerHTML;
        custEmployment.NatureOfBusinessID = row.cells[3].id;
        custEmployment.NatureOfBusiness = row.cells[3].innerHTML;
        custEmployment.Income = row.cells[4].innerHTML;
        custEmployment.Contact_No = row.cells[5].innerHTML;
        var active = row.cells[6].innerHTML;
        var activeArray = new Array();
        activeArray = active.split(" to ");
        custEmployment.FromDate = activeArray[0];
        if (activeArray[1] == "Present")
            custEmployment.ToDate = "";
        else
            custEmployment.ToDate = activeArray[1];
        var jsonObject = $.ajax({
            url: '/Customer/UpdateEmployment',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', custEmployment: " + JSON.stringify(custEmployment) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $("#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"employment\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
    if (type == "education") {
        var custEducation = {};
        custEducation.EducationTypeID = row.cells[1].id;
        custEducation.EducationType = row.cells[1].innerHTML;
        custEducation.SchoolName = row.cells[2].innerHTML;
        custEducation.GraduationDate = row.cells[3].innerHTML;
        var jsonObject = $.ajax({
            url: '/Customer/UpdateEducation',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', custEducation: " + JSON.stringify(custEducation) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $("#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"education\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
    if (type == "character") {
        var custCharacter = {};
        var FullName = row.cells[1].id.split("|");
        custCharacter.LastName = FullName[0];
        custCharacter.FirstName = FullName[1];
        custCharacter.MiddleName = FullName[2];
        custCharacter.RelationShip = row.cells[2].innerHTML;
        var FullAddress = row.cells[3].id.split("|");
        custCharacter.StreetAddress = FullAddress[0];
        custCharacter.CityID = FullAddress[1];
        custCharacter.ProvinceID = FullAddress[2];
        custCharacter.ContactNo = row.cells[4].innerHTML;
        var jsonObject = $.ajax({
            url: '/Customer/UpdateCharacter',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'update', custCharacter: " + JSON.stringify(custCharacter) + " }"
        });
        jsonObject.done(function (data) {
            $("#update-display-modal-body").children().remove();
            $("#update-display-modal-body").append(data);
            $("#update-display-modal-footer").children().remove();
            $("#update-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='updateTable(\"character\")'>Update</button><button class='btn btn-danger' onclick='CancelUpdate()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#UpdateTableData").modal();
        });
    }
}

function addData(type) {
    if (type == "spouse") {
        var custEmployment = {};
        var jsonObject = $.ajax({
            url: '/Customer/UpdateEmployment',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', custEmployment: " + JSON.stringify(custEmployment) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"spouse\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();
            UpdateCity("addBox");
        });
    }
    if (type == "address") {
        var custAddress = {};
        var jsonObject = $.ajax({
            url: '/Customer/UpdateAddress',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', custAddress: " + JSON.stringify(custAddress) + " }"
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
    if (type == "dependents") {
        var custDependents = {};
        var jsonObject = $.ajax({
            url: '/Customer/UpdateDependent',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', custDependents: " + JSON.stringify(custDependents) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"dependents\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();
            UpdateCity("addBox");
        });
    }
    if (type == "employment") {
        var custEmployment = {};
        var jsonObject = $.ajax({
            url: '/Customer/UpdateEmployment',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', custEmployment: " + JSON.stringify(custEmployment) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"employment\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();
            UpdateCity("addBox");
        });
    }
    if (type == "education") {
        var custEducation = {};
        var jsonObject = $.ajax({
            url: '/Customer/UpdateEducation',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', custEducation: " + JSON.stringify(custEducation) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"education\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();
            UpdateCity("addBox");
        });
    }
    if (type == "character") {
        var custCharacter = {};
        var jsonObject = $.ajax({
            url: '/Customer/UpdateCharacter',
            type: 'POST',
            contentType: 'application/json;charset=utf-8',
            data: "{ UpdateType: 'add', custCharacter: " + JSON.stringify(custCharacter) + " }"
        });
        jsonObject.done(function (data) {
            $("#add-display-modal-body").children().remove();
            $("#add-display-modal-body").append(data);
            $("#add-display-modal-footer").children().remove();
            $("#add-display-modal-footer").append("<div class='row' style='padding:10px'><button class='btn btn-info' onclick='addTable(\"character\")'>Add</button><button class='btn btn-danger' onclick='CancelAdd()'>Cancel</button></div>");
            $('.applyDatePicker').datepicker({ forceParse: false });
            $("#AddTableData").modal();
            UpdateCity("addBox");
        });
    }

}

function CancelUpdate() {
    $("#update-display-modal-body").children().remove();
    $("#update-display-modal-footer").children().remove();
    $("#UpdateTableData").modal('hide');
}

function updateTable(type) {
    if (type == "spouse") {
        $.validator.unobtrusive.parse($("#updateEmployment"));
        $(".validation").css("color", "red");
        if (!$("#updateEmployment").valid()) {
            return;
        }
        var spouseVal = $("#updateEmployment").serializeArray();
        currentRowUpdate.cells[1].id = spouseVal[0].value;
        currentRowUpdate.cells[1].innerHTML = $("#BusinessTypeID>option:selected").text();
        currentRowUpdate.cells[2].innerHTML = spouseVal[1].value;
        currentRowUpdate.cells[3].id = spouseVal[2].value;
        currentRowUpdate.cells[3].innerHTML = $("#NatureOfBusinessID>option:selected").text();
        currentRowUpdate.cells[4].innerHTML = spouseVal[3].value;
        currentRowUpdate.cells[5].innerHTML = spouseVal[4].value;
        if (spouseVal[6].value == '') {
            currentRowUpdate.cells[6].innerHTML = spouseVal[5].value + " to Present";
        } else {
            currentRowUpdate.cells[6].innerHTML = spouseVal[5].value + " to " + spouseVal[6].value;
        }
    }
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
    if (type == "dependents") {
        $.validator.unobtrusive.parse($("#updateDependents"));
        $(".validation").css("color", "red");
        if (!$("#updateDependents").valid()) {
            return;
        }
        var dependentsVal = $("#updateDependents").serializeArray();
        currentRowUpdate.cells[1].id = dependentsVal[2].value + "|" + dependentsVal[0].value + "|" + dependentsVal[1].value;
        currentRowUpdate.cells[1].innerHTML = dependentsVal[2].value + ", " + dependentsVal[0].value + " " + dependentsVal[1].value;
        currentRowUpdate.cells[2].id = dependentsVal[3].value;
        currentRowUpdate.cells[2].innerHTML = $(".genderUpdateBox>option:selected").text();
        currentRowUpdate.cells[3].innerHTML = dependentsVal[4].value;
        currentRowUpdate.cells[4].id = dependentsVal[5].value;
        currentRowUpdate.cells[4].innerHTML = $(".relationshipTypeUpdateBox>option:selected").text();
        currentRowUpdate.cells[5].id = dependentsVal[6].value + "|" + dependentsVal[8].value + "|" + dependentsVal[7].value;
        currentRowUpdate.cells[5].innerHTML = dependentsVal[6].value + ", " + $(".cityUpdateBox>option:selected").text() + ", " + $(".provinceUpdateBox>option:selected").text();
        currentRowUpdate.cells[6].innerHTML = dependentsVal[9].value;
        currentRowUpdate.cells[7].innerHTML = dependentsVal[10].value;
    }
    if (type == "employment") {
        $.validator.unobtrusive.parse($("#updateEmployment"));
        $(".validation").css("color", "red");
        if (!$("#updateEmployment").valid()) {
            return;
        }
        var employmentVal = $("#updateEmployment").serializeArray();
        currentRowUpdate.cells[1].id = employmentVal[0].value;
        currentRowUpdate.cells[1].innerHTML = $("#BusinessTypeID>option:selected").text();
        currentRowUpdate.cells[2].innerHTML = employmentVal[1].value;
        currentRowUpdate.cells[3].id = employmentVal[2].value;
        currentRowUpdate.cells[3].innerHTML = $("#NatureOfBusinessID>option:selected").text();
        currentRowUpdate.cells[4].innerHTML = employmentVal[3].value;
        currentRowUpdate.cells[5].innerHTML = employmentVal[4].value;
        if (employmentVal[6].value == '') {
            currentRowUpdate.cells[6].innerHTML = employmentVal[5].value + " to Present";
        } else {
            currentRowUpdate.cells[6].innerHTML = employmentVal[5].value + " to " + employmentVal[6].value;
        }
    }
    if (type == "education") {
        $.validator.unobtrusive.parse($("#updateEducation"));
        $(".validation").css("color", "red");
        if (!$("#updateEducation").valid()) {
            return;
        }
        var educationVal = $("#updateEducation").serializeArray();
        currentRowUpdate.cells[1].id = educationVal[0].value;
        currentRowUpdate.cells[1].innerHTML = $(".educationUpdateBox>option:selected").text();
        currentRowUpdate.cells[2].innerHTML = educationVal[1].value;
        currentRowUpdate.cells[3].innerHTML = educationVal[2].value;
    }
    if (type == "character") {
        $.validator.unobtrusive.parse($("#updateCharacter"));
        $(".validation").css("color", "red");
        if (!$("#updateCharacter").valid()) {
            return;
        }
        var characterVal = $("#updateCharacter").serializeArray();
        currentRowUpdate.cells[1].id = characterVal[2].value + "|" + characterVal[0].value + "|" + characterVal[1].value;
        currentRowUpdate.cells[1].innerHTML = characterVal[2].value + ", " + characterVal[0].value + " " + characterVal[1].value;
        currentRowUpdate.cells[2].innerHTML = characterVal[3].value;
        currentRowUpdate.cells[3].id = characterVal[4].value + "|" + characterVal[6].value + "|" + characterVal[5].value;
        currentRowUpdate.cells[3].innerHTML = characterVal[4].value + ", " + $(".cityUpdateBox>option:selected").text() + ", " + $(".provinceUpdateBox>option:selected").text();
        currentRowUpdate.cells[4].innerHTML = characterVal[7].value;
    }
    $("#update-display-modal-body").children().remove();
    $("#update-display-modal-footer").children().remove();
    $("#UpdateTableData").modal('hide');
}

function CancelAdd() {
    $("#add-display-modal-body").children().remove();
    $("#add-display-modal-footer").children().remove();
    $("#AddTableData").modal('hide');
}

function addTable(type) {
    var append = "";
    if (type == "spouse") {
        $.validator.unobtrusive.parse($("#addEmployment"));
        $(".validation").css("color", "red");
        if (!$("#addEmployment").valid()) {
            return;
        }
        var spouseVal = $("#addEmployment").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"spouse\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"spouse\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + spouseVal[0].value + "'>" + $("#BusinessTypeID>option:selected").text() + "</td>";
        append += "<td>" + spouseVal[1].value + "</td>";
        append += "<td id='" + spouseVal[2].value + "'>" + $("#NatureOfBusinessID>option:selected").text() + "</td>";
        append += "<td>" + spouseVal[3].value + "</td>";
        append += "<td>" + spouseVal[4].value + "</td>";
        append += "<td>" + spouseVal[5].value;
        if (spouseVal[6].value == null || spouseVal[6].value == "")
            append += " to Present</td>";
        else
            append += " to " + spouseVal[6].value + "</td>";
        append += "</tr>";
        $("#tblSpouse tbody").find("tr#delete").remove();
        $("#tblSpouse tbody").append(append);
    }
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
    if (type == "dependents") {
        $.validator.unobtrusive.parse($("#addDependents"));
        $(".validation").css("color", "red");
        if (!$("#addDependents").valid()) {
            return;
        }
        var dependentsVal = $("#addDependents").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"dependents\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"dependents\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + dependentsVal[2].value + "|" + dependentsVal[0].value + "|" + dependentsVal[1].value + "'>" + dependentsVal[2].value + ", " + dependentsVal[0].value + " " + dependentsVal[1].value + "</td>";
        append += "<td id='" + dependentsVal[3].value + "'>" + $(".genderAddBox>option:selected").text() + "</td>";
        append += "<td>" + dependentsVal[4].value + "</td>";
        append += "<td id='" + dependentsVal[5].value + "'>" + $(".relationshipTypeAddBox>option:selected").text() + "</td>";
        append += "<td id='" + dependentsVal[6].value + "|" + dependentsVal[8].value + "|" + dependentsVal[7].value + "'>" + dependentsVal[6].value + ", " + $(".cityAddBox>option:selected").text() + ", " + $(".provinceAddBox>option:selected").text() + "</td>";
        append += "<td>" + dependentsVal[9].value + "</td>";
        append += "<td>" + dependentsVal[10].value + "</td>";
        append += "</tr>";
        $("#tblDependents tbody").find("tr#delete").remove();
        $("#tblDependents tbody").append(append);
    }
    if (type == "employment") {
        $.validator.unobtrusive.parse($("#addEmployment"));
        $(".validation").css("color", "red");
        if (!$("#addEmployment").valid()) {
            return;
        }
        var employmentVal = $("#addEmployment").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"employment\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"employment\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + employmentVal[0].value + "'>" + $("#BusinessTypeID>option:selected").text() + "</td>";
        append += "<td>" + employmentVal[1].value + "</td>";
        append += "<td id='" + employmentVal[2].value + "'>" + $("#NatureOfBusinessID>option:selected").text() + "</td>";
        append += "<td>" + employmentVal[3].value + "</td>";
        append += "<td>" + employmentVal[4].value + "</td>";
        append += "<td>" + employmentVal[5].value;
        if (employmentVal[6].value == null || employmentVal[6].value == "")
            append += " to Present</td>";
        else
            append += " to " + employmentVal[6].value + "</td>";
        append += "</tr>";
        $("#tblEmployment tbody").find("tr#delete").remove();
        $("#tblEmployment tbody").append(append);
    }
    if (type == "education") {
        $.validator.unobtrusive.parse($("#addEducation"));
        $(".validation").css("color", "red");
        if (!$("#addEducation").valid()) {
            return;
        }
        var educationVal = $("#addEducation").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"education\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"education\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + educationVal[0].value + "'>" + $(".educationUpdateBox>option:selected").text() + "</td>";
        append += "<td>" + educationVal[1].value + "</td>";
        append += "<td>" + educationVal[2].value + "</td>";
        append += "</tr>";
        $("#tblEducation tbody").find("tr#delete").remove();
        $("#tblEducation tbody").append(append);
    }
    if (type == "character") {
        $.validator.unobtrusive.parse($("#addCharacter"));
        $(".validation").css("color", "red");
        if (!$("#addCharacter").valid()) {
            return;
        }
        var characterVal = $("#addCharacter").serializeArray();
        append = "<tr>";
        append += "<td id='" + getGUID() + "' class='text-center' width='70px'><a class='btn btn-success btn-xs' title='Edit' onClick='editData(this.closest(\"tr\"),\"character\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                "&nbsp;<a class='btn btn-danger btn-xs' title='Delete' onClick='DeleteData(this.closest(\"tr\"),\"character\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a></td>";
        append += "<td id='" + characterVal[2].value + "|" + characterVal[0].value + "|" + characterVal[1].value + "'>" + characterVal[2].value + ", " + characterVal[0].value + " " + characterVal[1].value + "</td>";
        append += "<td>" + characterVal[3].value + "</td>";
        append += "<td id='" + characterVal[4].value + "|" + characterVal[6].value + "|" + characterVal[5].value + "'>" + characterVal[4].value + ", " + $(".cityAddBox>option:selected").text() + ", " + $(".provinceAddBox>option:selected").text() + "</td>";
        append += "<td>" + characterVal[7].value + "</td>";
        append += "</tr>";
        $("#tblCharacter tbody").find("tr#delete").remove();
        $("#tblCharacter tbody").append(append);
    }
    $("#add-display-modal-body").children().remove();
    $("#add-display-modal-footer").children().remove();
    $("#AddTableData").modal('hide');
}

function DeleteData(row, type) {
    row.remove();
    if (type == "spouse") {
        if ($("#tblSpouse tbody tr").length <= 0) {
            $("#tblSpouse tbody").append("<tr id='delete'><td colspan='7'>The spouse neither have any employment record nor have any businesses.</td></tr>");
        }
    }
    if (type == "address") {
        if ($("#tblAddress tbody tr").length <= 0) {
            $("#tblAddress tbody").append("<tr id='delete'><td colspan='10'>No Other Addresses.</td></tr>");
        }
    }
    if (type == "dependents") {
        if ($("#tblDependents tbody tr").length <= 0) {
            $("#tblDependents tbody").append("<tr><td colspan='8'>This customer have no dependents.</td></tr>");
        }
    }
    if (type == "employment") {
        if ($("#tblEmployment tbody tr").length <= 0) {
            $("#tblEmployment tbody").append("<tr id='delete'><td colspan='7'>The customer have no Employment/Business Record and History.</td></tr>");
        }
    }
    if (type == "education") {
        if ($("#tblEducation tbody tr").length <= 0) {
            $("#tblEducation tbody").append("<tr id='delete'><td colspan='4'>This customer do not have any acquired education.</td></tr>");
        }
    }
    if (type == "character") {
        if ($("#tblCharacter tbody tr").length <= 0) {
            $("#tblCharacter tbody").append("<tr id='delete'><td colspan='5'>This customer have no character reference available.</td></tr>");
        }
    }
}

function UpdateCity(type) {
    if (type == "Main") {
        $(".cityMainBox").children().remove();
        $.post("/Customer/UpdateCity", { ProvinceID: $(".provinceMainBox>option:selected").val() }, function (data) {
            $.each(data, function (cityID, city) {
                $(".cityMainBox").append("<option value='" + city.CityID + "'>" + city.Description + "</option>")
            });
        });
    }
    if (type == "updateBox") {
        $(".cityUpdateBox").children().remove();
        $.post("/Customer/UpdateCity", { ProvinceID: $(".provinceUpdateBox>option:selected").val() }, function (data) {
            $.each(data, function (cityID, city) {
                $(".cityUpdateBox").append("<option value='" + city.CityID + "'>" + city.Description + "</option>")
            });
        });
    }
    if (type == "addBox") {
        $(".cityAddBox").children().remove();
        $.post("/Customer/UpdateCity", { ProvinceID: $(".provinceAddBox>option:selected").val() }, function (data) {
            $.each(data, function (cityID, city) {
                $(".cityAddBox").append("<option value='" + city.CityID + "'>" + city.Description + "</option>")
            });
        });
    }
}

function UpdateAgent() {
    $(".MainAgent").children().remove();
    $.post("/Customer/UpdateAgent", { ApplicationTypeID: $(".MainApplicationType>option:selected").val(), OrganizationID: $(".MainOrganization>option:selected").val() }, function (data) {
        $.each(data, function (AgentProfileID, AgentProfile) {
            $(".MainAgent").append("<option value='" + AgentProfile.AgentProfileID + "'>" + AgentProfile.Description + "</option>")
        });
    });
}

function AddCustomerData() {
    $.validator.unobtrusive.parse($("#addForm"));
    $(".validation").css("color", "red");
    if (!$("#addForm").valid()) {
        return;
    }
    var custModel = {};
    var PISID = $("#CustRecord_ID").val();
    $.each($("#addForm")[0], function (rowkey, ctrl) {
        custModel[ctrl.name] = ctrl.value;
    });
    var custEmployment = [];
    $.each($("#tblSpouse tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Employment = {};
        Employment.ID = row.cells[0].id;
        Employment.PISID = PISID;
        Employment.BusinessTypeID = row.cells[1].id;
        Employment.BusinessType = row.cells[1].innerHTML;
        Employment.EmployerName = row.cells[2].innerHTML;
        Employment.NatureOfBusinessID = row.cells[3].id;
        Employment.NatureOfBusiness = row.cells[3].innerHTML;
        Employment.Income = row.cells[4].innerHTML;
        Employment.Contact_No = row.cells[5].innerHTML;
        Employment.IsSpouse = "Y";
        var active = row.cells[6].innerHTML;
        var activeArray = new Array();
        activeArray = active.split(" to ");
        Employment.FromDate = activeArray[0];
        if (activeArray[1] == "Present")
            Employment.ToDate = "";
        else
            Employment.ToDate = activeArray[1];
        custEmployment[rowKey] = Employment;
    });
    $.each($("#tblEmployment tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Employment = {};
        Employment.PISID = PISID;
        Employment.ID = row.cells[0].id;
        Employment.BusinessTypeID = row.cells[1].id;
        Employment.BusinessType = row.cells[1].innerHTML;
        Employment.EmployerName = row.cells[2].innerHTML;
        Employment.NatureOfBusinessID = row.cells[3].id;
        Employment.NatureOfBusiness = row.cells[3].innerHTML;
        Employment.Income = row.cells[4].innerHTML;
        Employment.Contact_No = row.cells[5].innerHTML;
        Employment.IsSpouse = "N";
        var active = row.cells[6].innerHTML;
        var activeArray = new Array();
        activeArray = active.split(" to ");
        Employment.FromDate = activeArray[0];
        if (activeArray[1] == "Present")
            Employment.ToDate = "";
        else
            Employment.ToDate = activeArray[1];
        custEmployment[custEmployment.length] = Employment;
    });
    var custAddress = [];
    $.each($("#tblAddress tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Address = {};
        Address.ID = row.cells[0].id;
        Address.PISID = PISID;
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
        custAddress[rowKey] = Address;
    });
    var custDependents = [];
    $.each($("#tblDependents tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Dependents = {};
        Dependents.PISID = PISID;
        Dependents.ID = row.cells[0].id;
        var FullName = row.cells[1].id.split("|");
        Dependents.LastName = FullName[0];
        Dependents.FirstName = FullName[1];
        Dependents.MiddleName = FullName[2];
        Dependents.GenderID = row.cells[2].id;
        Dependents.Gender = row.cells[2].innerHTML;
        Dependents.BirthDate = row.cells[3].innerHTML;
        Dependents.RelationshipTypeID = row.cells[4].id;
        Dependents.RelationshipType = row.cells[4].innerHTML;
        var FullAddress = row.cells[5].id.split("|");
        Dependents.StreetAddress = FullAddress[0];
        Dependents.CityID = FullAddress[1];
        Dependents.ProvinceID = FullAddress[2];
        Dependents.SchoolAddress = row.cells[6].innerHTML;
        Dependents.ContactNo = row.cells[7].innerHTML;
        custDependents[rowKey] = Dependents;
    });
    var custEducation = [];
    $.each($("#tblEducation tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Education = {};
        Education.PISID = PISID;
        Education.ID = row.cells[0].id;
        Education.EducationTypeID = row.cells[1].id;
        Education.EducationType = row.cells[1].innerHTML;
        Education.SchoolName = row.cells[2].innerHTML;
        Education.GraduationDate = row.cells[3].innerHTML;
        custEducation[rowKey] = Education;
    });
    var custCharacter = [];
    $.each($("#tblCharacter tbody").children(), function (rowKey, row) {
        if (row.cells.length == 1) {
            return false;
        }
        var Character = {};
        Character.PISID = PISID;
        Character.ID = row.cells[0].id;
        var FullName = row.cells[1].id.split("|");
        Character.LastName = FullName[0];
        Character.FirstName = FullName[1];
        Character.MiddleName = FullName[2];
        Character.RelationShip = row.cells[2].innerHTML;
        var FullAddress = row.cells[3].id.split("|");
        Character.StreetAddress = FullAddress[0];
        Character.CityID = FullAddress[1];
        Character.ProvinceID = FullAddress[2];
        Character.ContactNo = row.cells[4].innerHTML;
        custCharacter[rowKey] = Character;
    });

    var jsonObject = $.ajax({
        url: '/Customer/AddCustomerData',
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        data: "{ custModel: " + JSON.stringify(custModel) + ", custAddress: " + JSON.stringify(custAddress) + ", custDependents: " + JSON.stringify(custDependents) + ", custEmployment: " + JSON.stringify(custEmployment) + ", custEducation: " + JSON.stringify(custEducation) + ", custCharacter: " + JSON.stringify(custCharacter) + ",PISID: '" + PISID + "' }"
    });
    jsonObject.done(function (data) {
        if (data == 1) {            
            toastr.info("Successful Updating.");
            window.location.href = '/Customer?ID=' + PISID;
        } else {            
            toastr.error("Updating Failed: Contact Administrator.");
        }
    });
}

function getGUID() {
    var ret;
    var jsonObject = $.ajax({
        url: '/Customer/getGUID',
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