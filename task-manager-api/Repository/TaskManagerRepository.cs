using MongoDB.Driver;
using Microsoft.Extensions.Options;
using task_manager_api.Config;
using task_manager_api.Entity;
using task_manager_api.Model;

namespace task_manager_api.Repository
{
    public class TaskManagerRepository
    {
        private readonly IMongoCollection<TaskEntity> _taskCollection;

        public TaskManagerRepository(IOptions<MongoDbSettings> settings) {
            var setting = settings.Value;

            var mongoClient = new MongoClient(setting.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(setting.DatabaseName);
            _taskCollection = mongoDatabase.GetCollection<TaskEntity>(setting.CollectionName);
        }

        public async Task<List<TaskEntity>> GetTasksAsync()
        {
            List<TaskEntity> tasks = await _taskCollection.Find(_ => true).ToListAsync();
            return tasks;
        }

        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task)
        {
            await _taskCollection.InsertOneAsync(task);
            return task;
        }

        public async Task<TaskEntity> GetTaskByIdAsync(string Id)
        {
            return await _taskCollection.Find(task => task.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<TaskEntity?> UpdateTaskAsync(string id, TaskEntity task)
        {
            var result = await _taskCollection.ReplaceOneAsync(t => t.Id == id, task);
            return result.MatchedCount == 0 ? null : task;
        }

        public async Task<String> DeleteTaskAsync(string Id)
        {
            var result = await _taskCollection.DeleteOneAsync(t => t.Id == Id);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"Task with id {Id} not found.");
            return Id;
        }
    }
}
