using LobbyingMadeSimple.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using LobbyingMadeSimple.Models;
using System.Data.Entity;

namespace LobbyingMadeSimple.Repositories
{

    public class IssueRepository : IIssueRepository
    {   
        ApplicationDbContext _db;

        public IssueRepository()
        {
            _db = new ApplicationDbContext();
        }

        public IssueRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Issue issue)
        {
            issue.CreatedAt = DateTime.UtcNow;
            _db.Issues.Add(issue);
            _db.SaveChanges();
        }

        public Issue Find(int id)
        {
            return _db.Issues.Find(id);
        }

        public List<Issue> GetAll()
        {
            return _db.Issues.ToList();
        }

        public virtual List<Issue> GetAllVotableIssues()
        {
            return _db.Issues.Where(i => i.IsVotableIssue == true).ToList();
        }

        public List<Issue> GetAllVotableIssuesSortedByDate()
        {
            var list = GetAllVotableIssues();
            list.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            return list;
        }

        public List<Issue> GetAllVotableIssuesSortedByVoteCount()
        {
            var list = GetAllVotableIssues();
            list.Sort((x, y) => y.Votes.Count.CompareTo(x.Votes.Count));
            return list;
        }
        public virtual List<Issue> GetAllFundableIssues()
        {
            return _db.Issues.Where(i => i.IsFundable == true).ToList();
        }

        public List<Issue> GetAllFundableIssuesSortedByDate()
        {
            var list = GetAllFundableIssues();
            list.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            return list;
        }

        public void Update(Issue issue)
        {
            issue.UpdatedAt = DateTime.UtcNow;
            _db.Entry(issue).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(Issue issue)
        {
            _db.Issues.Remove(issue);
            _db.SaveChanges();
        }

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