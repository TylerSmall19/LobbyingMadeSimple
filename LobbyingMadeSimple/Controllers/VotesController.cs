using LobbyingMadeSimple.Interfaces;
using LobbyingMadeSimple.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LobbyingMadeSimple.Controllers
{
    public class VotesController : Controller
    {
        private IIssueRepository _issueRepo;
        public VotesController(IIssueRepository issueRepo)
        {
            _issueRepo = issueRepo;
        }

        // POST: Issues/5/Vote/Up
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int issueId, string voteType)
        {
            Issue issue = _issueRepo.Find(issueId);

            if (issue == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            bool isUpvote = voteType == "Up";

            Vote vote = new Vote()
            {
                AuthorID = User.Identity.GetUserId(),
                IssueID = issue.IssueID,
                IsUpvote = isUpvote
            };

            issue.Votes.Add(vote);
            _issueRepo.Update(issue);

            if (vote.VoteID > 0)
            {
                var data = new
                {
                    voteScore = issue.NetScore(),
                    neededVotes = issue.VotesLeftUntilApproval(),
                    totalVotes = issue.TotalVotes(),
                    issueId = issue.IssueID,
                    wasUpvote = isUpvote
                };

                return Json(data);
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}