using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Models
{
    public class Issue
    {
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
        [Display(Name = "This issue is State-Level")]
        public bool IsStateIssue { get; set; }
        [Display(Name = "Affected State")]
        public string StateAbbrev { get; set; }

        // Associations
        [ScaffoldColumn(false)]
        public string AuthorID { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}