using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Models
{
    public class Tasks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("title")]
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [BsonElement("status")]
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [BsonElement("userId")]
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        
        [BsonElement("due_date")]
        [JsonPropertyName("due_date")]
        public DateTime? Due_Date { get; set; }
        
        [BsonElement("created_date")]
        [JsonPropertyName("created_date")]
        public DateTime? Created_Date { get; set; }
        
        [BsonElement("updated_date")]
        [JsonPropertyName("updated_date")]
        public DateTime? Updated_Date { get; set; }
    }
}
