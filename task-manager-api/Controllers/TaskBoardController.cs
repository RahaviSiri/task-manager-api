using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using task_manager_api.Entity;
using task_manager_api.Model;
using task_manager_api.Services;

namespace task_manager_api.Controllers
{
    [ApiController]
    [Route("/api/task/")]
    public class TaskBoardController : ControllerBase
    {
        private readonly TaskManagerService _taskManagerService;

        public TaskBoardController(TaskManagerService taskManagerService)
        {
            this._taskManagerService = taskManagerService;
        }

        [HttpGet(Name = "GetTasks")]
        public async Task<IActionResult> GetTasksAsync()
        {
            List<Tasks> tasks = await _taskManagerService.GetTasksAsync();
            return Ok(tasks);
        }

        [HttpPost(Name = "AddTasks")]
        public async Task<IActionResult> CreateTasksAsync([FromBody] CreateTaskDTO taskDTO)
        {
            await _taskManagerService.CreateTasksAsync(taskDTO);
            return Ok("Task Created");
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTasksAsync(string Id,[FromBody] CreateTaskDTO taskDTO)
        {
            await _taskManagerService.UpdateTasksAsync(Id,taskDTO);
            return Ok("Task Updated");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTaskAsync(string Id)
        {
            await _taskManagerService.DeleteTaskAsync(Id);
            return Ok("Task Deleted");
        }
    }
}
