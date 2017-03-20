var tblBookingCCI;
$(function () {
    getBookingCCI();
    $("#selfindCCI").change(function () {
        $('#lblfindCCI').html($('#selfindCCI option:selected').text());
        tblBookingCCI.ajax.reload();
    });
});
function getBookingCCI() {
    tblBookingCCI = $('#tbBookingCCI').DataTable({
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
            url: 'Booking/RetrieveChangeCCIForm',
            data: function (d) {
                d.status = document.getElementById("selfindCCI").value;
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            //{ data: 'Seq' },
            { data: 'dsm_description' },
            { data: 'code' },
            { data: 'datetime_created' },
            { data: 'branch_name' },
            { data: 'la_code' },
            { data: 'applicants_name' },
            { data: 'previous_cci_name' },
            { data: 'current_cci_name' }
        ]

    });
}