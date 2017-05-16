using LobbyingMadeSimple.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Models
{
    public class FundViewModel
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Title { get; set; }
        public double FundingGoal { get; set; }
        public double FundingRaised { get; set; }

        public string GetPrettyFundingPercentage()
        {
            return Math.Round(((FundingRaised / FundingGoal) * 100)).ToString();
        }

        public static implicit operator FundViewModel(Issue issue)
        {
            return new FundViewModel()
            {
                Id = issue.Id,
                ShortDescription = issue.ShortDescription,
                Title = issue.Title,
                FundingGoal = issue.FundingGoal,
                FundingRaised = issue.FundingRaised
            };
        }
    }
}