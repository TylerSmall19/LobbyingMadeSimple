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
        private IVoteRepository _voteRepo;
        private IIssueRepository _issueRepo;
        public VotesController(IVoteRepository voteRepo, IIssueRepository issueRepo)
        {
            _voteRepo = voteRepo;
            _issueRepo = issueRepo;
        }

        // POST: Issues/5/Vote/Up
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? issueId, string voteType)
        {
            bool isUpvote = voteType == "Up";

            Vote vote = new Vote()
            {
                AuthorID = User.Identity.GetUserId(),
                IssueID = (int)issueId,
                IsUpvote = isUpvote
            };

            _voteRepo.Add(vote);

            if (vote.VoteID > 0)
            { 
                Issue issue = _issueRepo.Find(vote.IssueID);

                var data = new {
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