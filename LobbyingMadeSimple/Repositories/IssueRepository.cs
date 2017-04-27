using LobbyingMadeSimple.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using LobbyingMadeSimple.Models;

namespace LobbyingMadeSimple.Repositories
{

    public class IssueRepository : IIssueRepository
    {   
        ApplicationDbContext db = new ApplicationDbContext();  
        
        public List<Issue> GetAllVotableProducts()
        {
            return db.Issues.Where(r => r.IsApproved == false).ToList();
        }
    }
}