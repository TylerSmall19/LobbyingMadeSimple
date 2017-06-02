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

            instance.requestPaymentMethod(function (err, payload) {
                // Submit payload.nonce to your server
                if (err) {
                    //console.log(err);
                    return;
                }

                //console.log(payload);
            });
            e.preventDefault();
        });
    });
}