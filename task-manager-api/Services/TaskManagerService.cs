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
        public Task<List<TaskEntity>> GetTasksAsync()
        {
            return _taskManagerRepository.GetTasksAsync();
        }

        public async Task<TaskEntity> CreateTasksAsync(CreateTaskDTO taskDTO)
        {
            TaskEntity task = new TaskEntity
            {
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                StartDate = taskDTO.StartDate,
                SubTasks = taskDTO.SubTasks,
                Status = taskDTO.Status
            };
            return await _taskManagerRepository.CreateTaskAsync(task);
        }

        public async Task<TaskEntity> UpdateTasksAsync(string Id, CreateTaskDTO taskDTO)
        {
            var task = await _taskManagerRepository.GetTaskByIdAsync(Id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with id {Id} not found.");
            }

            task.Title = taskDTO.Title;
            task.Description = taskDTO.Description;
            task.StartDate = taskDTO.StartDate;
            task.SubTasks = taskDTO.SubTasks;
            task.Status = taskDTO.Status;
            return await _taskManagerRepository.UpdateTaskAsync(Id,task);
        }

        public async Task<String> DeleteTaskAsync(string Id)
        {
            var task = await _taskManagerRepository.GetTaskByIdAsync(Id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with id {Id} not found.");
            }
            return await _taskManagerRepository.DeleteTaskAsync(Id);
        }
    }
}
