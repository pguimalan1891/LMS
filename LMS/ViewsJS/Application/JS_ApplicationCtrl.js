
$(function () {

    loadComakers();
    loadCollaterals();
});

function loadComakers() {
    var url = 'getComakers';
    jsonReq(url, { loanApplicationNo: "New" }, function (data) {
        $("#co-makers-list").html(data);
    });
}

function loadCollaterals() {
    var url = 'getCollaterals';
        jsonReq(url, { loanApplicationNo: "New" }, function (data) {
            $("#collateral-list").html(data);
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

