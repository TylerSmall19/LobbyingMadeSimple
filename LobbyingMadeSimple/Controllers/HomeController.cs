using LobbyingMadeSimple.Models;
using System.Web.Mvc;
using LobbyingMadeSimple.Core.Interfaces;
using System.Collections.Generic;
using LobbyingMadeSimple.Web.Models;

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
            var votableVms = new List<VotableHomeIssueViewModel>();
            // Convert Issue entities into votableVms
            _issueRepo.GetAllVotableIssuesSortedByVoteCount().ForEach(i => votableVms.Add(i));

            var viewModel = new HomePageViewModel()
            {
                VotableIssues = votableVms,
                FundableIssues = _issueRepo.GetAllFundableIssuesSortedByDate()
            };
            
            return View(viewModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}