var tblAccountingRFP;
$(function () {

    getAccountingRFP();

    $("#selfindRFP").change(function () {
        $('#lblfindRFP').html($('#selfindRFP option:selected').text());
        tblAccountingRFP.ajax.reload();
    });

    $('#tbAccountingRFP tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

});
function getAccountingRFP() {
    tblAccountingRFP = $('#tbAccountingRFP').DataTable({
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
            url: '../Accounting/RetrieveRequestforPayment',
            data: function (d) {
                d.status = document.getElementById("selfindRFP").value;
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
            { data: 'et_description' },
            { data: 'amount' },
            { data: 'prepared_by_name' }
        ]

    });
}