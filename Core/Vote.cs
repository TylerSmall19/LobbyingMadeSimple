using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
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
