var tblComponentSubmission;
var tblComponentSubmitted;
var tblComponentCancelled;
var GlobalORNumber = "";
var fntblComponentSubmission = $("#tblORListingSubmission thead");
var fntblComponentSubmitted = $("#tblORListingSubmitted thead");
var fntblComponentCancelled = $("#tblORListingCancelled thead");
var customerName = "";
var status = "";
var tbl = "";
var tblbody = "";
var isRedirect = 0;

$(document).ready(function () {
    $("#divSubmitted").hide();
    $("#divCancelled").hide();
    status = 14; tbl = "#tblORListingSubmission";
    loadComponentSubmission();
    status = 13; tbl = "#tblORListingSubmitted";
    loadComponentSubmitted();
    status = 1; tbl = "#tblORListingCancelled";
    loadComponentCancelled();    
    showListing(14);
});

function showListing(status) {
    $("#divSubmitted").css("visibility", "visible");
    $("#divCancelled").css("visibility", "visible");
    if (status == 14) {
        $("#display-modal-header").children().remove();
        $("#display-modal-header").append(
            "<button class='btn btn-info' id='dlgadd-add-btn' onclick='updateOfficialReceipt()'>Finalize Official Receipt</button> " +
                "<button class='btn btn-danger' id='dlgadd-close-btn' onclick='cancelOfficialReceipt()'>Cancel Official Receipt</button> " +
               "<button class='btn btn-danger pull-right' onclick='$(\"#ViewOfficialReceiptModal\").modal(\"hide\");'>Close</button>"
            );
        
        $("#divSubmission").show();
        $("#divSubmitted").hide();
        $("#divCancelled").hide();
    } else if (status == 13) {
        $("#display-modal-header").children().remove();
        $("#display-modal-header").append(
            "<button class='btn btn-danger' style='visibility:hidden;' id='dlgadd-close-btn'>Cancel Official Receipt</button> " +
            " <button class='btn btn-danger pull-right' onclick='$(\"#ViewOfficialReceiptModal\").modal(\"hide\");'>Close</button>"
            );
        $("#divSubmission").hide();
        $("#divSubmitted").show();
        $("#divCancelled").hide();
    } else if (status == 1) {
        $("#display-modal-header").children().remove();
        $("#display-modal-header").append(
            "<button class='btn btn-danger' style='visibility:hidden;' id='dlgadd-close-btn'>Cancel Official Receipt</button> " +
            " <button class='btn btn-danger pull-right' onclick='$(\"#ViewOfficialReceiptModal\").modal(\"hide\");'>Close</button>"
            );
        $("#divSubmission").hide();
        $("#divSubmitted").hide();
        $("#divCancelled").show();
    }
}

function loadComponentSubmission() {    
    var req = $.ajax({
        type: 'GET',
        async: false,
        url: "OfficialReceipt/FetchORListing",
        contentType: "application/json; charset=utf-8",
        data: { "status": status, "CustomerName": customerName }
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {
        var dataColumns = [];
        var thead = "<tr>";
        var colCount = 0;
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                if (compdatakey == "OR No") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<a onclick='viewOR(\"" + data + "\")'>" + data + "</a>";
                        }
                    };
                } else {
                    dataColumns[colCount] = { "data": compdatakey, "autowidth": true };
                }
                thead += "<th>" + compdatakey + "</th>";
                colCount += 1;
            });
            return false;
        });
        thead += "</tr>";
        fntblComponentSubmission.append(thead);
        tblComponentSubmission = $(tbl).DataTable({
            "pageLength": 10,
            "bFilter": true,
            "sDom": '<"top"l>rt<"bottom"ip><"clear">',
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "aaSorting": [],
            "sAjaxDataProp": "",
            "ajax": {
                "url": "OfficialReceipt/FetchORListing",
                "datatype": "json",
                "type": "GET",
                "data": { "status": status, "CustomerName": customerName }
            },
            "columns": dataColumns
        });
    });
}
function loadComponentSubmitted() {
    var req = $.ajax({
        type: 'GET',
        async: false,
        url: "OfficialReceipt/FetchORListing",
        contentType: "application/json; charset=utf-8",
        data: { "status": status, "CustomerName": customerName }
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {
        var dataColumns = [];
        var thead = "<tr>";
        var colCount = 0;
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                if (compdatakey == "OR No") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<a onclick='viewOR(\"" + data + "\")'>" + data + "</a>";
                        }
                    };
                } else {
                    dataColumns[colCount] = { "data": compdatakey, "autowidth": true };
                }
                thead += "<th>" + compdatakey + "</th>";
                colCount += 1;
            });
            return false;
        });
        thead += "</tr>";
        fntblComponentSubmitted.append(thead);
        tblComponentSubmitted = $(tbl).DataTable({
            "pageLength": 10,
            "bFilter": true,
            "sDom": '<"top"l>rt<"bottom"ip><"clear">',
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "aaSorting": [],
            "sAjaxDataProp": "",
            "ajax": {
                "url": "OfficialReceipt/FetchORListing",
                "datatype": "json",
                "type": "GET",
                "data": { "status": status, "CustomerName": customerName }
            },
            "columns": dataColumns
        });
    });
}
function loadComponentCancelled() {
    var req = $.ajax({
        type: 'GET',
        async: false,
        url: "OfficialReceipt/FetchORListing",
        contentType: "application/json; charset=utf-8",
        data: { "status": status, "CustomerName": customerName }
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {
        var dataColumns = [];
        var thead = "<tr>";
        var colCount = 0;
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                if (compdatakey == "OR No") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<a onclick='viewOR(\"" + data + "\")'>" + data + "</a>";
                        }
                    };
                } else {
                    dataColumns[colCount] = { "data": compdatakey, "autowidth": true };
                }
                thead += "<th>" + compdatakey + "</th>";
                colCount += 1;
            });
            return false;
        });
        thead += "</tr>";
        fntblComponentCancelled.append(thead);
        tblComponentCancelled = $(tbl).DataTable({
            "pageLength": 10,
            "bFilter": true,
            "sDom": '<"top"l>rt<"bottom"ip><"clear">',
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "aaSorting": [],
            "sAjaxDataProp": "",
            "ajax": {
                "url": "OfficialReceipt/FetchORListing",
                "datatype": "json",
                "type": "GET",
                "data": { "status": status, "CustomerName": customerName }
            },
            "columns": dataColumns
        });
    });
}

function cancelOfficialReceipt() {
    
    if (confirm("Cancel Official Receipt?")) {        
        $.post("OfficialReceipt/UpdateOfficialReceipt", { "ORNumber": GlobalORNumber, "isFinalize": "0" }, function (data) {
            $("#alertModal").modal();
            $("#divAlert").empty();
            $("#divSuccess").empty();
            if (data == "0") {
                $("#divAlert").append("OfficialReceipt " + GlobalORNumber + " Cancelled.");
                isRedirect = 1;
            } else {
                $("#divAlert").append("Internal Server Error: Call Administrator.");
            }            
        });
    }
    
}

function updateOfficialReceipt() {    
    if (confirm("Finalize Official Receipt?")) {
        $.post("OfficialReceipt/UpdateOfficialReceipt", { "ORNumber": GlobalORNumber, "isFinalize": "1" }, function (data) {
            $("#alertModal").modal();
            $("#divAlert").empty();
            $("#divSuccess").empty();
            $("#divSuccess").append("OfficialReceipt " + GlobalORNumber + " Updated.");
            if (data == "0") {
                $("#divSuccess").append("OfficialReceipt " + GlobalORNumber + " Finalized.");
                isRedirect = 1;
            } else {
                $("#divAlert").append("Internal Server Error: Call Administrator.");
            }
        });        
    }    
    
}

function checkifRedirect() {
    if (isRedirect != 0) {
        window.location.href = "ORListing";
    }
}

function viewOR(ORNumber) {
    var modalDispViewORBody = $("#display-modal-body");
    var modalDispViewORMain = $("#ViewOfficialReceiptModal");
    GlobalORNumber = ORNumber;
    $.get("ViewORNumber", { "ORNumber": ORNumber }, function (data) {
        modalDispViewORBody.empty().html(data);
        modalDispViewORMain.modal();
    });
}

function updateListing(ident){
    if (ident == 0){
        customerName = $("#txtForSubmissionName").val();
        status = 14; tbl = "#tblORListingSubmission";
        tblComponentSubmission.destroy();
        fntblComponentSubmission.children().remove();
        loadComponentSubmission();
    }
    if (ident == 1) {
        customerName = $("#txtSubmittedName").val();
        status = 13; tbl = "#tblORListingSubmitted";
        tblComponentSubmitted.destroy();
        fntblComponentSubmitted.children().remove();
        loadComponentSubmitted();
    }
    if (ident == 2) {
        customerName = $("#txtCancelledName").val();
        status = 1; tbl = "#tblORListingCancelled";
        tblComponentCancelled.destroy();
        fntblComponentCancelled.children().remove();
        loadComponentCancelled();
    }

}