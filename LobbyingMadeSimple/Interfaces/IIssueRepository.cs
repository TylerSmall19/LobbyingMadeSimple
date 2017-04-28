using LobbyingMadeSimple.Models;
using System;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IIssueRepository : IDisposable
    {
        List<Issue> GetAllVotableProducts();
        List<Issue> GetAll();
        Issue Find(int? id);
        void Add(Issue issue);
        void Update(Issue issue);
        void Remove(Issue issue);
    }
}
