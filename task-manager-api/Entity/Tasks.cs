using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace task_manager_api.Entity
{
    [BsonIgnoreExtraElements]
    public class TaskEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("subTasks")]
        public List<SubTask> SubTasks { get; set; } = new();

        [BsonElement("status")]
        public string Status { get; set; } = null!;
    }
}
