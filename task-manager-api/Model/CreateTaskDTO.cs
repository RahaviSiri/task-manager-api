using task_manager_api.Entity;

namespace task_manager_api.Model
{
    public class CreateTaskDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public List<SubTask> SubTasks { get; set; } = new();
        public string Status { get; set; } = null!;

    }
}
