using LobbyingMadeSimple.Helpers;
using LobbyingMadeSimple.Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LobbyingMadeSimple.Core;

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
            var userId = User.Identity.GetUserId();

            if (issue == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!issue.IsVotableIssue)
            {
                Response.StatusCode = 422;
                if (Request.IsAjaxRequest())
                {
                    var data = new
                    {
                        issueId = issue.Id,
                        isVotable = false
                    };

                    return Json(data);
                }
                return View("../Issues/Vote", _issueRepo.GetAllVotableIssuesSortedByDate());
            }

            var usersVotes = issue.Votes.Where(v => v.AuthorID == userId);
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
                    AuthorID = userId,
                    IssueID = issue.Id,
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
                        votes = issue.TotalVotes(),
                        issueId = issue.Id,
                        wasUpvote = isUpvote,
                        votePercentageCssClass = HtmlHelpers.GetCssClassForVotePercentage(votePercent),
                        isVotable = true
                    };

                    return Json(data);
                } else
                {
                    return Redirect("/Issues/Vote#" + issueId);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}