$(function () {
    $('.vote-btn').on("submit", vote);
});
    
function vote(e) {
    e.preventDefault();

    var action = this.action;
    var data = $(this).serialize();

    $.post(action, data, voteSuccess);
}

function voteSuccess(resp) {
    // Capture the variables needed for selection
    var id = resp.issueId;
    var voteCount = resp.neededVotes;
    var selector = '#' + id;
    var votePercent = resp.votePercent;

    console.log(resp.votePercentageCssClass);

    // Remove button colors from each button in the voted issue's div
    $(selector + " input").each(function (i, item) {
        $(item)
            .removeClass("btn-primary")
            .removeClass("btn-danger")
            .removeClass("btn-success");
    });

    // Remove the color from the percentage
    $(selector + " .vote-percentage-string")
        .addClass(resp.votePercentageCssClass)
        .removeClass("text-primary");

    if (resp.wasUpvote) {
        // Turn button green
        $(selector + " input.up-vote").addClass("btn-success");
    } else {
        // Turn button red
        $(selector + " input.down-vote").addClass("btn-danger");
    }

    // Update Vote Total
    $(selector + " .vote-count-display").text(voteCount);
    // Update percentage display
    $(selector + " .vote-percentage").text(votePercent);
}