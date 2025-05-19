using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrappers;

namespace MatchEstate.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetTasks(User);

            var response = new DataResponse<string>()
            {
                Success = true,
                Message = "",
                Data = tasks
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(string taskText)
        {
            var response = new BaseResponse();

            var result = await _taskService.AddTask(User, taskText);

            if (result)
            {
                response.Success = true;
                response.Message = "";
                return Ok(response);
            }
            response.Success = false;
            response.Message = "An error occured.";
            return Ok(response);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(string taskId)
        {
            var response = new BaseResponse();
            var result = await _taskService.DeleteTask(User ,taskId);

            if (result)
            {
                response.Success = true;
                response.Message = "";
                return Ok(response);
            }

            response.Success = false;
            response.Message = "An error occured.";
            return Ok(response);
        }

        public async Task<IActionResult> SignAsDoneTask(bool checkedInput, string taskId)
        {
            var response = new BaseResponse();

            var result = await _taskService.SignAsDoneTask(User, checkedInput, taskId);

            if (result)
            {
                response.Success = true;
                response.Message = "";
                return Ok(response);
            }
            response.Success = false;
            response.Message = "An error occured.";
            return Ok(response);
        }
    }
}
