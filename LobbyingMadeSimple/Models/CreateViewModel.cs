using LobbyingMadeSimple.Core;
using System.ComponentModel.DataAnnotations;

namespace LobbyingMadeSimple.Web.Models
{
    public class CreateViewModel
    {
        public CreateViewModel()
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

        public static implicit operator Issue(CreateViewModel vm)
        {
            var issue = new Issue()
            {
                Title = vm.Title,
                ShortDescription = vm.ShortDescription,
                LongDescription = vm.LongDescription,
                IsStateIssue = vm.IsStateIssue,
                FundingGoal = vm.FundingGoal,
            };

            if (vm.IsStateIssue)
            {
                issue.StateAbbrev = vm.StateAbbrev;
            }
            return issue;
        }
    }
}