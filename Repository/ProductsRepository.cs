using CheckThingsAPI.Entities.Interfaces;
using CheckThingsAPI.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Repository
{
    public class ProductsRepository<T> : IProductsRepository<T>
        where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;

        public ProductsRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.Run(() => _collection.Find(doc => true).ToListAsync());
        }

        public Task<T> GetByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                return _collection.Find(doc => doc.Id == id).SingleOrDefaultAsync();
            });
        }

        public Task InsertOneAsync(T document)
        {
            return Task.Run(() =>
            {
                document.Id = new BsonObjectIdGenerator().GenerateId(_collection, document).ToString();
                _collection.InsertOneAsync(document);
            });
        }

        public void ReplaceOneAsync(string id, T document)
        {
            Task.Run(() =>
            {
                _collection.ReplaceOneAsync(doc => doc.Id == id, document);
            });
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                _collection.FindOneAndDeleteAsync(doc => doc.Id == id);
            });
        }
    }
}
