using BusinessLayer.Abstract;
using DTOLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MatchEstate.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IPropertyListingService _listingService;
        private readonly IAccountService _accountService;
        public AccountController(IPropertyListingService listingService, IAccountService accountService)
        {
            _listingService = listingService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.title = "Accounting Page";
            List<(string listingTitle, decimal earning)> kazancsOfMonth = await _listingService.GetEarningsOfMonth(UserId);
            return View(kazancsOfMonth);
        }

        [HttpPost]
        public async Task<IActionResult> AddIncomeExpense(IncomeExpenseModelDTO incomeExpenseModel)
        {
            bool result = await _accountService.AddIncomeExpense(User, incomeExpenseModel);
            if (result)
            {
                TempData["success"] = incomeExpenseModel.IncomeExpense == "income" ? "Income has been saved." : "Expense has been saved.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "An error occured.";
            return RedirectToAction("Index");
        }
    }
}
