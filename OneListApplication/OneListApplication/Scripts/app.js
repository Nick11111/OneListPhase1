// When client is not on same domain.
var serviceUrl = 'http://takethisapart.com/onelist/api/coupons';
function getCoupons() {
    $("#coupons").replaceWith("<span id='value1'></span>");
    var method = $('#method').val();
    $.ajax({
        type: method,
        url: serviceUrl
    }).done(function (data) {
        data.forEach(function (val) {
            callback(val)
        });
    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#value1').text(jqXHR.responseText || textStatus);
    });
}

function callback(val) {
    //  $("#manufacturers").replaceWith("<span id='value1'>(Result)</span>");
    $("#value1").replaceWith("<ul id='coupons' />");
    var couponID = val.CouponID;
    $('<div id="cup'+couponID+'"/>').appendTo($('#coupons'));
    var str = "Title3: <br>" + val.Title + "</br> Description: " + val.Description + " Discount Percentage: " + val.DiscountPercentage + " Start Date: " + val.StartDate + " Ending Date: " + val.EndingDate;
    var Title=val.Title;
    var Description=val.Description;
    var discount=val.DiscountPercentage;
    var initialDate = val.StartDate;
    var i = new Date(initialDate);
    var begintring = "Starting Date: "+ i.getDate() + "-" + (i.getMonth() + 1) + "-" + i.getFullYear();
    var expireDate = val.EndingDate;
    var d = new Date(expireDate);
    var datestring = "Ending Date: " + d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
    $('<b/>', { text: Title }).appendTo($('#cup' + couponID + ''));
    $('<p/>', { text: Description }).appendTo($('#cup' + couponID + ''));
    $('<p/>', { text: begintring }).appendTo($('#cup' + couponID + ''));
    $('<p/>', { text: datestring }).appendTo($('#cup' + couponID + ''));
    //$('<hr/>').appendTo($('#coupons'));

}

// Deletes and refreshes list.
function updateList() {
    $("#coupons").replaceWith("<span id='value1'>(Result)<br /></span>");
    sendRequest();
}
// Add a new coupon.
function create() {
    jQuery.support.cors = true;
    var coupon = {
        CouponID: 0,
        Title: $('#txtAdd_Title').val(),
        Description: $('#txtAdd_Description').val(),
        DiscountPercentage: $('#txtAdd_DiscountPercentage').val(),
        RetailID: $('#txtAdd_RetailID').val(),
        StartDate: $('#txtAdd_StartDate').val(),
        EndingDate: $('#txtAdd_EndingDate').val()
    };
    var cr = JSON.stringify(coupon);
    $.ajax({
        url: serviceUrl,
        type: 'POST',
        data: JSON.stringify(coupon),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#couponCreate')
                .text('Coupon successfully created.');
            updateList();
        },
        error: function (_httpRequest, _status, _httpError) {
            // XMLHttpRequest, textStatus, errorThrow
            $('#couponCreate')
            .text('Error while adding Coupon.  XMLHttpRequest:'
                    + _httpRequest + '  Status: ' + _status
                    + '  Http Error: ' + _httpError);
        }
    });
}
function update() {
    jQuery.support.cors = true;
    var coupon = {
            CouponID: $('#txtUp_CouponID').val(),
            Title: $('#txtUp_Title').val(),
            Description: $('#txtUp_Description').val(),
            DiscountPercentage: $('#txtUp_DiscountPercentage').val(),
            RetailID: $('#txtUp_RetailID').val(),
            StartDate: $('#txtUp_StartDate').val(),
            EndingDate: $('#txtUp_EndingDate').val()
    };

    var cr = JSON.stringify(coupon);
    $.ajax({

        url: serviceUrl + "/" + $('#txtUp_CouponID').val(),
        type: 'PUT',
        data: JSON.stringify(coupon),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#couponUpdate')
            .text('The update was successful.');
            updateList();
        },
        error: function (_httpRequest, _status, _httpError) {
            $('#coupUpdate')
            .text('Error while updating coupon.  XMLHttpRequest:'
            + _httpRequest + '  Status: ' + _status + '  Http Error: '
            + _httpError);
        }
    });
}
function del() {
    var id = $('#txtDelete_CouponID').val();
    $.ajax({
        url: serviceUrl + "/" + id,
        type: 'DELETE',
        dataType: 'json',

        success: function (data) {
            $('#couponDelete').text('Delete successful.');
            updateList();
        }
    }).fail(
        function (jqueryHeaderRequest, textStatus, err) {
            $('#couponDelete').text('Delete error: ' + err);
        });
}

