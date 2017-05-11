using LobbyingMadeSimple.Models;
using System.Collections.Generic;
using Core;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IVoteRepository : IRepoBase<Vote>, IEditableBase<Vote>
    {
        List<Vote> GetAllVotesForIssue(int issueId);
    }
}
