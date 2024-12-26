using DTOLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAccountService
    {
        public Task<int> AddIncomeExpense(ClaimsPrincipal user_claim, IncomeExpenseModelDTO incomeExpenseModel);
    }
}
