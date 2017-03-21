var tblBookingCV;
$(function () {

    getBookingCVRecords();
    
    $("#selfindCV").change(function () {
        $('#lblfindCV').html($('#selfindCV option:selected').text());
        tblBookingCV.ajax.reload();
    });
});

function getBookingCVRecords() {
    tblBookingCV = $('#tbBookingCV').DataTable({
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
            url: '../Booking/RetrieveCheckVoucher',
            data: function (d) {
                d.status = document.getElementById("selfindCV").value;
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
            { data: 'dlr_code' },
            { data: 'bis_dlr_code' },
            { data: 'cvt_description' },
            { data: 'bat_description' },
            { data: 'bank_description' },
            { data: 'check_no' },
            { data: 'amount' },
            { data: 'applicants_name' },
            { data: 'payee' },
            { data: 'check_date' }
        ]

    });

}