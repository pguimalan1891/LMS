var tblComponent;
var fntblComponent = $("#tbBooking thead")
$(function () {
    var statuscode = $("#selfindDLR option:first-child").val();
    //$('#lblfindDLR').text($("#selfindDLR option:first-child").text());
    getBookingRecords('/Booking/RetrieveBookingRecords', statuscode);

    $("#selfindDLR").change(function () {
        //getBookingRecords('/Booking/RetrieveBookingRecords', $(this).val());
        //tblComponent.ajax.reload()
        tblComponent.destroy();
        $("#tbBooking thead tr").remove();
        fntblComponent = $("#tbBooking thead")
        $('#lblfindDLR').text($('#selfindDLR option:selected').text());
        getBookingRecords('/Booking/RetrieveBookingRecords', $(this).val());
    });
});

function getBookingRecords(url,statuscode) {
    var req = $.ajax({
        type: 'post',
        async: true,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'status': statuscode })
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
            processing:true,
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
                    d.status = statuscode;
                    return d;
                }
            },
            "columns": dataColumns
        });
        tblComponent.columns(0).visible(false, false);
    });
}