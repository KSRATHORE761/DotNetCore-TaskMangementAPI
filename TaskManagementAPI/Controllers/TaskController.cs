using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace TaskManagementAPI.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly MongoDBTaskService _taskService;
        public TaskController(MongoDBTaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        [HttpPost]
        [Route("add-task")]
        public async Task<JsonResult> AddTask([FromBody] Tasks tasks)
        {
            await _taskService.CreateTaskAsync(tasks);
            return Json("Successfully added task!");
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllTask")]
        public async Task<List<Tasks>> GetAllTasks()
        {
            List<Tasks> task = await _taskService.GetAllAsync();
            return task;
        }
        [Authorize]
        [HttpPost]
        [Route("EditTask")]
        public async Task<JsonResult> EditTaskAsync([FromBody] Tasks tasks)
        {
            if (tasks != null)
            {
                Console.WriteLine(tasks.Id);
                await _taskService.UpdateTaskAsync(tasks);
                return Json("Task edited successfully!");
            }
            return Json("Task not edited!");

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
