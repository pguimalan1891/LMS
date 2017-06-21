    
$(document).ready(function () {
    LocateDLR();
    $("#OfficialReceipt_PaymentModeID").on("change", function () {
        if ($("#OfficialReceipt_PaymentModeID>option:selected").text() != "Cash") {
            $("#OfficialReceipt_BankID").removeAttr('disabled');
            $("#OfficialReceipt_CheckNo").removeAttr('disabled');
        } else {
            $("#OfficialReceipt_PaymentModeID").val(0);
            $("#OfficialReceipt_CheckNo").val("");
            $("#OfficialReceipt_BankID").attr('disabled','disabled');
            $("#OfficialReceipt_CheckNo").attr('disabled','disabled');
        }
    });
});


function LocateDLR() {    
    loadComponents();
    $("#DisplayDLRAccounts").modal();
}

function loadComponents() {
    var req = $.ajax({
        type: 'GET',
        async: true,
        url: "OfficialReceipt/FetchDLRActiveAccounts",
        contentType: "application/json; charset=utf-8",
        data: {},
    });

    req.error(function (request, status, error) {
        toastr.error(request.responseText);
    });

    req.done(function (data) {        
        var dataColumns = [];
        var colCount = 0;
        var thead = "<tr>";
        $.each(data, function (datakey, comp) {
            $.each(comp, function (compdatakey, compData) {                
                dataColumns[colCount] = { "data": compdatakey, "autowidth": true };
                thead += "<th>" + compdatakey + "</th>";
                colCount += 1;                
            });
            return false;
        });
        thead += "</tr>";
        $("#tbl-dlrActiveAccts thead").append(thead);
        tblComponent = $("#tbl-dlrActiveAccts").DataTable({
            "pageLength": 10,
            "bFilter": true,
            "sDom": '<"top"l>rt<"bottom"ip><"clear">',
            "bProcessing": true,
            "bServerside": true,
            "responsive": true,
            "aaSorting": [],
            "sAjaxDataProp": "",
            "ajax": {
                "url": "OfficialReceipt/FetchDLRActiveAccounts",
                "datatype": "json",
                "type": "GET"
            },
            "columns": dataColumns
        });
        $("#filter_searchkey").unbind("keyup");
        $("#filter_searchkey").keyup(function (e) {
            var code = e.which;
            if (code == 13) {
                tblComponent.search($(this).val()).draw();
            }
        });
        $("#cmdFilter").unbind("click");
        $("#cmdFilter").click(function () {
            tblComponent.search($("#filter_searchkey").val()).draw();
        });
        $("#tbl-dlrActiveAccts tbody").on("dblclick","tr", function () {
            var drow = this;
            loadOfficialReceiptDetails(drow.cells[1].innerHTML, drow.cells[5].innerHTML, drow.cells[6].innerHTML);
        });
    });
}

function loadOfficialReceiptDetails(dlrNo, laNo, custName) {
    $("#DisplayDLRAccounts").modal("hide");
    $("#OfficialReceipt_LoanAccountNo").val(laNo);
    $("#OfficialReceipt_DirectLoanReceiptNo").val(dlrNo);
    $("#OfficialReceipt_CustomerName").val(custName);
    $.post("OfficialReceipt/getCollectionDues", { "DLRNumber": dlrNo }, function (data) {
        $("#OfficialReceipt_PIPDue").val(data[0].PIPDue);
        $("#OfficialReceipt_GIBCODue").val(data[0].GIBCODue);
        $("#OfficialReceipt_RFCDue").val(data[0].RFCDue);
        $("#OfficialReceipt_DateDue").val(data[0].DueDate);
        $("#OfficialReceipt_AmountDue").val(data[0].TotalAmountDue)
    });
}

function submitOfficialReceipt() {
    var chkr = verifyMoney() + verifyString();
    if (chkr != "") {
        $("#alertModal").modal();
        $("#divAlert").children().remove();
        $("#divAlert").append(chkr);
        return;
    }    
    //$.validator.unobtrusive.parse($("#submitForm"));    
    //if (!$("#submitForm").valid() || verifyMoney() != "") {
    //    $("#alertModal").modal();
    //    return;
    //}
    var OfficialReceiptModel = {};    
    $.each($("#submitForm")[0], function (rowkey, ctrl) {
        OfficialReceiptModel[ctrl.name] = ctrl.value;
    });
    var jsonObject = $.ajax({
        type: 'POST',
        url: 'OfficialReceipt/SubmitOfficialReceipt',
        contentType: 'application/json;charset=utf-8',
        data: "{ ORModel: " + JSON.stringify(OfficialReceiptModel) + " }"
    });
    jsonObject.done(function (data) {
        if (data == 0) {
            toastr.info("Successful Updating.");
            window.location.href = "ORListing";
        } else {
            toastr.error("Updating Failed: Contact Administrator.");            
        }
    });
}

function getTotals() {
    $("#OfficialReceipt_TotalDiscount").val((parseFloat($("#OfficialReceipt_PenaltyWaived").val()) + parseFloat($("#OfficialReceipt_PromptPaymentDiscount").val()) + parseFloat($("#OfficialReceipt_AccelerationDiscount").val())).toFixed(2));
    $("#OfficialReceipt_TotalRFC").val((parseFloat($("#OfficialReceipt_PenaltyWaived").val()) + parseFloat($("#OfficialReceipt_RFC").val()) + parseFloat($("#OfficialReceipt_PromptPaymentDiscount").val()) + parseFloat($("#OfficialReceipt_AccelerationDiscount").val())).toFixed(2));
    $("#OfficialReceipt_PenaltyWaived").val(parseFloat($("#OfficialReceipt_PenaltyWaived").val()).toFixed(2));
    $("#OfficialReceipt_PromptPaymentDiscount").val(parseFloat($("#OfficialReceipt_PromptPaymentDiscount").val()).toFixed(2));
    $("#OfficialReceipt_AccelerationDiscount").val(parseFloat($("#OfficialReceipt_AccelerationDiscount").val()).toFixed(2));
    $("#OfficialReceipt_GIBCO").val(parseFloat($("#OfficialReceipt_GIBCO").val()).toFixed(2));
    $("#OfficialReceipt_RFC").val(parseFloat($("#OfficialReceipt_RFC").val()).toFixed(2));
    $("#OfficialReceipt_PIP").val(parseFloat($("#OfficialReceipt_PIP").val()).toFixed(2));
    $("#OfficialReceipt_AmountReceived").val(parseFloat($("#OfficialReceipt_AmountReceived").val()).toFixed(2));
}