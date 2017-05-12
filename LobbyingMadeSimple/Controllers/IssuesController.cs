using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LobbyingMadeSimple.Core.Interfaces;
using LobbyingMadeSimple.Core;
using LobbyingMadeSimple.Web.Models;

namespace LobbyingMadeSimple.Controllers
{
    public class IssuesController : Controller
    {
        private IIssueRepository _repo;

        public IssuesController(IIssueRepository repo)
        {
            _repo = repo;
        }

        // GET: Issues
        public ActionResult Index()
        {
            List<IssueViewModel> issueViewModels = new List<IssueViewModel>();
            _repo.GetAll().ForEach(i => issueViewModels.Add(i));

            return View(issueViewModels);
        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Issue issue = _repo.Find((int) id);

            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue); // TODO: Make a IssueDetailsViewModel
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
        public ActionResult Create(IssueViewModel vmIssue)
        {
            Issue issue = vmIssue;
            issue.AuthorID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                _repo.Add(issue);
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

            Issue issue = _repo.Find((int) id);

            if (issue == null)
            {
                return HttpNotFound();
            }

            return View((IssueViewModel)issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(IssueViewModel issue)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(issue);

                return RedirectToAction("Index");
            }

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

            Issue issue = _repo.Find((int) id);

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
            Issue issue = _repo.Find(id);
            _repo.Remove(issue);

            return RedirectToAction("Index");
        }

        // GET: Issues/Vote
        [HttpGet]
        //[Authorize] TODO: Uncomment before production
        public ActionResult Vote()
        {
            List<Issue> issues = _repo.GetAllVotableIssuesSortedByDate();
            return View(issues);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
