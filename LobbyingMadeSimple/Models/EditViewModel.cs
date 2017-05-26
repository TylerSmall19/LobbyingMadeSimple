using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LobbyingMadeSimple.Core;
using System.ComponentModel.DataAnnotations;

namespace LobbyingMadeSimple.Web.Models
{
    public class EditViewModel
    {
        public int Id { get; set; }
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

        public static implicit operator EditViewModel(Issue issue)
        {
            return new EditViewModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                ShortDescription = issue.ShortDescription,
                LongDescription = issue.LongDescription
            };
        }
    }

    public static class EditExtensionMethods
    {
        public static void MapFromEditVm(this Issue issue, EditViewModel vm)
        {
            issue.Title = vm.Title;
            issue.ShortDescription = vm.ShortDescription;
            issue.LongDescription = vm.LongDescription;
        }
    }
}