using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LobbyingMadeSimple.Models
{
    public class Issue
    {
        // Constructor
        public Issue()
        {
            VoteCountNeeded = 1500;
            Votes = new List<Vote>();
        }

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
        public int VoteCountNeeded { get; set; }        // Defaults to 1500
        public int UpvoteCount { get { return Votes.Where(v => v.IsUpvote == true).Count(); } }
        public int DownVoteCount { get { return Votes.Where(v => v.IsUpvote == false).Count(); } }
        public bool IsVotableIssue { get { return !HasBeenApproved() && !HasBeenDenied(); } set { } }

        // Associations
        [ScaffoldColumn(false)]
        public string AuthorID { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        /// <summary>
        /// Formats the double percentage to a displayable string percentage 
        /// </summary>
        /// <returns>A string representing the percetange of the issue's upvotes versus total votes </returns>
        public string GetPrettyPercentage()
        {
            string result = Math.Round(GetPercentage() * 100).ToString();
            return result == "NaN" ? "0" : result;
        }

        /// <summary>
        /// Finds the existing vote with a matching UserId if any vote exists in the collection
        /// </summary>
        /// <param name="uid">UserId to match in the search</param>
        /// <returns>The Vote instance found matching the ID from Issue's collection or null if non is found</returns>
        public Vote GetVoteForUser(string uid)
        {
            return Votes.Where(v => v.AuthorID == uid).FirstOrDefault();
        }

        /// <summary>
        /// Gives the percentage of upvotes that an issue has out of all votes
        /// </summary>
        /// <returns>The overall score of upvotes minus downvotes</returns>
        private double GetPercentage()
        {
            return (double)UpvoteCount / TotalVotes();
        }

        /// <summary>
        /// Used to get the total number of votes (both up and down) for an issue
        /// </summary>
        /// <returns>The number of votes for the issue after being totaled</returns>
        public int TotalVotes()
        {
            return UpvoteCount + DownVoteCount;
        }

        /// <summary>
        /// Determines the numer of votes left for the issue before voting will end
        /// </summary>
        /// <returns>The number of votes needed until it can reach approval stage</returns>
        public int VotesLeftUntilApproval()
        {
            return VoteCountNeeded - TotalVotes();
        }

        /// <summary>
        /// Determines if the issue has enouogh votes and upvotes to get approval for funding
        /// </summary>
        /// <returns>true if the issue has enough votes and is upvoted enough to get approval for funding</returns>
        public bool HasBeenApproved()
        {
            return HasEnoughVotes() && HasHighEnoughPercentage();
        }

        /// <summary>
        /// Determines if the issue has enough votes but not enough of a percentage to get approved
        /// </summary>
        /// <returns>true if the issue has enough votes to qualify for approval but not a high enough rating to get approved</returns>
        public bool HasBeenDenied()
        {
            return HasEnoughVotes() && !HasHighEnoughPercentage();
        }

        /// <summary>
        /// Determines if the issue has enough votes for approval based on its vote count needed and its total vote score
        /// </summary>
        /// <returns>true if the issue has enough votes</returns>
        private bool HasEnoughVotes()
        {
            return TotalVotes() >= VoteCountNeeded;
        }

        /// <summary>
        /// Determines if an issue has a high enough percentage of upvotes.
        /// 
        /// Currently that percentage is 2/3 majority (~.667)
        /// </summary>
        /// <returns>true is an issue's vote score is higher than the needed majority</returns>
        private bool HasHighEnoughPercentage()
        {
            return GetPercentage() >= (double)2 / 3;
        }
    }
}