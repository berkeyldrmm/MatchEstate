using Microsoft.AspNetCore.Mvc;

namespace MatchEstate.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorPage(int code)
        {
            ViewBag.title = "ERROR!";
            if (code == 404)
            {
                return View("NotFound");
            }
            return View(code);
        }
    }
}