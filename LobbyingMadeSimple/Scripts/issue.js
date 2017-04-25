$(function () {
    $("#state-issue").on("change", function () {
        toggleDropDown(isStateIssue());
    });
});

function isStateIssue() {
    return $('#state-issue input')[0].checked;
}

function toggleDropDown(isShown) {
    var $dropDown = $("#state-dropdown");

    isShown ?
        $dropDown.removeClass('hidden')
        : $dropDown.addClass('hidden');
}