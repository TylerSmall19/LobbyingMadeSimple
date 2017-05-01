using LobbyingMadeSimple.Interfaces;
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
        private IVoteRepository _repo;
        public VotesController(IVoteRepository repo)
        {
            _repo = repo;
        }

        // TODO: Create Vote Repo; Get this into a working state
        // POST: Votes
        //[HttpPost]
        //public ActionResult Vote(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    _repo.Find(id);

        //    return RedirectToRoute("Issues/Vote");
        //}
    }
}