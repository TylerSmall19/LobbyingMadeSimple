using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LobbyingMadeSimple.Core
{
    public class Vote
    {
            public int VoteID { get; set; }
            [Required]
            public string AuthorID { get; set; }
            [ForeignKey("AuthorID")]
            public virtual ApplicationUser Author { get; set; }

            [Required]
            public int IssueID { get; set; }
            public virtual Issue Issue { get; set; }

            [Required]
            public bool IsUpvote { get; set; }
    }
}
