var tblBookingDV;
$(function () {
    getBookingDV();
    $("#selfindDV").change(function () {
        $('#lblfindDV').html($('#selfindDV option:selected').text());
        tblBookingDV.ajax.reload();
    });
});
function getBookingDV() {
    tblBookingDV = $('#tbBookingDV').DataTable({
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
            url: 'Booking/RetrieveDisbursementVoucher',
            data: function (d) {
                d.status = document.getElementById("selfindDV").value;
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
            { data: 'cir_form_code' },
            { data: 'rfp_code' },
            { data: 'et_description' },
            { data: 'amount' },
            { data: 'payee' },
            { data: 'prepared_by_datetime' },
            { data: 'prepared_by_name' }
        ]

    });
}