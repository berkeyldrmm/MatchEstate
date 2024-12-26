using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddIncomeExpense(ClaimsPrincipal user_claim, IncomeExpenseModelDTO incomeExpenseModel)
        {
            User? user = await _userManager.GetUserAsync(user_claim);
            List<IncomeExpenseModelDTO> incomeExpenseModelList = JsonConvert.DeserializeObject<List<IncomeExpenseModelDTO>>(user.IncomeExpenses);
            incomeExpenseModel.Date = DateTime.Now;
            incomeExpenseModelList.Add(incomeExpenseModel);
            string incomeExpenseModelJson = JsonConvert.SerializeObject(incomeExpenseModelList);
            user.IncomeExpenses = incomeExpenseModelJson;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return await _unitOfWork.SaveChanges();
            }
            return 0;
        }
    }
}
