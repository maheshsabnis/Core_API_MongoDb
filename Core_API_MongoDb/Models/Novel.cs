
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Core_API_MongoDb.Models
{
    /// <summary>
    /// Required for mapping the Common Language Runtime (CLR) object to the MongoDB collection.
    /// Annotated with [BsonId] to make this property the document's primary key.
    /// Annotated with [BsonRepresentation(BsonType.ObjectId)] to allow passing the parameter as type string instead of an ObjectId structure. Mongo handles the conversion from string to ObjectId.
    /// </summary>
    public class Novel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? NovelName { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
    }
}
