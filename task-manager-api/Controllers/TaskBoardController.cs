using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using task_manager_api.Entity;
using task_manager_api.Hubs;
using task_manager_api.Model;
using task_manager_api.Services;


namespace task_manager_api.Controllers
{
    [ApiController]
    [Route("/api/task/")]
    public class TaskBoardController : ControllerBase
    {
        private readonly TaskManagerService _taskManagerService;
        private readonly IHubContext<TaskHub> _hub;

        public TaskBoardController(TaskManagerService taskManagerService, IHubContext<TaskHub> hub)
        {
            this._taskManagerService = taskManagerService;
            this._hub = hub;
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
            Tasks task = await _taskManagerService.CreateTasksAsync(taskDTO);
            await _hub.Clients.All.SendAsync("TaskCreated", task);
            return Ok(task);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTasksAsync(string Id,[FromBody] CreateTaskDTO taskDTO)
        {
            Tasks task = await _taskManagerService.UpdateTasksAsync(Id,taskDTO);
            await _hub.Clients.All.SendAsync("TaskUpdated", task);
            return Ok(task);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTaskAsync(string Id)
        {
            String id = await _taskManagerService.DeleteTaskAsync(Id);
            await _hub.Clients.All.SendAsync("TaskDeleted", id);
            return Ok(id);
        }
    }
}
