using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Date Added")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Last Edited")]
        public DateTime? UpdatedAt { get; set; }
    }
}
