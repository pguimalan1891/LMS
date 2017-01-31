var tblComponent = $("#tbl-component")
var fntblComponent = $("#tbl-component thead")
var compnent = "";
$(document).ready(function () {
    compnent = "Company Type"
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

function AddComponent() {
    $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {

        dlgAdd = $("#dlgAdd");        
        dlgAdd.empty();
        var AppendStr = "";
        $.each(data, function (datakey, comp) {

            AppendStr += "<div class='row'>" +
                   "<div class='col-md-3 col-md-offset-1' style='padding:5px'>" + comp.FieldName + "</div>" +
                   "<div class='col-md-4' style='padding:2px'><input class='retvetxtBox' type='text' width='100%' id='txt" + comp.FieldName + "' /></div>" +
                   "</div>";            
        });
        dlgAdd.append(AppendStr);        
        dlgAdd.dialog({
            title: "Add " + compnent,
            width: 450,
            closeOnEscape: false,
            resizable: false,
            modal: true,
            draggable: true,
            create: function () {
                $(this).parent().find('button:contains("Add")').addClass('btn btn-info btn-small');
                $(this).parent().find('button:contains("Close")').addClass('btn btn-danger btn-small');
            },
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            buttons: {
                "Add": function () {
                    var AddRet = "";
                    $(".retvetxtBox").each(function (index, element) {
                        AddRet += $(element).val() + "|"
                    });

                    var props = {
                        ComponentName: "None",
                        compData: AddRet,
                        opCode: "0"
                    };
                    
                    var req = $.ajax({
                        type: 'POST',
                        async: true,
                        url: "/DevelopmentTools/AddComponent",
                        contentType: "applicaiton/json; charset=utf-8",
                        data: { 'libcomp': JSON.stringify(props) }
                    });
                    req.error(function (request, status, error) {
                        alert(request.responseText);
                    });
                    req.done(function (data) {
                        alert("Successfully Added!");
                        $(this).dialog('destroy');
                        $(this).empty();
                    });
                },
                "Close": function () {
                    $(this).dialog('destroy');
                    $(this).empty();
                }
            }
        });
    });
}

function EditComponent(editItem) {
   
}

function DeleteComponent(delItem) {
    alert(delItem);
}