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
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Issue issue)
        {
            db.Issues.Add(issue);
            db.SaveChanges();
        }

        public Issue Find(int id)
        {
            return db.Issues.Find(id);
        }

        public List<Issue> GetAll()
        {
            return db.Issues.ToList();
        }

        public List<Issue> GetAllVotableIssues()
        {
            return db.Issues.Where(i => i.IsVotableIssue == true).ToList();
        }

        public void Update(Issue issue)
        {
            db.Entry(issue).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(Issue issue)
        {
            db.Issues.Remove(issue);
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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