using Microsoft.AspNetCore.Mvc;

namespace MatchEstate.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var nameSurname = TempData["nameSurname"];
            ViewBag.title = "Home Page";
            return View();
        }
        
    }
}