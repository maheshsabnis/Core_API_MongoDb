using Core_API_MongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Core_API_MongoDb.Services
{
    /// <summary>
    /// This class contains code for performing CRUD 
    /// Operations on MongoDB Collection
    /// </summary>
    public class NovelService
    {
        private readonly IMongoCollection<Novel> _NovelsCollection;

        public NovelService(IOptions<DatabaseSettings> dbSettings )
        {
            // Retrieve the DbInfromation

            // Reads the server instance for running database operations. The constructor of this class is provided in the MongoDB connection string:
            var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);
            //  Represents the Mongo database for running operations. 
            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DatabaseName);
            // the collection name.
            _NovelsCollection = mongoDatabase.GetCollection<Novel>(
                dbSettings.Value.CollectionName);
        }


        public async Task<List<Novel>> GetAsync() =>
              await _NovelsCollection.Find(_ => true).ToListAsync();

        public async Task<Novel?> GetAsync(string id) =>
            await _NovelsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Novel novel) =>
            await _NovelsCollection.InsertOneAsync(novel);

        public async Task UpdateAsync(string id, Novel novel) =>
            await _NovelsCollection.ReplaceOneAsync(x => x.Id == id, novel);

        public async Task RemoveAsync(string id) =>
            await _NovelsCollection.DeleteOneAsync(x => x.Id == id);

    }
}
