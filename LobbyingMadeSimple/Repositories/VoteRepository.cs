using LobbyingMadeSimple.Interfaces;
using LobbyingMadeSimple.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LobbyingMadeSimple.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private IIssueRepository _issueRepo;
        private ApplicationDbContext _db;

        public VoteRepository(IIssueRepository issueRepo, ApplicationDbContext db)
        {
            _issueRepo = issueRepo;
            _db = db;
        }

        public List<Vote> GetAllVotesForIssue(int issueId)
        {
            return _db.Votes.Where(x => x.IssueID == issueId).ToList();
        }

        public Vote Find(int id)
        {
            return _db.Votes.Find(id);
        }

        public List<Vote> GetAll()
        {
            return _db.Votes.ToList();
        }

        public void Add(Vote vote)
        {
            _db.Votes.Add(vote);

            Issue issue = _issueRepo.Find(vote.IssueID);
            issue.AddVote(vote.IsUpvote);
            _issueRepo.Update(issue);

            _db.SaveChanges();
        }

        public void Update(Vote vote)
        {
            _db.Entry(vote).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(Vote vote)
        {
            _db.Votes.Remove(vote);
            _db.SaveChanges();
        }

        // IDisposal Implementation
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}