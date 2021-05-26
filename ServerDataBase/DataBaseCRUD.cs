using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServerDataBase
{
    public class DataBaseCRUD
    {
        public string CollectionName { get; set; } = "TestCollection";

        private IMongoDatabase _mongoDatabase;

        public DataBaseCRUD(string db = "TestDB")
        {
            MongoClient mongoClient = new MongoClient();
            _mongoDatabase = mongoClient.GetDatabase(db);

            LogOperationResult("DataBaseCRUD ctor OK");
        }

        public void LogOperationResult(string text)
        {
            Debug.WriteLine($"---LOG {text}");
        }

        public async void Insert<T>(T type)
        {
            IMongoCollection<T> currentCollection = _mongoDatabase.GetCollection<T>(CollectionName);
            await currentCollection.InsertOneAsync(type);
        }

        public List<T> Read<T>()
        {
            IMongoCollection<T> currentCollection = _mongoDatabase.GetCollection<T>(CollectionName);

            return currentCollection.Find(new BsonDocument()).ToList();
        }

        //overloaded by name
        public List<T> Read<T>(string name)
        {
            IMongoCollection<T> currentCollection = _mongoDatabase.GetCollection<T>(CollectionName);

            var filtered = Builders<T>.Filter.Eq("Name", name);

            return currentCollection.Find(filtered).ToList();
        }

        public void Update<T>(string name, string field, string value)
        {
            IMongoCollection<T> currentCollection = _mongoDatabase.GetCollection<T>(CollectionName);
            var filtered = Builders<T>.Filter.Eq("Name", name);
            var update = Builders<T>.Update
                .Set(field, value);

            currentCollection.UpdateOne(filtered, update);
        }

        public bool Delete<T>(string name)
        {
            IMongoCollection<T> currentCollection = _mongoDatabase.GetCollection<T>(CollectionName);
            FilterDefinition<T> filtered = Builders<T>.Filter.Eq("Name", name);

            if (currentCollection.DeleteOne(filtered).DeletedCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
