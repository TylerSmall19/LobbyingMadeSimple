$(function () {
    var $currencyFields = $('.currency-field');

    // Add commas when things first get loaded in
    addCommas($currencyFields);
    // Handle subsequent changes to the currency input fields
    $currencyFields.on('input', addCommas.bind(this, $currencyFields));
    $('.currency-form').on('submit', stripCurrencyFieldCommasBeforeSubmission);
});

function addCommas(fields) {
    fields.each(function (i, field) {
        // Strip commas to get around parseInt issues
        var numberString = stripCommas(field.value);
        var newVal = formatNumberToCurrency(numberString);

        field.value = newVal == 'NaN' ? 0 : newVal;
    });
}

function stripCommas(string) {
    return string.replace(/,/g, '');
}

function formatNumberToCurrency(numberString) {
    return new Intl.NumberFormat().format(parseInt(numberString, 10));
}

function stripCurrencyFieldCommasBeforeSubmission(e) {
    $(this)
        .find('.currency-field')
        .each(function (i, field) {
            field.value = field.value.replace(/,/g, '');
        });
}