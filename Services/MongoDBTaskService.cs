using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MongoDBTaskService
    {
        private IMongoCollection<Tasks> _tasksCollections;
        public MongoDBTaskService(IOptions<MongoDBTasksSettings> tasksSettings) 
        { 
            MongoClient client = new MongoClient(tasksSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(tasksSettings.Value.DatabaseName);
            _tasksCollections = database.GetCollection<Tasks>(tasksSettings.Value.CollectionName);

        }
        public async Task<List<Tasks>> GetAllAsync()
        {
            return await _tasksCollections.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Tasks>> GetAsync(string id)
        {
            FilterDefinition<Tasks> filter = Builders<Tasks>.Filter.Eq("Id", id);
            return await (_tasksCollections.Find(filter)).ToListAsync();
        }

        public async Task CreateTaskAsync(Tasks task)
        {
            try
            {
                if (task != null)
                {
                    await _tasksCollections.InsertOneAsync(task);
                }
                return;
            }
            catch (Exception ex)
            {
                return;
            }
            
        }

        public async Task UpdateTaskAsync(Tasks task)
        {
            try
            {
                if (task != null)
                {
                    FilterDefinition<Tasks> filter = Builders<Tasks>.Filter.Eq("Id", task.Id);
                    await _tasksCollections.ReplaceOneAsync(filter, task);
                }
                return;
            }
            catch (Exception ex)
            {
                return;
            }

        }

        public async Task DeleteTaskAsync(string id)
        {
            FilterDefinition<Tasks> filter = Builders<Tasks>.Filter.Eq("Id", id);
            await _tasksCollections.DeleteOneAsync(filter);
            return;
        }

    }
}
