using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace task_manager_api.Entity
{
    [BsonIgnoreExtraElements]
    public class Tasks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = null!;  
    }
}
