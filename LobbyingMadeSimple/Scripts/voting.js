$(function () {
    $('.vote-btn').on("submit", vote);
});
    
function vote(e) {
    e.preventDefault();

    var action = this.action;
    var data = $(this).serialize();

    $.post(action, data)
        .done(voteSuccess)
        .fail(voteFailed);
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
            .removeClass("btn-primary btn-danger btn-success");
    });

    // Remove the color from the percentage
    $(selector + " .vote-percentage-string")
        .removeClass("text-success text-danger")
        .addClass(resp.votePercentageCssClass);

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

function voteFailed(xhr) {
    var resp = xhr.responseJSON;

    if (!resp.isVotable) {
        $('#' + resp.issueId).remove();
    }
}