using System.Collections.Generic;
using LobbyingMadeSimple.Core;
using LobbyingMadeSimple.Web.Models;

namespace LobbyingMadeSimple.Models
{
    public class HomePageViewModel
    {
        public List<VotableHomeIssueViewModel> VotableIssues { get; set; }
        public List<Issue> FundableIssues { get; set; }
    }
}