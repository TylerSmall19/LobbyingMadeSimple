using LobbyingMadeSimple.Models;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IVoteRepository : IRepoBase<Vote>, IEditableBase<Vote>
    {
        List<Vote> GetAllVotesForIssue(int issueId);
    }
}
