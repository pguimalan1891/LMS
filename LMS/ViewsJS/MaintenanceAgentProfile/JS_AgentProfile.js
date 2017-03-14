var tblComponent;
var fntblComponent = $("#tbl-agent thead")
$(document).ready(function () {
    loadComponents("/MaintenanceAgentProfile/getAgentProfileList");
    $('.applyDatePicker').datepicker();
    if ($("#AGENTCode").val() != "") {
        modalDispAgentProf = $("#display-modal-body");
        modalDispCustProfMain = $("#DisplayAgentModal");
        $.get("MaintenanceAgentProfile/FetchAgentProfileByCode", { "Code": $("#AGENTCode").val() }, function (data) {
            modalDispAgentProf.empty().html(data);
            modalDispCustProfMain.modal();
        });
    }
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

        var thead = "<tr>"
        var colCount = 0;
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                if (compdatakey == "AgentCode") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<a onclick='ViewAgent(\"" + data + "\")'>" + data + "</a>";
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
        tblComponent = $("#tbl-agent").DataTable({
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
    });
}

function ViewAgent(code) {    
    modalDispAgentProf = $("#display-modal-body");
    modalDispCustProfMain = $("#DisplayAgentModal");
    $.get("MaintenanceAgentProfile/FetchAgentProfileByCode", { "Code": code }, function (data) {
        modalDispAgentProf.empty().html(data);
        modalDispCustProfMain.modal();
    });
}

function AddAgent() {
    window.location.href = '/AgentProfileAdd';
}

