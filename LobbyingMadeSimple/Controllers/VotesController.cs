﻿using LobbyingMadeSimple.Helpers;
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

            var usersVotes = issue.Votes.Where(v => v.AuthorID == User.Identity.GetUserId());
            Vote vote;
            bool isUpvote = voteType == "Up";

            if (usersVotes.Count() > 0)
            {
                vote = usersVotes.First();
                vote.IsUpvote = isUpvote;
            } else
            {
                vote = new Vote()
                {
                    AuthorID = User.Identity.GetUserId(),
                    IssueID = issue.IssueID,
                    IsUpvote = isUpvote
                };

                issue.Votes.Add(vote);
            }

            _issueRepo.Update(issue);

            if (vote.VoteID > 0)
            {
                if (Request.IsAjaxRequest())
                {
                    string votePercent = issue.GetPrettyPercentage();
                    var data = new
                    {
                        votePercent = votePercent,
                        neededVotes = issue.VotesLeftUntilApproval(),
                        totalVotes = issue.TotalVotes(),
                        issueId = issue.IssueID,
                        wasUpvote = isUpvote,
                        votePercentageCssClass = HtmlHelpers.GetCssClassForVotePercentage(votePercent)
                    };

                    return Json(data);
                } else
                {
                    return RedirectToRoute("/Issues/Vote");
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}