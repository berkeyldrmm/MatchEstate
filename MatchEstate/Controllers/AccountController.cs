using BusinessLayer.Abstract;
using DTOLayer;
using Microsoft.AspNetCore.Mvc;

namespace MatchEstate.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IListingService _listingService;
        private readonly IAccountService _accountService;
        public AccountController(IListingService listingService, IAccountService accountService)
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
            int result = await _accountService.AddIncomeExpense(User, incomeExpenseModel);
            if (result > 0)
            {
                TempData["success"] = "Income/Expense has been saved.";
                return RedirectToAction("Index");
            }

            TempData["success"] = "An error occured.";
            return RedirectToAction("Index");
        }
    }
}
