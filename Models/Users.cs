using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("firstName")]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;
        [BsonElement("lastName")]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = null!;
        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [BsonElement("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

    }
}
