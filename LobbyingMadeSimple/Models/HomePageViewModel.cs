using System.Collections.Generic;

namespace LobbyingMadeSimple.Web.Models
{
    public class HomePageViewModel
    {
        public List<VotableHomeIssueViewModel> VotableIssues { get; set; }
        public List<FundableHomeIssueViewModel> FundableIssues { get; set; }
    }
}