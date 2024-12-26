using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITaskService
    {
        public Task<bool> AddTask(ClaimsPrincipal userId, string taskText);
        public Task<bool> DeleteTask(ClaimsPrincipal user_claim, string taskId);
        public Task<bool> SignAsDoneTask(ClaimsPrincipal user_claim, bool checkedInput, string taskId);
        public Task<string> GetTasks(ClaimsPrincipal user_claim);
    }
}
