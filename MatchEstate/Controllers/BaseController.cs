using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchEstate.Controllers
{
    public class BaseController : Controller
    {
        protected string UserId => HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
