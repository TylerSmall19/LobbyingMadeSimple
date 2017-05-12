using System.Collections.Generic;
using Core;

namespace LobbyingMadeSimple.Core.Interfaces
{
    public interface IVoteRepository : IRepoBase<Vote>, IEditableBase<Vote>
    {
        List<Vote> GetAllVotesForIssue(int issueId);
    }
}
