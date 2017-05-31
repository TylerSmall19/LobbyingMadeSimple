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
    var voteCount = resp.votes;
    var selector = '#' + id;
    var votePercent = resp.votePercent;

    // Remove button colors from each button in the voted issue's div
    removeVoteButtonColors(selector + " .vote-btn");

    // Remove the color from the percentage (Chainable)
    changePercentageColors(selector + " .vote-percentage-string", resp.votePercentageCssClass);

    // Color the button that was clicked
    colorVoteButton(resp.wasUpvote, selector);

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

// Helper functions
function removeVoteButtonColors(selector) {
    $(selector).each(function (i, item) {
        $(item)
            .removeClass("btn-primary btn-danger btn-success");
    });
}

function removePercentageColors(selector) {
    var $sel = $(selector);
    $sel.removeClass("text-success text-failure");
    return $sel;
}

function changePercentageColors(selector, color) {
    removePercentageColors(selector).addClass(color);
}

function colorVoteButton(wasUpvote, selector) {
    if (wasUpvote) {
        // Turn upvote button green
        $(selector + " input.up-vote").addClass("btn-success");
    } else {
        // Turn downvote button red
        $(selector + " input.down-vote").addClass("btn-danger");
    }
}