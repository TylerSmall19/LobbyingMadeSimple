$( bindEventListeners() );

function bindEventListeners() {
    $('#contribute-btn').on('click', validateValue);
}

function validateValue(e) {
    e.preventDefault();
    var $amount = $('#amount');
    // stripCommas defined in currency.js
    var val = stripCommas($amount.val());
    if (val > 0) {
        // Defined in braintree-dropin.js
        addBraintreeDropin();
        $('#contribute-btn').addClass('hidden');
    } else {
        $('#error-catcher').html('<p class="text-info error-text">Sorry, but that is not a valid contribution.</p>');
    }
}