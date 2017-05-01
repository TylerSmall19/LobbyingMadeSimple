using LobbyingMadeSimple.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IIssueRepository : IDisposable, IRepoBase<Issue>, IEditableBase<Issue>
    {
        List<Issue> GetAllVotableProducts();
    }
}
