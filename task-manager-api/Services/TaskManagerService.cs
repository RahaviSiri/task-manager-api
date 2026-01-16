using task_manager_api.Entity;
using task_manager_api.Model;
using task_manager_api.Repository;

namespace task_manager_api.Services
{
    public class TaskManagerService
    {
        private readonly TaskManagerRepository _taskManagerRepository;
        public TaskManagerService(TaskManagerRepository taskManagerRepository) {
            _taskManagerRepository = taskManagerRepository;
        }
        public Task<List<Tasks>> GetTasksAsync()
        {
            return _taskManagerRepository.GetTasksAsync();
        }

        public async Task CreateTasksAsync(CreateTaskDTO taskDTO)
        {
            await _taskManagerRepository.CreateTaskAsync(taskDTO);
        }

        public async Task UpdateTasksAsync(string Id, CreateTaskDTO taskDTO)
        {
            Tasks task = await _taskManagerRepository.GetTaskByIdAsync(Id);
            task.Title = taskDTO.title;
            await _taskManagerRepository.UpdateTasksAsync(Id,task);
        }

        public async Task DeleteTaskAsync(string Id)
        {
            await _taskManagerRepository.DeleteTaskAsync(Id);
        }
    }
}
