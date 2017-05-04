using LobbyingMadeSimple.Interfaces;
using LobbyingMadeSimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LobbyingMadeSimple.Controllers
{
    public class HomeController : Controller
    {
        private IIssueRepository _issueRepo;
        public HomeController(IIssueRepository repo)
        {
            _issueRepo = repo;
        }
        public ActionResult Index()
        {
            List<Issue> votableIssues = _issueRepo.GetAllVotableIssues();
            return View(votableIssues);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}