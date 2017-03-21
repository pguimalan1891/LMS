var tblBookingCIR;
$(function () {

    getBookingCIRRecords();

    $("#selfindCIR").change(function () {
        $('#lblfindCIR').html($('#selfindCIR option:selected').text());
        tblBookingCIR.ajax.reload();
    });
});
function getBookingCIRRecords() {
    tblBookingCIR = $('#tbBookingCIR').DataTable({
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
            url: '../Booking/RetrieveCIRForm',
            data: function (d) {
                d.status = document.getElementById("selfindCIR").value;
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            { data: 'dsm_description' },
            { data: 'code' },
            { data: 'datetime_created' },
            { data: 'branch_name' },
            { data: 'loan_application_code' },
            { data: 'loan_type_description' },
            { data: 'loan_terms_description' },
            { data: 'dlr_code' },
            { data: 'bis_dlr_code' },
            { data: 'agent_profile_name' },
            { data: 'commission_amount' }
        ]

    });

}