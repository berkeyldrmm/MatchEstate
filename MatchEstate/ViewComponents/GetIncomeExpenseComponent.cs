using DTOLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace RealEstate.ViewComponents
{
    public class GetIncomeExpenseComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public GetIncomeExpenseComponent(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(decimal earning)
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            List<IncomeExpenseModelDTO> incomeExpenseList = JsonConvert.DeserializeObject<List<IncomeExpenseModelDTO>>(user.IncomeExpenses);
            List<IncomeExpenseModelDTO> incomeExpenseListOfMonth = incomeExpenseList.Where(g => g.Date.Month == DateTime.Now.Month).ToList();
            ViewBag.kazanc = earning;
            return View(incomeExpenseListOfMonth);
        }
    }
}
