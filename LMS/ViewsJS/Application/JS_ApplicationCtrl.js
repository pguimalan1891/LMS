
$(function () {

    ///loadComakers($('#AccountNo').val());
    initComakers();
    initCollaterals();
    initStaticOthers();
    intiCollType();
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
        $('.col3').hide();
        $('.col2').hide();
        $('.col1').show();
        
    }else if(fldType==="2")
    {
        $('.col1').hide();
        $('.col3').hide();
        $('.col2').show();
    }else if(fldType==="3")
    {
        $('.col1').hide();
        $('.col2').hide();
        $('.col3').show();
    }
}



function intiCollType() {
    jsonReq('../Application/ListCollateralType', { }, function (data) {

        $('#list_coltype').html("");
        $.each(data, function (datakey, comp) {
            $("#list_coltype").append("<option class='col" + comp.COLLATERAL_GROUP_ID + "' value='" + comp.ID + "' >" + comp.DESCRIPTION + "</option>");
        });
        changeCollateralFields('1');
        
    });
    
}

function initStaticOthers() {
    jsonReq('../Application/ListFuelType', {}, function (data) {

        $('#list_fuelType').html("");
        $.each(data, function (datakey, comp) {
            $("#list_fuelType").append("<option value='" + comp.ID + "' >" + comp.DESCRIPTION + "</option>");
        });
     

    });

    jsonReq('../Application/ListColor', {}, function (data) {

        $('#list_color').html("");
        $.each(data, function (datakey, comp) {
            $("#list_color").append("<option value='" + comp.ID + "' >" + comp.DESCRIPTION + "</option>");
        });
      

    });

    jsonReq('../Application/ListCollateralUsage', {}, function (data) {

        $('#list_collateralUsage').html("");
        $.each(data, function (datakey, comp) {
            $("#list_collateralUsage").append("<option   value='" + comp.ID + "' >" + comp.DESCRIPTION + "</option>");
        });
    

    });

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
                url: '../Application/ListComakers/' + $('#AccountNo').val(),
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

    function reloadCollaterals()
    {
        tmpObj = tmpCollObject;
        tmpObj['MLV'] = 'TEST';
        tempCollaterls.push(tmpObj);
        tblBookingCV = $('#tblCollateral').DataTable();
        tblBookingCV.clear();
        tblBookingCV.rows.add(tempCollaterls);
        tblBookingCV.draw();

    }


    function reloadCollateral() {
        var tmpObj = {
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
       
        tmpObj['LOAN_APPLICATION_ID'] = $('#AccountNo').val();
        tmpObj['ID'] = 'NEW';
        tmpObj['ColType'] = getDescFromOption($('#list_coltype').val(),'list_coltype'); //;
        tmpObj['ColGroup'] = getDescFromOption($('#colGroup').val(), 'colGroup');// ;
        tmpObj['SERIAL_NUMBER'] = $('#fld_coll_SERIAL_NUMBER').val();
        tmpObj['DESCRIPTION'] = $('#fld_coll_DESCRIPTION').val();
        tmpObj['ColUsage'] = getDescFromOption($('#list_collateralUsage').val(),'list_collateralUsage');
       tmpObj['YEAR'] = $('#fld_coll_YEAR').val();
       tmpObj['MODEL'] = $('#fld_coll_MODEL').val();
       tmpObj['Color'] = $('#list_color').val();
       tmpObj['FuelType'] = $('#list_fuelType').val();
       tmpObj['CHASSIS_NUMBER'] = $('#fld_coll_CHASSIS_NUMBER').val();
       tmpObj['ENGINE_NUMBER'] = $('#fld_coll_ENGINE_NUMBER').val();
       tmpObj['PLATE_NUMBER'] = $('#fld_coll_PLATE_NUMBER').val();
       tmpObj['TCT_NUMBER'] = $('#fld_coll_TCT_NUMBER').val();
       tmpObj['ODO_READING'] = $('#fld_coll_ODO_READING').val();
       tmpObj['CR_NUMBER'] = $('#fld_coll_CR_NUMBER').val();
       tmpObj['OR_NUMBER'] = $('#fld_coll_OR_NUMBER').val();
       tmpObj['OR_EXPIRATION_DATE'] = $('#fld_coll_OR_EXPIRATION_DATE').val();
       tmpObj['INSURANCE_NAME'] = $('#fld_coll_INSURANCE_NAME').val();
       tmpObj['INSURANCE_EXPIRATION_DATE'] = $('#fld_coll_INSURANCE_EXPIRATION_DATE').val();
       tmpObj['MLV'] = $('#fld_coll_MLV').val();
       tmpObj['APPRAISED_VALUE'] = $('#fld_coll_APPRAISED_VALUE').val();
       tmpObj['LOAN_VALUE'] = $('#fld_coll_LOAN_VALUE').val();
       tmpObj['DIRECT_LOAN_RECEIPT_ID'] = $('#fld_coll_DIRECT_LOAN_RECEIPT_ID').val();
       var addInfo = "";


       if ($('#colGroup').val() === "1")
       {
           addInfo += "YEAR:" + tmpObj['YEAR'] + "<br/>";
           addInfo += "MODEL:" + tmpObj['MODEL'] + "<br/>";
           addInfo += "COLOR:" + getDescFromOption(tmpObj['Color'], 'list_color') + "<br/>";
           addInfo += "FUEL:" + getDescFromOption(tmpObj['FuelType'], 'list_fuelType') + "<br/>";
           addInfo += "CHASSIS NUMBER:" + tmpObj['CHASSIS_NUMBER'] + "<br/>";
           addInfo += "ENGINE NUMBER:" + tmpObj['ENGINE_NUMBER'] + "<br/>";
           addInfo += "ODO READING:" + tmpObj['ODO_READING'] + "<br/>";
           addInfo += "CRE NUMBER:" + tmpObj['CRE_NUMBER'] + "<br/>";
           addInfo += "CR EXPIRY DATE:" + tmpObj['CR_EXPIRATION_DATE'] + "<br/>";
           addInfo += "OR_NUMBER:" + tmpObj['OR_NUMBER'] + "<br/>";
           addInfo += "OR EXPIRY DATE:" + tmpObj['OR_EXPIRATION_DATE'] + "<br/>";
           addInfo += "INSURANCE NAME:" + tmpObj['INSURANCE_NAME'] + "<br/>";
           addInfo += "INSURANCE EXPIRATION_DATE:" + tmpObj['INSURANCE_EXPIRATION_DATE'] + "<br/>";

       } else if ($('#colGroup').val() === "2")
       {
           addInfo += "YEAR:" + tmpObj['YEAR'] + "<br/>";
           addInfo += "MODEL:" + tmpObj['MODEL'] + "<br/>";
           addInfo += "SERIAL NUMBER:" + tmpObj['SERIAL_NUMBER'] + "<br/>";

       } else if ($('#colGroup').val() === "3")
       {
           addInfo += "TCT NUMBER:" + tmpObj['TCT_NUMBER'] + "<br/>";
       }
       tmpObj['ADDITIONAL_INFO'] = addInfo;

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
                url: '../Application/ListCollaterals/' + $('#AccountNo').val(),
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


    function getDescFromOption(key, selectSet)
    {

       var results = {};

       $('#' + selectSet+' option').each(function (i, el) {
           results[el.value] = el.text;
       });

        return results[key];

    }