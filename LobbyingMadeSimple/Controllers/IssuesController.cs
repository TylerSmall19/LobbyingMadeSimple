using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LobbyingMadeSimple.Models;
using Microsoft.AspNet.Identity;

namespace LobbyingMadeSimple.Controllers
{
    public class IssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Issues
        public ActionResult Index()
        {
            //List<Issue> issues = ListIssueRepository.GetAllIssues();
            var issues = db.Issues.Include(i => i.Author);
            return View(issues.ToList());
        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // GET: Issues/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Title,ShortDescription,LongDescription,IsStateIssue,StateAbbrev")] Issue issue)
        {
            issue.AuthorID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "StateName", issue.AuthorID);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Title,ShortDescription,LongDescription,IsStateIssue,StateAbbrev")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Users, "Id", "StateName", issue.AuthorID);
            return View(issue);
        }

        // GET: Issues/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Issues/Vote
        [HttpGet]
        [Authorize]
        public ActionResult Vote()
        {
            var issues = db.Issues.Where(v => v.IsApproved == true);
            return View(issues.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
