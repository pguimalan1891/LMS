var tblBooking;
$(function () {

    getBookingRecords2();

    $("#selfindDLR").change(function () {
        $('#lblfindDLR').html($('#selfindDLR option:selected').text());
        tblBooking.ajax.reload();
    });

    $('#tbBooking tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

});

function getBookingRecords2() {
    tblBooking = $('#tbBooking').DataTable({
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
            url: 'Booking/RetrieveBookingRecords',
            data: function (d) {
                d.status = document.getElementById("selfindDLR").value;
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            //{ data: 'Seq' },
            { data: 'Status' },
            { data: 'LMS DLR No' },
            { data: 'DLR No' },
            { data: 'Date' },
            { data: 'Branch' },
            { data: 'Account No' },
            { data: 'Customer' },
            { data: 'Loan Type' },
            { data: 'Loan Terms' },
            { data: 'Loan Set' },
            { data: 'MLV' }
        ]

    });
}