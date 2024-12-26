using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MatchEstate.Controllers
{
    public class MatchingController : BaseController
    {
        private readonly IMatchingService _matchingService;
        public MatchingController(IMatchingService matchingService)
        {
            _matchingService = matchingService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.title = "Matchings Page";

            var requestListingPairs = await _matchingService.FindMatches(UserId);
            return View(requestListingPairs);
        }
    }
}
