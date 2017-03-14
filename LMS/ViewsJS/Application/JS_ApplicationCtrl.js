
$(function () {

    ///loadComakers($('#AccountNo').val());
    initComakers();
});



function loadComakers(code) {
     jsonReq('../Application/ListComakers', { loanCode: code }, function (data) {
       
         tblBookingCV = $('#tblComakers').DataTable();
         tblBookingCV.clear();
         tblBookingCV.rows.add(data);
         tblBookingCV.draw();
        });
}

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
function initComakers()
{
    $('#tblComakers').DataTable({
        autoWidth: true,
        initComplete: function () {
        },
        searching: false,
        processing: true,
        language: {
            processing: "DataTables is currently busy"
        },
        ajax: {
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: '/Application/ListComakers/' + $('#AccountNo').val(),
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            { data: 'FIRST_NAME' },
            { data: 'MIDDLE_NAME' },
            { data: 'LAST_NAME' },
            { data: 'DATE_OF_BIRTH' },
            { data: 'PHONE_NUMBER' },
            { data: 'ADDRESS' }
        ]

    });
}

function addCollateral()
{
    if ($('#AccountNo').val() != "" && $('#AccountNo').val() != "New" && $('#AccountNo').val() != undefined)
    {

    }else
    {

    }

}

