using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Login
    {
        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [BsonElement("password")]
        [JsonPropertyName("password")]
        public string Password { get;set; }= null!;
    }
}
