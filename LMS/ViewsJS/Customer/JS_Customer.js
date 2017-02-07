var tblComponent;
var fntblComponent = $("#tbl-customer thead")
$(document).ready(function () {    
    loadComponents("/Customer/FetchCustomerRecord");
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
        //var dataColumns =
        //    [
        //    {
        //        "data": null, "targets": -1, "sortable": false,
        //        "render": function (data, type, full, meta) {
        //            return "<a class='btn btn-success btn-minier' href='#' title='Edit' onClick='EditComponent(\"" + data.ID + "\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>";
        //        }
        //    },
        //    ];
        //var colCount = 1;
        
        
        var thead = "<tr>"
        var colCount = 0;        
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                if (compdatakey == "Code") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<a href='#' onclick='ViewCustomer(\"" + data + "\")'>" + data + "</a>";
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
        tblComponent = $("#tbl-customer").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "sAjaxDataProp": "",
            "ajax": {
                "url": url,
                "datatype": "json",
                "type": "GET"
            },
            "columns": dataColumns
        });
        tblComponent.columns(0).visible(false, false);
        //$.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
        //    $.each(data, function (datakey, comp) {
        //        if (comp.FieldisHide == "Hide") {
        //            tblComponent.columns(comp.FieldisHideColIndex).visible(false, false);
        //        }
        //    });
        //});
    });
}

function ViewCustomer(data) {
    alert(data);
}