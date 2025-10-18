using System.Security.Claims;
using Shared.Dtos.Account;

namespace BusinessLayer.Abstract
{
    public interface IAccountService
    {
        public Task<bool> AddIncomeExpense(ClaimsPrincipal user_claim, IncomeExpenseModelDTO incomeExpenseModel);
    }
}
