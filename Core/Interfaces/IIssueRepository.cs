using System;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Core.Interfaces
{
    public interface IIssueRepository : IDisposable, IRepoBase<Issue>, IEditableBase<Issue>
    {
        List<Issue> GetAllVotableIssues();
        List<Issue> GetAllVotableIssuesSortedByDate();
        List<Issue> GetAllVotableIssuesSortedByVoteCount();
        List<Issue> GetAllFundableIssues();
        List<Issue> GetAllFundableIssuesSortedByDate();
    }
}
