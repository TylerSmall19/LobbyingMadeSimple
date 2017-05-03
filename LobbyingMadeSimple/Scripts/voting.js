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
    var id = resp.issueId;
    var voteCount = resp.neededVotes;
    var selector = '#' + id

    $(selector + " input").each(function (i, item) {
        var $item = $(item);
        $item.removeClass("btn-primary");
        $item.removeClass("btn-danger");
        $item.removeClass("btn-success");
    });

    if (resp.wasUpvote) {
        // Turn button green
        $(selector + " input.up-vote").addClass("btn btn-success");
    } else {
        // Turn button red
        $(selector + " input.down-vote").addClass("btn btn-danger");
    }

    $(selector + " .vote-count-display").text(voteCount);
}