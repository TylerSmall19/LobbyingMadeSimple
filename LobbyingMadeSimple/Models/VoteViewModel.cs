using LobbyingMadeSimple.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Web.Models
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int VotesLeftUntilApproval { get; set; }
        public string ApprovalPercentage { get; set; }
        public string ApprovalPercentageColor { get; set; }
        public string UpvoteButtonColor { get; set; }
        public string DownvoteButtonColor { get; set; }
    }

    public static class IssueExtensionMethods
    {
        public static VoteViewModel ConvertToVoteViewModel(this Issue issue, string Uid)
        {
            return new VoteViewModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                ShortDescription = issue.ShortDescription,
                VotesLeftUntilApproval = issue.VotesLeftUntilApproval(),
                ApprovalPercentage = issue.GetPrettyPercentage(),
                ApprovalPercentageColor = Helpers.HtmlHelpers.GetCssClassForVotePercentage(issue.GetPrettyPercentage()),
                DownvoteButtonColor = Helpers.HtmlHelpers.GetVoteButtonColor(Uid, issue, false),
                UpvoteButtonColor = Helpers.HtmlHelpers.GetVoteButtonColor(Uid, issue, true)
            };
        }
    }
}