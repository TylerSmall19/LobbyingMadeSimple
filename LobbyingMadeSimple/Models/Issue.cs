using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LobbyingMadeSimple.Models
{
    public class Issue
    {
        // Constructor
        public Issue()
        {
            IsApprovedForFunding = false;
            VoteCountNeeded = 1500;
            UpvoteCount     = 0;
            DownVoteCount   = 0;
        }

        public bool IsApproved { get { return IsApprovedForFunding; } set { } }

        // Properties
        public int IssueID { get; set; }

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

        // Non-User-Input properties
        private bool IsApprovedForFunding { get; set; } // Defaults to false
        public int VoteCountNeeded { get; set; }        // Defaults to 1500
        public int UpvoteCount { get; set; }            // Defaults to 0
        public int DownVoteCount { get; set; }          // Defaults to 0

        // Associations
        [ScaffoldColumn(false)]
        public string AuthorID { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        // Methods
        // <summary>
        // Adds to vote count for current issue.
        // This should save a query to the DB after an issue
        // is found.
        // </summary>
        public virtual void AddVote(bool VoteWeight)
        {
            if (VoteWeight)
            {
                UpvoteCount++;
            }
            else
            {
                DownVoteCount++;
            }

            UpdateIfApproved();
        }

        public int NetScore()
        {
            return UpvoteCount - DownVoteCount;
        }

        public int TotalVotes()
        {
            return UpvoteCount + DownVoteCount;
        }

        public int VotesLeftUntilApproval()
        {
            return VoteCountNeeded - TotalVotes();
        }

        private void UpdateIfApproved()
        {
            if (HasEnoughVotes() && HasEnoughUpvotes())
            {
                IsApprovedForFunding = true;
            }
        }

        private bool HasEnoughVotes()
        {
            return UpvoteCount + DownVoteCount >= VoteCountNeeded;
        }

        private bool HasEnoughUpvotes()
        {
            return HasHighEnoughPercentage();
        }

        private bool HasHighEnoughPercentage()
        {
            int totalVotes = UpvoteCount + DownVoteCount;
            return (double)UpvoteCount / totalVotes >= (double)2 / 3;
        }
    }
}