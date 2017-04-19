var tblComponent;
$(document).ready(function () {
    loadComponents();
});

function loadComponents() {
    var req = $.ajax({
        type: 'GET',
        async: true,
        url: "Home/GetCustomerList",
        contentType: "application/json; charset=utf-8",
        data: {},
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {
        var dataColumns = [];
        var colCount = 0;
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {
                if (compdatakey == "Code") {
                    dataColumns[colCount] = {
                        "data": compdatakey, "autowidth": true, "render": function (data, type, row, meta) {
                            return "<div class='text-navy'><a href='#' onclick='window.location=\"Application/NewLoanApplication?borrower=" + data + "\"'><i class='fa fa-pencil-square-o' aria-hidden='true'></i>New Loan</a> | <a href='#'><i class='fa fa-bars' aria-hidden='true'></i> List of Transactions</a></div>";
                        }
                    };
                } else if (compdatakey == "FullName") {
                    dataColumns[colCount] = { "data": compdatakey, "autowidth": true };
                }                
                colCount += 1;
            });
            return false;
        });
        tblComponent = $("#tblEasyCustSearch").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "sDom": '',
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "aaSorting": [],
            "sAjaxDataProp": "",
            "ajax": {
                "url": "Home/GetCustomerList",
                "datatype": "json",
                "type": "GET"
            },
            "columns": dataColumns
        });        
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
