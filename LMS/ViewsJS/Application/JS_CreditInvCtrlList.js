$(function () {
    
            getBookingCVRecords("31", "");
     
});

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



function listDocumentStatus()
{
    jsonReq("../ListDocumentStatus", {}, function (data) {
        $("#filter_DocumentStatus").html("");
        $.each(data, function (datakey, comp) {
            if (myJsVariable === comp.CODE)
            {
                $("#filter_DocumentStatus").append("<option value='" + comp.CODE + "' selected='selected' >" + comp.DESCRIPTION + "</option>");
            }else
            {
                if (myJsVariable === "[Approval]")
                {
                   
                    if (comp.DESCRIPTION.indexOf(" Approval") !== -1)
                    {
                        $("#filter_DocumentStatus").append("<option value='" + comp.CODE + "' >" + comp.DESCRIPTION + "</option>");
                    }
                   
                } else
                {
                    $("#filter_DocumentStatus").append("<option value='" + comp.CODE + "' >" + comp.DESCRIPTION + "</option>");
                }
              
            }
            
        });

    }, "json");
}


function getBookingCVRecordsWithFilter(status, searchkey) {

    if (searchkey == "" || searchkey === undefined) {
        searchkey = "[All]";
    }


    jsonReq('../ListApplications/' + status + '/' + searchkey, {}, function (newDataArray) {


        tblBookingCV = $('#tbApplicationList').DataTable();
        tblBookingCV.clear();
        tblBookingCV.rows.add(newDataArray);
        tblBookingCV.draw();

    }, "json");


    

    
}


function getBookingCVRecords(status, searchkey) {
    if (searchkey == "" || searchkey === undefined)
    {
        searchkey = "[All]";
    }

    tblBookingCV = $('#tbApplicationList').DataTable({
        autoWidth: true,
        initComplete: function () {
        },
        searching:false,
        processing: true,
        language: {
            processing: "DataTables is currently busy"
        },
        ajax: {
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: 'New/List',
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            { data: 'Status' },
            { data: 'LA_No' },
            { data: 'Date' },
            { data: 'Branch' },
            { data: 'Customer' },
            { data: 'CustomerAddress' },
            { data: 'Product' },
            { data: 'Desired' },
            { data: 'Recommended' },
            { data: 'Approved' },
            { data: 'Set' },
            { data: 'Terms' },
            { data: 'Purpose' },
            { data: 'CCI' },
            { data: 'CiStatus' }
            ]

    });

}

function getCustomerRecords(searchkey) {
 
    tblCustomer = $('#tblCustomerList').DataTable({
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
            url: 'ListBorrowers/a',
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json;
            }
        },
        columns: [
            { data: 'newLoanLink' },
            { data: 'Code' },
            { data: 'FirstName' },
            { data: 'MiddleName' },
            { data: 'LastName' },
            { data: 'DateofBirth' },
            { data: 'Gender' },
            { data: 'CivilStatus' },
            { data: 'City' },
            { data: 'Address' }
           
        ]

    });

}

function getCustomerRecordsWithFilter(searchkey) {


    jsonReq('ListBorrowers/' + searchkey, {}, function (newDataArray) {

        tblBookingCV = $('#tblCustomerList').DataTable();
        tblBookingCV.clear();
        tblBookingCV.rows.add(newDataArray);
        tblBookingCV.draw();

    }, "json");





}



function addComaker()
{

}