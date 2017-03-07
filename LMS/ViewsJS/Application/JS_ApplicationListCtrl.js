$(function () {

    getBookingCVRecords();
    listDocumentStatus();
});

function jsonReq(url, parms, callback, returnType) {

    $.ajax({
        url: url,
        type: "POST",
        dataType: returnType,
        data: parms,
        success: function (data) {
            callback(data);

        }
    });
}



function listDocumentStatus()
{
    jsonReq("Application/ListDocumentStatus", {}, function (data) {
        $("#filter_DocumentStatus").html("");
        $.each(data, function (datakey, comp) {
            $("#filter_DocumentStatus").append("<option value='"+comp.CODE+"' >"+comp.DESCRIPTION+"</option>");
        });

    }, "json");
}



function getBookingCVRecords() {
    tblBookingCV = $('#tbApplicationList').DataTable({
        autoWidth: true,
        initComplete: function () {
        },
        processing: true,
        language: {
            processing: "DataTables is currently busy"
        },
        ajax: {
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: '/Application/ListApplications',
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            { data: 'Status' },
            { data: 'LA_No' },
            { data: 'Date' },
            { data: 'Branch' },
            { data: 'Customer' },
            { data: 'CustomerAddress' },
            { data: 'Product' },
            { data: 'Desired' },
            { data: 'Recommended' },
            { data: 'Approved' },
            { data: 'Set' },
            { data: 'Terms' },
            { data: 'Purpose' },
            { data: 'CCI' },
            { data: 'CiStatus' }
            ]

    });

}