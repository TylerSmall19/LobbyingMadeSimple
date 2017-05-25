using LobbyingMadeSimple.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Web.Models
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int VoteCount { get; set; }
        public string IssueScope { get; set; }
        public string ApprovalPercentage { get; set; }
        public double FundingRaised { get; set; }
        public double FundingGoal { get; set; }
        public string UpvoteButtonColor { get; set; }
        public string DownvoteButtonColor { get; set; }
    }

    public static class ExtensionMethods
    {
        public static DetailsViewModel ConvertToDetailsViewModel(this Issue issue, string uid)
        {
            return new DetailsViewModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                ShortDescription = issue.ShortDescription,
                LongDescription = issue.LongDescription, 
                VoteCount = issue.TotalVotes(),
                IssueScope = issue.IsStateIssue ? issue.StateAbbrev : "Federal",
                ApprovalPercentage = issue.GetPrettyPercentage(),
                FundingGoal = issue.FundingGoal,
                FundingRaised = issue.FundingRaised,
                UpvoteButtonColor = Helpers.HtmlHelpers.GetVoteButtonColor(uid, issue, true),
                DownvoteButtonColor = Helpers.HtmlHelpers.GetVoteButtonColor(uid, issue, false)
            };
        }
    }
}