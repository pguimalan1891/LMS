var tblComponent = $("#tbl-component")
var fntblComponent = $("#tbl-component thead")
$(document).ready(function () {
    loadComponents("/DevelopmentTools/FetchLibraryComponent");    
});

function loadComponents(url) {
    var req = $.ajax({
        type: 'GET',
        async: true,
        url: url,
        contentType: "applicaiton/json; charset=utf-8",
        data: {},
    });

    req.error(function (request, status, error) {
        alert(request.responseText);
    });

    req.done(function (data) {
        
        var dataColumns = [
            {
                "data": null, "targets": -1, "sortable": false, "width": "90px",
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-success btn-minier' href='#' title='Edit' onClick='EditComponent(" + data.ID + ")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                        "&nbsp;<a class='btn btn-danger btn-minier' href='#' title='Delete' onClick='DeleteComponent(" + data.ID + ")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a>"
                }
            },
        ];
        var colCount = 1;
        var thead = "<tr><th></th>"
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                dataColumns[colCount] = { "data": compdatakey, "autoWidth": true };
                thead += "<th>" + compdatakey + "</th>";
                colCount += 1;
            });
            return false;
        });
        thead += "</tr>";
        fntblComponent.append(thead);        

        tblComponent.dataTable({
            "pageLength": 10,
            "bFilter": true,
            "bProcessing": true,
            "bServerside": true,
            "sAjaxDataProp": "",
            "data": data,
            "columns": dataColumns
        });
    });
}

function EditComponent(editItem) {
    alert(editItem);
}

function DeleteComponent(delItem) {
    alert(delItem);
}