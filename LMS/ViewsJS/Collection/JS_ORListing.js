var tblComponent;
var fntblComponent = $("#tblORListingSubmission thead");
$(document).ready(function () {
    loadComponent("14", "#tblORListingSubmission", $("#tblORListingSubmission thead"));
    loadComponent("13", "#tblORListingSubmitted", $("#tblORListingSubmitted thead"));
    loadComponent("1", "#tblORListingCancelled", $("#tblORListingCancelled thead"));
    $("#divSubmitted").hide();
    $("#divCancelled").hide();

});

function showListing(status) {
    if (status == 14) {
        $("#divSubmission").show();
        $("#divSubmitted").hide();
        $("#divCancelled").hide();
    } else if (status == 13) {
        $("#divSubmission").hide();
        $("#divSubmitted").show();
        $("#divCancelled").hide();
    } else if (status == 1) {
        $("#divSubmission").hide();
        $("#divSubmitted").hide();
        $("#divCancelled").show();
    }
}

function loadComponent(status, tbl, tblbody) {
    fntblComponent = tblbody;
    var req = $.ajax({
        type: 'GET',
        async: false,
        url: "OfficialReceipt/FetchORListing",
        contentType: "application/json; charset=utf-8",
        data: { "status": status }
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
                if (compdatakey == "OR No.") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<a onclick='ViewCustomer(\"" + data + "\")'>" + data + "</a>";
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
        fntblComponent.append(thead);
        tblComponent = $(tbl).DataTable({
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
                "data": { "status": status }
            },
            "columns": dataColumns
        });
    });
}