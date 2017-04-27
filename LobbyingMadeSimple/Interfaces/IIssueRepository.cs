using LobbyingMadeSimple.Models;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IIssueRepository
    {
        List<Issue> GetAllVotableProducts();
    }
}
