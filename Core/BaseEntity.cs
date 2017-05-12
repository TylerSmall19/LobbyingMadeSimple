using System;
using System.ComponentModel.DataAnnotations;

namespace LobbyingMadeSimple.Core
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
