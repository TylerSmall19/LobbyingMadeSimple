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

    // Remove button colors from each button in the voted issue's div
    $(selector + " input").each(function (i, item) {
        $(item)
            .removeClass("btn-primary")
            .removeClass("btn-danger")
            .removeClass("btn-success");
    });

    // Remove the color from the percentage
    $(selector + " .vote-percentage-string")
        .addClass(getCssClassForVotePercentage(votePercent))
        .removeClass("text-primary");

    if (resp.wasUpvote) {
        // Turn button green
        $(selector + " input.up-vote").addClass("btn btn-success");
    } else {
        // Turn button red
        $(selector + " input.down-vote").addClass("btn btn-danger");
    }

    // Update Vote Total
    $(selector + " .vote-count-display").text(voteCount);
    // Update percentage display
    $(selector + " .vote-percentage").text(votePercent);
}

function getCssClassForVotePercentage(percent) {
    if (percent >= "67") {
        return "text-success";
    } else {
        return "text-danger";
    }
}