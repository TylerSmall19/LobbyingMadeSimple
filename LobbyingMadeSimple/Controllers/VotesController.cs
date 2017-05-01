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
        public VotesController(IVoteRepository repo)
        {
            _repo = repo;
        }

        // TODO: Create Vote Repo; Get this into a working state
        // POST: Issues/5/Vote/Up
        [HttpPost]
        public ActionResult Vote(int? id, string voteType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            new Vote()
            {
                AuthorID = User.Identity.GetUserId(),
                IssueID = (int)id,
                IsUpvote = voteType == "Up"
            };

            //_repo.Find((int) id);
            return RedirectToRoute("Issues/Vote");
        }
    }
}