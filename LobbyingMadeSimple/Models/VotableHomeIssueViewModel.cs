using LobbyingMadeSimple.Core;

namespace LobbyingMadeSimple.Web.Models
{
    public class VotableHomeIssueViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApprovalPercentage { get; set; }
        public string ShortDescription { get; set; }
        public string ApprovalPercentageColor { get; set; }
        public string IssueScope { get; set; }
        public int VoteCount { get; set; }

        public static implicit operator VotableHomeIssueViewModel(Issue issue)
        {
            return new VotableHomeIssueViewModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                ApprovalPercentage = issue.GetPrettyPercentage(),
                ShortDescription = issue.ShortDescription, 
                VoteCount = issue.TotalVotes(),
                IssueScope = issue.IsStateIssue ? "State: " + issue.StateAbbrev : "Federal",
                ApprovalPercentageColor = Helpers.HtmlHelpers.GetCssClassForVotePercentage(issue.GetPrettyPercentage())
            };
        }
    }
}