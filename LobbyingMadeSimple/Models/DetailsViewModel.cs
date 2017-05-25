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
        public string FundingRaised { get; set; }
        public string FundingGoal { get; set; }
        public string UpvoteButtonColor { get; set; }
        public string DownvoteButtonColor { get; set; }
    }
}