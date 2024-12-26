using Microsoft.AspNetCore.Mvc;
using DTOLayer;
using Microsoft.AspNetCore.Identity;
using EntityLayer.Entities;
using System.Security.Claims;
using MatchEstate.Controllers;

namespace MatchEstate.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.title = "Home Page";
            return View();
        }
        
    }
}