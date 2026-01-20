using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace task_manager_api.Entity
{
    [BsonIgnoreExtraElements]
    public class SubTask
    {
        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("order")]
        public int Order { get; set; }
    }
}
