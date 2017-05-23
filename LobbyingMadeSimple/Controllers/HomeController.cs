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
            var fundableVms = new List<FundableHomeIssueViewModel>();

            // Convert Issue entities into vm collections
            _issueRepo.GetTopVotableIssues(15).ForEach(i => votableVms.Add(i));
            _issueRepo.GetAllFundableIssuesSortedByDate().ForEach(i => fundableVms.Add(i));

            var viewModel = new HomePageViewModel()
            {
                VotableIssues = votableVms,
                FundableIssues = fundableVms
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