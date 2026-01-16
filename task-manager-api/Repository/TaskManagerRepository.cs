using MongoDB.Driver;
using Microsoft.Extensions.Options;
using task_manager_api.Config;
using task_manager_api.Entity;
using task_manager_api.Model;

namespace task_manager_api.Repository
{
    public class TaskManagerRepository
    {
        private readonly IMongoCollection<Tasks> _taskCollection;

        public TaskManagerRepository(IOptions<MongoDbSettings> settings) {
            var setting = settings.Value;

            var mongoClient = new MongoClient(setting.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(setting.DatabaseName);
            _taskCollection = mongoDatabase.GetCollection<Tasks>(setting.CollectionName);
        }

        public async Task<List<Tasks>> GetTasksAsync()
        {
            List<Tasks> tasks = await _taskCollection.Find(_ => true).ToListAsync();
            return tasks;
        }

        public async Task<Tasks> CreateTaskAsync(CreateTaskDTO taskDTO)
        {
            Tasks task = new Tasks
            {
                Title = taskDTO.Title
            };
            await _taskCollection.InsertOneAsync(task);
            return task;
        }

        public async Task<Tasks> GetTaskByIdAsync(string Id)
        {
            return await _taskCollection.Find(task => task.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Tasks> UpdateTasksAsync(string Id, Tasks task)
        {
            await _taskCollection.ReplaceOneAsync(t => t.Id == Id,task);
            return task;
        }

        public async Task<String> DeleteTaskAsync(string Id)
        {
            await _taskCollection.DeleteOneAsync(t => t.Id == Id);
            return Id;
        }
    }
}
