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
    var $optionList = $("#state-dropdown select");

    if (isShown) {
        $dropDown.removeClass('hidden')
    } else {
        $dropDown.addClass('hidden');
        $optionList[0].selectedIndex = 0;
    }
}