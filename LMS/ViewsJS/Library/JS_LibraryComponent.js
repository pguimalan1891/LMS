var tblComponent = $("#tbl-component")
var fntblComponent = $("#tbl-component thead")
var compnent = "";
$(document).ready(function () {
    compnent = "Company Type"
    loadComponents("/DevelopmentTools/FetchLibraryComponent");    
});

function reloadComponent() {
    tblComponent.fnDestroy();
    fntblComponent.empty();
    loadComponents("/DevelopmentTools/FetchLibraryComponent");
}
function loadComponents(url) {    
    var req = $.ajax({
        type: 'GET',
        async: true,
        url: url,
        contentType: "application/json; charset=utf-8",
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
                $(this).parent().find('button:contains("Add New")').addClass('btn btn-info btn-small');
                $(this).parent().find('button:contains("Close")').addClass('btn btn-danger btn-small');
            },
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            buttons: {
                "Add New": function () {
                    var AddRet = "";
                    $(".retvetxtBox").each(function (index, element) {
                        AddRet += $(element).val() + "|"
                    });
                    
                    var req = $.ajax({
                        type: 'POST',
                        async: true,
                        url: "/DevelopmentTools/AddComponent",
                        contentType: "application/json; charset=utf-8",
                        data: "{'compData': '" + AddRet + "','opCode': 0 }"
                    });
                    req.error(function (request, status, error) {
                        alert(request.responseText);
                    });
                    req.done(function (data) {
                        if (data == "1") {
                            alert("Record Added!");
                            dlgAdd.dialog('destroy');
                            dlgAdd.empty();
                            reloadComponent();
                        } else {
                            alert(data + " SQL Error Number!");
                        }                        
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
    $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
        dlgEdit = $("#dlgEdit");
        dlgEdit.empty();
        var AppendStr = "";
        $.each(data, function (datakey, comp) {
            AppendStr += "<div class='row'>" +
                   "<div class='col-md-3 col-md-offset-1' style='padding:5px'>" + comp.FieldName + "</div>" +
                   "<div class='col-md-4' style='padding:2px'><input class='retvetxtBox' type='text' width='100%' id='txt" + comp.FieldName + "' /></div>" +
                   "</div>";
            
        });
        dlgEdit.append(AppendStr);
        $.each(data, function (datakey, comp) {            
            $("#txt" + comp.FieldName).val($("#tbl-component tbody tr:eq(" + editItem + ") td:eq(" + comp.FieldLink + ")").text());
        });        
        dlgEdit.dialog({
            title: "Update " + compnent,
            width: 450,
            closeOnEscape: false,
            resizable: false,
            modal: true,
            draggable: true,
            create: function () {
                $(this).parent().find('button:contains("Update")').addClass('btn btn-info btn-small');
                $(this).parent().find('button:contains("Close")').addClass('btn btn-danger btn-small');
            },
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            buttons: {
                "Update": function () {
                    var UpdateRet = editItem + "|";
                    $(".retvetxtBox").each(function (index, element) {
                        UpdateRet += $(element).val() + "|"
                    });
                    
                    var req = $.ajax({
                        type: 'POST',
                        async: true,
                        url: "/DevelopmentTools/UpdateComponent",
                        contentType: "application/json; charset=utf-8",
                        data: "{'compData': '" + UpdateRet + "','opCode': 1 }"
                    });
                    req.error(function (request, status, error) {
                        alert(request.responseText);
                    });
                    req.done(function (data) {
                        if (data == "1") {
                            alert("Record Updated!");
                            dlgEdit.dialog('destroy');
                            dlgEdit.empty();
                            reloadComponent();
                        } else {
                            alert(data + " SQL Error Number!");
                        }
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

function DeleteComponent(delItem) {
    $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
        dlgDelete = $("#dlgDelete");
        dlgDelete.empty();
        var AppendStr = "";
        $.each(data, function (datakey, comp) {
            AppendStr += "<div class='row'>" +
                   "<div class='col-md-3 col-md-offset-1' style='padding:5px'>" + comp.FieldName + " :</div>" +
                   "<div class='col-md-4' style='padding:2px'><span id='spn" + comp.FieldName + "' /></div>" +
                   "</div>";

        });
        dlgDelete.append(AppendStr);
        $.each(data, function (datakey, comp) {            
            $("#spn" + comp.FieldName).text($("#tbl-component tbody tr:eq(" + delItem + ") td:eq(" + comp.FieldLink + ")").text());
        });
        dlgDelete.dialog({
            title: "Delete " + compnent,
            width: 450,
            closeOnEscape: false,
            resizable: false,
            modal: true,
            draggable: true,
            create: function () {
                $(this).parent().find('button:contains("Yes")').addClass('btn btn-danger btn-small');
                $(this).parent().find('button:contains("No")').addClass('btn btn-info btn-small');
            },
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            },
            buttons: {
                "Yes": function () {
                    var DeleteRet = delItem + "|";
                    
                    var req = $.ajax({
                        type: 'POST',
                        async: true,
                        url: "/DevelopmentTools/DeleteComponent",
                        contentType: "application/json; charset=utf-8",
                        data: "{'compData': '" + DeleteRet + "','opCode': 2 }"
                    });
                    req.error(function (request, status, error) {
                        alert(request.responseText);
                    });
                    req.done(function (data) {
                        if (data == "1") {
                            alert("Record Deleted!");
                            dlgDelete.dialog('destroy');
                            dlgDelete.empty();
                            reloadComponent();
                        } else {
                            alert(data + " SQL Error Number!");
                        }
                    });
                },
                "No": function () {
                    $(this).dialog('destroy');
                    $(this).empty();
                }
            }
        });
    });
}