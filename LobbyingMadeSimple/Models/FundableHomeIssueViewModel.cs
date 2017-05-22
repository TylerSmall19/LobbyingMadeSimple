using LobbyingMadeSimple.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Web.Models
{
    public class FundableHomeIssueViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }

        public static implicit operator FundableHomeIssueViewModel(Issue issue)
        {
            return new FundableHomeIssueViewModel()
            {
                Id = issue.Id,
                Title = issue.Title,
                ShortDescription = issue.ShortDescription
            };
        }
    }
}