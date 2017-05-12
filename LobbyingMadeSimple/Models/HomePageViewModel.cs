using System.Collections.Generic;
using LobbyingMadeSimple.Core;

namespace LobbyingMadeSimple.Models
{
    public class HomePageViewModel
    {
        public List<Issue> VotableIssues { get; set; }
        public List<Issue> FundableIssues { get; set; }
    }
}