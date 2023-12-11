using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Reflection.Metadata;
using Models;


namespace Services
{
    public class MongoDBUserService
    {
        private readonly IMongoCollection<Users> _usersCollection;
        private readonly AuthService _authService;
        public MongoDBUserService(IOptions<MongoDBUserSettings> mongoDBSettings, AuthService authService)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<Users>(mongoDBSettings.Value.CollectionName);
            _authService = authService;
        }
        public async Task<List<Users>> GetAllAsync() {
            return await _usersCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<List<Users>> GetAsync(string email)
        {
            FilterDefinition<Users> filter =  Builders<Users>.Filter.Eq("email", email);
            return await (_usersCollection.Find(filter)).ToListAsync();
        }

        //public async Task<List<Users>> GetLoginAsync(string email)
        //{
        //    FilterDefinition<Users> filter = Builders<Users>.Filter.Eq("email", email);
        //    List<Users> user = _usersCollection.Find(filter).ToList();
        //    return await ().ToListAsync();
        //}


        public async Task CreateAsync(Users user)
        {
            if (user != null)
            {
                await _usersCollection.InsertOneAsync(user);
            }
            return;
        }
        public async Task AddToUserAsync(string id,string name){
            FilterDefinition<Users> filter = Builders<Users>.Filter.Eq("Id", id);
            UpdateDefinition<Users> update = Builders<Users>.Update.AddToSet<string>("name", name);
            await _usersCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id) { 
            FilterDefinition<Users> filter = Builders<Users>.Filter.Eq("Id", id);

            await _usersCollection.DeleteOneAsync(filter);
            return;
        }


    }

}