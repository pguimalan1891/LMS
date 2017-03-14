var tblComponent;
var fntblComponent = $("#tbl-component thead")
var component = "";
$(document).ready(function () {
    component = $("#txtComponentName").val();
    loadComponents("/DevelopmentTools/FetchLibraryComponent", { 'ComponentName': component });
});

function loadComponents(url, ajaxdata) {    
    var req = $.ajax({
        type: 'GET',
        async: true,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: ajaxdata,
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {
        
        var dataColumns = [
            {
                "data": null, "targets": -1, "sortable": false,
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-success btn-minier btn-sm' href='#' title='Edit' onClick='EditComponent(\"" + data.ID + "\")'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>" +
                        "&nbsp;<a class='btn btn-danger btn-minier btn-sm' href='#' title='Delete' onClick='DeleteComponent(\"" + data.ID + "\")'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a>"
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
            "responsive": true,
            "sAjaxDataProp": "",
            "ajax": {
                "url": url,
                "datatype": "json",
                "type": "GET",
                "data": ajaxdata
            },
            "columns": dataColumns
        });
        $.get("/DevelopmentTools/FetchLibraryUpdateComponent", { 'ComponentName': component }, function (data) {
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
    $.get("/DevelopmentTools/FetchLibraryUpdateComponent", { 'ComponentName': component }, function (data) {
        var dlgAdd = $("#dlgadd-modal-body");
        var dlgAddMain = $("#dlgAdd");
        dlgAdd.empty();
        var grpSelect = {};
        var AppendStr = "";
        $.each(data, function (datakey, comp) {
            if (comp.FieldisHide == "Show") {
                if (comp.FieldComponentType == "single") {
                    AppendStr += "<div class='row-fluid'>" +
                           "<div class='form-horizontal'>" +
                           "<div class='form-group'>" +
                           "<label class='col-md-3 control-label' for='txtAdd" + comp.FieldComponentID + "'>" + comp.FieldName + "</label>" +
                           "<div class='col-md-9'><input style='width:100%' class='form-control retveAddtxtBox' type='text' id='txtAdd" + comp.FieldComponentID + "' /></div>" +
                           "</div></div></div>";
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
                        AppendStr += "<div class='row-fluid'>" +
                            "<div class='form-horizontal'>" +
                            "<div class='form-group'>" +
                            "<label class='col-md-3 control-label' for='slcAdd" + comp.FieldComponentID + "'>" + comp.FieldName + "</label>";
                        AppendStr += "<div class='col-md-9'><select style='width:100%' class='form-control retvetAddSelect' id='slcAdd" + comp.FieldComponentID + "'><option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option></select></div>" +
                            "</div></div></div>";
                        grpSelect[comp.FieldName] = comp.FieldName;
                        dlgAdd.append(AppendStr);
                        AppendStr = "";
                    } else {
                        $("#slcAdd" + comp.FieldComponentID).append("<option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option>");
                    }
                }
            }
        });        
        dlgAddMain.modal();
        $("#dlgadd-close-btn").unbind("click");
        $("#dlgadd-close-btn").bind("click", function () {
            dlgAddMain.modal("hide");            
        });
        $("#dlgadd-add-btn").unbind("click");
        $("#dlgadd-add-btn").bind("click", function () {
            var AddRet = "";
            $(".retveAddtxtBox").each(function (index, element) {
                AddRet += $(element).val() + "|"
            });
            $(".retvetAddSelect").each(function (index, element) {
                AddRet += $("#" + element.id + ">option:selected").val() + "|";
            });
            var req = $.ajax({
                type: 'POST',
                async: true,
                url: "/DevelopmentTools/AddComponent",
                contentType: "application/json; charset=utf-8",
                data: "{'ComponentName': '" + component + "','compData': '" + AddRet + "','opCode': 0 }"
            });
            req.error(function (request, status, error) {
                toastr.error(request.responseText);
            });
            req.done(function (data) {
                if (data == "1") {
                    toastr.info("Record Added!");
                    dlgAddMain.modal("hide");
                    tblComponent.ajax.reload();
                } else {
                    toastr.error(data + " SQL Error Number!");
                }
            });
        });
        
    });
}

function EditComponent(editItem) {
    event.preventDefault();
    $.get("/DevelopmentTools/FetchLibraryUpdateComponent", { 'ComponentName': component }, function (data) {
        var dlgEdit = $("#dlgedit-modal-body");
        var dlgEditMain = $("#dlgEdit");
        dlgEdit.empty();
        var grpSelect = {};
        var AppendStr = "";
        var identifier = "";
        $.each(data, function (datakey, comp) {
            if (comp.FieldisHide == "Show") {
                if (comp.FieldComponentType == "single") {
                    AppendStr += "<div class='row-fluid'>" +
                            "<div class='form-horizontal'>" +
                            "<div class='form-group'>" +
                            "<label class='col-md-3 control-label' for='txtEdit" + comp.FieldComponentID + "'>" + comp.FieldName + "</label>" +
                            "<div class='col-md-9'><input style='width:100%' class='retveEdittxtBox form-control' type='text' id='txtEdit" + comp.FieldComponentID + "'/></div>" +
                            "</div></div></div>";
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
                        AppendStr += "<div class='row-fluid'>" +
                            "<div class='form-horizontal'>" +
                            "<div class='form-group'>" +
                            "<label class='col-md-3 control-label' for='slc" + comp.FieldComponentID + "'>" + comp.FieldName + "</label>";
                        AppendStr += "<div class='col-md-9'><select style='width:100%' class='form-control retvetEditSelect' id='slcEdit" + comp.FieldComponentID + "'><option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option></select></div>" +
                            "</div></div></div>";
                        grpSelect[comp.FieldName] = comp.FieldName;
                        dlgEdit.append(AppendStr);
                        AppendStr = "";
                    } else {
                        $("#slcEdit" + comp.FieldComponentID).append("<option value='" + comp.FieldValue + "'>" + comp.FieldDisplay + "</option>");
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
                $("#slcEdit" + comp.FieldComponentID + " option").filter(function () {
                    return this.text == row[comp.FieldName];
                }).attr('selected', true);;
            } else {
                $("#txtEdit" + comp.FieldComponentID).val(row[comp.FieldName]);
            }
            
       
        });
        dlgEditMain.modal();
        $("#dlgEdit-close-btn").unbind("click");
        $("#dlgEdit-close-btn").bind("click", function () {
            dlgEditMain.modal("hide");
        });
        $("#dlgEdit-update-btn").unbind("click");
        $("#dlgEdit-update-btn").bind("click", function () {
            var UpdateRet = editItem + "|";
            $(".retveEdittxtBox").each(function (index, element) {
                UpdateRet += $(element).val() + "|"
            });
            $(".retvetEditSelect").each(function (index, element) {
                UpdateRet += $("#" + element.id + ">option:selected").val() + "|";
            });
            var req = $.ajax({
                type: 'POST',
                async: true,
                url: "/DevelopmentTools/UpdateComponent",
                contentType: "application/json; charset=utf-8",
                data: "{'ComponentName': '" + component + "','compData': '" + UpdateRet + "','opCode': 1 }"
            });
            req.error(function (request, status, error) {
                toastr.error(request.responseText);
            });
            req.done(function (data) {
                if (data == "1") {
                    toastr.info("Record Updated!");
                    dlgEditMain.modal("hide");
                    tblComponent.ajax.reload();
                } else {
                    toastr.error(data + " SQL Error Number!");
                }
            });
        });       
    });
}

function DeleteComponent(delItem) {
    event.preventDefault();
    $.get("/DevelopmentTools/FetchLibraryUpdateComponent", { 'ComponentName': component }, function (data) {
        var grpSelect = {};
        var dlgDelete = $("#dlgdelete-modal-body");
        var dlgDeleteMain = $("#dlgDelete");
        dlgDelete.empty();
        var AppendStr = "";
        $.each(data, function (datakey, comp) {
            if (comp.FieldisHide == "Show") {               
                if (comp.FieldComponentType == "single") {
                    AppendStr += "<div class='row-fluid'>" +
                           "<div class='form-horizontal'>" +
                           "<div class='form-group'>" +
                           "<label class='col-md-3 control-label' for='spnDelete" + comp.FieldComponentID + "'>" + comp.FieldName + "</label>" +
                           "<div class='col-md-9'><span class='form-control' id='spnDelete" + comp.FieldComponentID + "' /></div>" +
                           "</div></div></div>";
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
                        AppendStr += "<div class='row-fluid'>" +
                            "<div class='form-horizontal'>" +
                            "<div class='form-group'>" +
                            "<label class='col-md-3 control-label' for='spnDelete" + comp.FieldComponentID + "'>" + comp.FieldName + "</label>";
                        AppendStr += "<div class='col-md-9'><span class='form-control' id='spnDelete" + comp.FieldComponentID + "' /></div>" +
                            "</div></div></div>";
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
        var row = tblComponent.row(function (idx, data, node) {
            return data[identifier] === delItem ? true : false;
        }).data();

        $.each(data, function (datakey, comp) {            
            $("#spnDelete" + comp.FieldComponentID).text(row[comp.FieldName]);
        });
        dlgDeleteMain.modal();
        $("#dlgDelete-close-btn").unbind("click");
        $("#dlgDelete-close-btn").bind("click", function () {
            dlgDeleteMain.modal("hide");
        });
        $("#dlgDelete-delete-btn").unbind("click");
        $("#dlgDelete-delete-btn").bind("click", function () {
            var DeleteRet = delItem + "|";
            var req = $.ajax({
                type: 'POST',
                async: true,
                url: "/DevelopmentTools/DeleteComponent",
                contentType: "application/json; charset=utf-8",
                data: "{'ComponentName': '" + component + "','compData': '" + DeleteRet + "','opCode': 2 }"
            });
            req.error(function (request, status, error) {
                toastr.error(request.responseText);
            });
            req.done(function (data) {
                if (data == "1") {
                    toastr.info("Record Deleted!");
                    dlgDeleteMain.modal("hide");
                    tblComponent.ajax.reload();
                } else {
                    toastr.error(data + " SQL Error Number!");
                }
            });
        });
       
    });
}