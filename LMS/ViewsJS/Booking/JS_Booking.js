var tblBooking;
$(function () {

    getBookingRecords2();

    $("#selfindDLR").change(function () {
        $('#lblfindDLR').html($('#selfindDLR option:selected').text());
        tblBooking.ajax.reload();
    });

    $('body').on('click', '.lmsdlrdetails', function () {
        //alert($(this).text());
        //$("#mdlDLR").modal();
        window.location.href = "../Booking/DLRbyLMSNo?lmsno=" + $(this).text();
    });
});

function getBookingRecords2() {
    tblBooking = $('#tbBooking').DataTable({
        autoWidth: true,
        initComplete: function () {
        },
        select: {
            style: 'multi'
        },
        processing: true,
        language: {
            processing: "DataTables is currently busy"
        },
        ajax: {
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: '../Booking/RetrieveBookingRecords',
            data: function (d) {
                d.status = document.getElementById("selfindDLR").value;
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columnDefs: [ {
            targets: 1,
            //data: 'LMS DLR No',
            render: function ( data, type, full, meta ) {
                return '<a class="lmsdlrdetails">' + data + '</a>';
            }
        }],
        columns: [
            //{ data: 'Seq' },
            { data: 'Status' },
            { data: 'LMS DLR No'},
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

    $('#tbBooking tbody').on('click', 'tr', function () {
        $(this).toggleClass('active');
    });

}