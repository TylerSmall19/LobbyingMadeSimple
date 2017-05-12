using System;
using System.ComponentModel.DataAnnotations;

namespace LobbyingMadeSimple.Core
{
    public class Contribution
    {
        // Properties
        public int ContributionID { get; set; }
        [Required]
        public int IssueID { get; set; }
        [Required]
        public Issue Issue { get; set; }
        [Required]
        public string AuthorID { get; set; }
        [Required]
        public ApplicationUser Author { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}