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
            return View(new IssueViewModel());
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
            return View(vmIssue);
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
        public ActionResult Edit(IssueViewModel issueVm)
        {
            // TODO: Refactor this 
            if (ModelState.IsValid)
            {
                _repo.Update(issueVm);

                return RedirectToAction("Index");
            }

            return View(issueVm);
        }

        // GET: Issues/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            // TODO: Refactor into IssueDeleteViewModel
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
            List<IssueViewModel> issueVms = new List<IssueViewModel>();
            issues.ForEach(i => issueVms.Add(i));

            return View(issueVms);
        }

        [HttpGet]
        public ActionResult Fund()
        {
            var fundableIssues = _repo.GetAllFundableIssuesSortedByDate();
            var fundableIssueVms = new List<IssueViewModel>();
            fundableIssues.ForEach(i => fundableIssueVms.Add(i));

            return View(fundableIssueVms);
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
