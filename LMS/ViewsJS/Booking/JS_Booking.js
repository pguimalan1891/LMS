var tblComponent;
var fntblComponent = $("#tbBooking thead")
$(function () {
    getBookingRecords('/Booking/RetrieveBookingRecords');
});

function getBookingRecords(url) {
    var req = $.ajax({
        type: 'post',
        async: true,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'status': 14 })
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
        tblComponent = $("#tbBooking").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "sAjaxDataProp": "",
            "ajax": {
                "url": url,
                "datatype": "json",
                "type": "post",
                //"data": JSON.stringify({ 'status': 14 })
                "data": function (d) {
                    d.status = 14;
                    return d;
                }
            },
            "columns": dataColumns
        });
        tblComponent.columns(0).visible(false, false);
    });
}

/*
function getBookingRecords() {
    tblDirectLoanReceipt = $('#tbBooking').DataTable();
    tblDirectLoanReceipt.destroy();
    tblDirectLoanReceipt = $('#tbBooking').DataTable({
        autoWidth: true,
        initComplete: function () {
        },
        processing: true,
        serverSide:true,
        ajax: {
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: '/Booking/RetrieveBookingRecords',
            dataSrc: function (json) {
                //console.log(json);
                var data = {
                    'data':json
                }
                console.log(data);
                return data;
            },
            columns: [
                { data: 'dsm_description' },
                { data: 'code' }
            ]
        }
    });
}*/