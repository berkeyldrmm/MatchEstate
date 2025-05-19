using BusinessLayer.Abstract;
using Shared.Dtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly UserManager<User> _userManager;
        public TaskService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> AddTask(ClaimsPrincipal user_claim, string taskText)
        {
            var user = await _userManager.GetUserAsync(user_claim);
            TaskModelDTO task = new TaskModelDTO()
            {
                Id = Guid.NewGuid().ToString() + DateTime.Now.ToString("hhmmss"),
                TaskText = taskText,
                Status = true
            };

            List<TaskModelDTO> tasks = JsonConvert.DeserializeObject<List<TaskModelDTO>>(user.Tasks);
            tasks.Add(task);
            string tasksJson = JsonConvert.SerializeObject(tasks);
            user.Tasks = tasksJson;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteTask(ClaimsPrincipal user_claim, string taskId)
        {
            var user = await _userManager.GetUserAsync(user_claim);
            List<TaskModelDTO> tasks = JsonConvert.DeserializeObject<List<TaskModelDTO>>(user.Tasks);
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            var result = tasks.Remove(task);
            if (result)
            {
                user.Tasks = JsonConvert.SerializeObject(tasks);
                var result1 = await _userManager.UpdateAsync(user);
                return result1.Succeeded;
            }
            return false;
        }

        public async Task<string> GetTasks(ClaimsPrincipal user_claim)
        {
            User? user = await _userManager.GetUserAsync(user_claim);
            return user.Tasks;
        }

        public async Task<bool> SignAsDoneTask(ClaimsPrincipal user_claim, bool checkedInput, string taskId)
        {
            var user = await _userManager.GetUserAsync(user_claim);
            List<TaskModelDTO> tasks = JsonConvert.DeserializeObject<List<TaskModelDTO>>(user.Tasks);
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            task.Status = checkedInput;
            user.Tasks = JsonConvert.SerializeObject(tasks);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
