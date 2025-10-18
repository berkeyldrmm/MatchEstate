using System.Security.Claims;

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
