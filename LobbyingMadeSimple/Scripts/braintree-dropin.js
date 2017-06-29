function addBraintreeDropin() {
    braintree.dropin.create({
        authorization: 'sandbox_ntvtsgtk_zqx4hcxrt6yqn7qt',
        container: '#dropin-container'
    }, function (createErr, instance) {
        if (createErr) {
            //console.error(createErr);
            return;
        }

        $('#submit-button').removeClass('hidden');
        $('#submit-button').on('click', function (e) {
            e.preventDefault();

            instance.requestPaymentMethod(function (err, payload) {
                // Submit payload.nonce to your server
                if (err) {
                    //console.log(err);
                    return;
                }
                // stripCommas from currency.js
                var amount = parseInt(stripCommas($('#amount').val()), 10);;
                $('#dropin-container').prepend('<span id="checkout-status">We\'re processing your payment. Hold tight!</span>');
                $.post(window.location.href, { nonce: payload.nonce, amount: amount }, function (resp) { console.log(resp); });
                $('#submit-button').hide();
            });
        });
    });
}