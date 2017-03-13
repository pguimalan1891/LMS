var tblComponent;
var fntblComponent = $("#tbl-customer thead")
$(document).ready(function () {    
    loadComponents("/Customer/FetchCustomerRecord");
    $('.applyDatePicker').datepicker();
    if ($("#CustomerCode").val() != "") {
        modalDispCustProf = $("#display-modal-body");
        modalDispCustProfMain = $("#DisplayCustomerModal");
        $.get("Customer/FetchCustomerRecordByID", { "Code": $("#CustomerCode").val() }, function (data) {
            modalDispCustProf.empty().html(data);
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
                if (compdatakey == "Code") {
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
        tblComponent = $("#tbl-customer").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "sDom": '<"top"l>rt<"bottom"ip><"clear">',
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,          
            "aaSorting": [],
            "sAjaxDataProp": "",            
            "ajax": {
                "url": url,
                "datatype": "json",
                "type": "GET"
            },            
            "columns": dataColumns
        });
        tblComponent.columns(0).visible(false, false);
        $("#filter_searchkey").unbind("keyup");
        $("#filter_searchkey").keyup(function (e) {
            var code = e.which;
            if (code == 13) {
                tblComponent.search($(this).val()).draw();
            }
        });
        $("#cmdFilter").unbind("click");
        $("#cmdFilter").click(function () {            
            tblComponent.search($("#filter_searchkey").val()).draw();
        });
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

function AddCustomer() {
    window.location.href = '/CustomerAdd';
}
