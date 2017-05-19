using LobbyingMadeSimple.Core;
using LobbyingMadeSimple.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Web.Models
{
    public class IssueViewModel
    {
        public IssueViewModel()
        {
            FundingGoal = 50000.00;
        }

        // Properties
        [Required]
        [MaxLength(60, ErrorMessage = "Must be fewer than 60 characters")]
        public string Title { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Must be fewer than 150 characters")]
        [Display(Name = "Summary")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Details")]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "This issue is State-Level (Leave blank if Federal)")]
        public bool IsStateIssue { get; set; }

        [Display(Name = "Affected State")]
        public string StateAbbrev { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Lobbying Amount Needed")]
        public double FundingGoal { get; set; } // Defaults to 50000.00

        // Non-editable properties
        public int Id { get; set; }
        public int VotesLeftUntilApproval { get; set; }
        public string ApprovalPercentage { get; set; }
        public string UpvoteButtonColor { get; set; }
        public string DownvoteButtonColor { get; set; }

        // Conversion operators
        public static implicit operator IssueViewModel(Issue issue)
        {
            return new IssueViewModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                ShortDescription = issue.ShortDescription,
                LongDescription = issue.LongDescription,
                IsStateIssue = issue.IsStateIssue,
                StateAbbrev = issue.StateAbbrev,
                FundingGoal = issue.FundingGoal,
                VotesLeftUntilApproval = issue.VotesLeftUntilApproval(),
                ApprovalPercentage = issue.GetPrettyPercentage(),
                UpvoteButtonColor = HtmlHelpers.GetVoteButtonColor(issue.Author.Id, issue, true),
                DownvoteButtonColor = HtmlHelpers.GetVoteButtonColor(issue.Author.Id, issue, false)
            };
        }

        public static implicit operator Issue(IssueViewModel vm)
        {
            var issue = new Issue()
            {
                Title = vm.Title,
                ShortDescription = vm.ShortDescription,
                LongDescription = vm.LongDescription,
                IsStateIssue = vm.IsStateIssue,
                FundingGoal = vm.FundingGoal
            };

            if (vm.IsStateIssue)
            {
                issue.StateAbbrev = vm.StateAbbrev;
            }
            return issue;
        }
    }
}