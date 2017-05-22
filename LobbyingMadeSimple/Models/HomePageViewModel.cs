using System.Collections.Generic;
using LobbyingMadeSimple.Web.Models;

namespace LobbyingMadeSimple.Models
{
    public class HomePageViewModel
    {
        public List<VotableHomeIssueViewModel> VotableIssues { get; set; }
        public List<FundableHomeIssueViewModel> FundableIssues { get; set; }
    }
}