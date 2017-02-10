var tblComponent;
var fntblComponent = $("#tbl-customer thead")
$(document).ready(function () {    
    loadComponents("/Customer/FetchCustomerRecord");

    $('.applyDatePicker').datepicker();
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
        
    });
}

function ViewCustomer(code) {
    modalDispCustProf = $("#display-modal-body");
    modalDispCustProfMain = $("#DisplayCustomerModal");
    $.get("Customer/FetchCustomerRecordByID", { "Code": code }, function (data) {
        modalDispCustProf.empty().html(data);
        modalDispCustProfMain.modal();
    });
}