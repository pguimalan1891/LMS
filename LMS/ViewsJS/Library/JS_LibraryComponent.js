var tblComponent;
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
        contentType: "application/json; charset=utf-8",
        data: {},
    });

    req.error(function (request, status, error) {
        alert(request.responseText);
    });

    req.done(function (data) {
        
        var dataColumns = [
            {
                "data": null, "targets": -1, "sortable": false,
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-success btn-minier' href='#' title='Edit' onClick='EditComponent(\"" + data.ID + "\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                        "&nbsp;<a class='btn btn-danger btn-minier' href='#' title='Delete' onClick='DeleteComponent(\"" + data.ID + "\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a>"
                }
            },
        ];
        var colCount = 1;
        var thead = "<tr><th></th>"
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
        tblComponent = $("#tbl-component").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "bProcessing": true,
            "bServerside": true,
            "sAjaxDataProp": "",
            "ajax": {
                "url": url,
                "datatype": "json",
                "type": "GET"
            },
            "columns": dataColumns
        });
        $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
            $.each(data, function (datakey, comp) {
                if (comp.FieldisHide == "Hide") {
                    tblComponent.columns(comp.FieldisHideColIndex).visible(false, false);
                }
            });
        });
    });

    
}

function AddComponent() {
    event.preventDefault();
    $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
        dlgAdd = $("#dlgAdd");        
        dlgAdd.empty();
        var grpSelect = {};
        var AppendStr = "";
        $.each(data, function (datakey, comp) {
            if (comp.FieldisHide == "Show") {
                if (comp.FieldComponentType == "single") {
                    AppendStr += "<div class='row'>" +
                           "<div class='form-group'>" +
                           "<div class='col-md-3 col-md-offset-1'><label for='txt" + comp.FieldComponentID + "'>" + comp.FieldName + "</label></div>" +
                           "<div class='form-group'><div class='col-md-7'><input style='width:100%' class='form-control retvetxtBox' type='text' id='txt" + comp.FieldComponentID + "' /></div></div>" +
                           "</div></div>";
                    dlgAdd.append(AppendStr);
                    AppendStr = "";
                } else if (comp.FieldComponentType == "select" && comp.FieldisHide == "Show") {                    
                    var isgrpSelectExist = 0;
                    $.each(grpSelect, function (grpDataKey, grpSelectData) {
                        if (grpSelectData == comp.FieldName) {
                            isgrpSelectExist = 1;
                        }
                    });
                    if (isgrpSelectExist == 0) {
                        AppendStr += "<div class='row'>" +
                            "<div class='form-group'>" +
                            "<div class='col-md-3 col-md-offset-1' ><label for='slc" + comp.FieldComponentID + "'>" + comp.FieldName + "</label></div>";
                        AppendStr += "<div class='form-group'><div class='col-md-7'><select style='width:100%' class='form-control retvetSelect' id='slc" + comp.FieldComponentID + "'><option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option></select></div>" +
                            "</div></div></div>";
                        grpSelect[comp.FieldName] = comp.FieldName;
                        dlgAdd.append(AppendStr);
                        AppendStr = "";
                    } else {
                        $("#slc" + comp.FieldComponentID).append("<option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option>");
                    }
                }
            }
        });              
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
                    $(".retvetSelect").each(function (index, element) {
                        AddRet += $("#" + element.id + ">option:selected").val() + "|";
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
                            tblComponent.ajax.reload();
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
    event.preventDefault();
    $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
        dlgEdit = $("#dlgEdit");
        dlgEdit.empty();
        var grpSelect = {};
        var AppendStr = "";
        var identifier = "";
        $.each(data, function (datakey, comp) {
            if (comp.FieldisHide == "Show") {
                if (comp.FieldComponentType == "single") {
                    AppendStr += "<div class='row'>" +
                            "<div class='form-group'>" +
                            "<div class='col-md-3 col-md-offset-1'><label for='txt" + comp.FieldName + "'>" + comp.FieldName + "</label></div>" +
                            "<div class='form-group'><div class='col-md-7'><input style='width:100%' class='retvetxtBox form-control' type='text' id='txt" + comp.FieldName + "'/></div></div>" +
                            "</div></div>";
                    dlgEdit.append(AppendStr);
                    AppendStr = "";
                } else if (comp.FieldComponentType == "select") {
                    var isgrpSelectExist = 0;
                    $.each(grpSelect, function (grpDataKey, grpSelectData) {
                        if (grpSelectData == comp.FieldName) {
                            isgrpSelectExist = 1;
                        }
                    });
                    if (isgrpSelectExist == 0) {
                        AppendStr += "<div class='row'>" +
                            "<div class='form-group'>" +
                            "<div class='col-md-3 col-md-offset-1' ><label for='slc" + comp.FieldComponentID + "'>" + comp.FieldName + "</label></div>";
                        AppendStr += "<div class='form-group'><div class='col-md-7'><select style='width:100%' class='form-control retvetSelect' id='slc" + comp.FieldComponentID + "'><option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option></select></div>" +
                            "</div></div>";
                        grpSelect[comp.FieldName] = comp.FieldName;
                        dlgEdit.append(AppendStr);
                        AppendStr = "";
                    } else {
                        $("#slc" + comp.FieldComponentID).append("<option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option>");
                    }
                }
            }
            identifier = comp.FieldLink;
        });        
        
        var row = tblComponent.row(function (idx, data, node) {
            return data[identifier] === editItem ? true : false;
        }).data();
        
        $.each(data, function (datakey, comp) {            
            //$("#txt" + comp.FieldName).val($("#tbl-component tbody tr:has(\"" + editItem + "\") td:eq(" + comp.FieldLink + ")").text());            
            if (comp.FieldComponentType == "select") {                
                $("#slc" + comp.FieldComponentID + " option").filter(function () {
                    return this.text == row[comp.FieldName];
                }).attr('selected', true);;
            } else {
                $("#txt" + comp.FieldComponentID).val(row[comp.FieldName]);
            }
            
       
        });        
        dlgEdit.dialog({
            title: "Update " + compnent,
            width: '450',
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
                    $(".retvetSelect").each(function (index, element) {
                        UpdateRet += $("#" + element.id + ">option:selected").val() + "|";
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
                            tblComponent.ajax.reload();
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
    event.preventDefault();
    $.get("/DevelopmentTools/FetchLibraryUpdateCompent", {}, function (data) {
        var grpSelect = {};
        dlgDelete = $("#dlgDelete");
        dlgDelete.empty();
        var AppendStr = "";
        $.each(data, function (datakey, comp) {
            if (comp.FieldisHide == "Show") {               
                if (comp.FieldComponentType == "single") {
                    AppendStr += "<div class='row'>" +
                           "<div class='form-group'>" +
                           "<div class='col-md-3 col-md-offset-1'><label for='spn" + comp.FieldComponentID + "'>" + comp.FieldName + "</label></div>" +
                           "<div class='form-group'><div class='col-md-7'><span class='form-control' id='spn" + comp.FieldComponentID + "' /></div></div>" +
                           "</div></div>";
                    dlgDelete.append(AppendStr);
                    AppendStr = "";
                } else if (comp.FieldComponentType == "select") {
                    var isgrpSelectExist = 0;
                    $.each(grpSelect, function (grpDataKey, grpSelectData) {
                        if (grpSelectData == comp.FieldName) {
                            isgrpSelectExist = 1;
                        }
                    });
                    if (isgrpSelectExist == 0) {
                        AppendStr += "<div class='row'>" +
                            "<div class='form-group'>" +
                            "<div class='col-md-3 col-md-offset-1'><label for='spn" + comp.FieldComponentID + "'>" + comp.FieldName + "</label></div>";
                        AppendStr += "<div class='form-group'><div class='col-md-7'><span class='form-control' id='spn" + comp.FieldComponentID + "' /></div></div>" +
                            "</div></div>";
                        grpSelect[comp.FieldName] = comp.FieldName;
                        dlgDelete.append(AppendStr);
                        AppendStr = "";
                    } else {
                        //$("#slc" + comp.FieldComponentID).append("<option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option>");
                    }
                }
            }
            identifier = comp.FieldLink;
        });
        //dlgDelete.append(AppendStr);        

        var row = tblComponent.row(function (idx, data, node) {
            return data[identifier] === delItem ? true : false;
        }).data();

        $.each(data, function (datakey, comp) {            
            $("#spn" + comp.FieldComponentID).text(row[comp.FieldName]);
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
                            tblComponent.ajax.reload();
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