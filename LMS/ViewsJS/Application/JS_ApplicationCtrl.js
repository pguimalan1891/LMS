
$(function () {

    ///loadComakers($('#AccountNo').val());
    initComakers();
    initCollaterals();
});

var tempComaker = {};
var tempCollaterls = {};
var tmpCollObject = {
    'ID': '',
    'LOAN_APPLICATION_ID': '',
    'ColType': '',
    'ColGroup': '',
    'SERIAL_NUMBER': '',
    'DESCRIPTION': '',
    'ColUsage': '',
    'YEAR': '',
    'MODEL': '',
    'Color': '',
    'FuelType': '',
    'CHASSIS_NUMBER': '',
    'ENGINE_NUMBER': '',
    'PLATE_NUMBER': '',
    'TCT_NUMBER': '',
    'ODO_READING': '',
    'CR_NUMBER': '',
    'OR_NUMBER': '',
    'OR_EXPIRATION_DATE': '',
    'INSURANCE_NAME': '',
    'INDSURANCE_EXPIRATION_DATE': '',
    'MLV': '',
    'APPRAISED_VALUE': '',
    'LOAN_VALUE': '',
    'DIRECT_LOAN_RECEIPT_ID': '',
    'ADDITIONAL_INFO': ''
};

var tmpComObject = {
    'ID': '',
    'LOAN_APPLICATION_ID': '',
    'FIRST_NAME': '',
    'MIDDLE_NAME': '',
    'LAST_NAME': '',
    'DATE_OF_BIRTH': '',
    'PHONE_NUMBER': '',
    'ADDRESS': '',
    'NOTES': ''
}

function changeCollateralFields(fldType)
{
    

    if(fldType==="1")
    {
        $('.colRE').hide();
        $('.colApp').hide();
        $('.colCar').show();

    }else if(fldType==="2")
    {
        $('.colRE').hide();
        $('.colCar').hide();
        $('.colApp').show();
    }else if(fldType==="3")
    {
        $('.colApp').hide();
        $('.colCar').hide();
        $('.colRE').show();
    }
}


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
                tempComaker = json;
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

function reloadCollateral()
{
        tmpObj = tmpCollObject;
        tmpObj['MLV'] = 'TEST';
        tempCollaterls.push(tmpObj);
        tblBookingCV = $('#tblCollateral').DataTable();
        tblBookingCV.clear();
        tblBookingCV.rows.add(tempCollaterls);
        tblBookingCV.draw();

}


function reloadCollaterals() {
    tmpObj = tmpCollObject;
    tmpObj['LOAN_APPLICATION_ID'] = $('#AccountNo').val();
    tmpObj['ID'] = 'NEW';
    tempCollaterls.push(tmpObj);
    tblBookingCV = $('#tblCollateral').DataTable();
    tblBookingCV.clear();
    tblBookingCV.rows.add(tempCollaterls);
    tblBookingCV.draw();

}


function reloadComakers() {
    tmpObj = tmpComObject;
    tmpObj['LOAN_APPLICATION_ID'] = $('#AccountNo').val();
    tmpObj['ID'] = 'NEW';
    tmpObj['FIRST_NAME']=$('#comaker_fName').val();
    tmpObj['MIDDLE_NAME'] = $('#comaker_mName').val();
    tmpObj['LAST_NAME'] = $('#comaker_lName').val();
    tmpObj['DATE_OF_BIRTH'] = $('#comaker_dateOfBirth').val();
    tmpObj['PHONE_NUMBER'] = $('#comaker_Phone').val();
    tmpObj['ADDRESS'] = $('#comaker_Address').val();
    tmpObj['NOTES'] = $('#comaker_Notes').val();
    tempComaker.push(tmpObj);
    tblBookingCV = $('#tblComakers').DataTable();
    tblBookingCV.clear();
    tblBookingCV.rows.add(tempComaker);
    tblBookingCV.draw();

}


function initCollaterals() {
    $('#tblCollateral').DataTable({
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
            url: '/Application/ListCollaterals/' + $('#AccountNo').val(),
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                tempCollaterls = json;
                return tempCollaterls;
            }
        },
        columns: [
            { data: 'MLV' },
            { data: 'APPRAISED_VALUE' },
            { data: 'LOAN_VALUE' },
            { data: 'ColGroup' },
            { data: 'ColType' },
            { data: 'DESCRIPTION' },
            { data: 'ColUsage' },
            { data: 'OR_EXPIRATION_DATE' },
            { data: 'ADDITIONAL_INFO' }
        ]

    });
}
