using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBNetCore.Models;

namespace MongoDBNetCore.Repositories
{
    public class ProductCollection : IProductCollection
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductCollection(IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Product>(
                options.Value.BooksCollectionName);
        }

        public async Task DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, new ObjectId(id));
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var result = await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
            return result;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task InsertProduct(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, product.Id);
            await _collection.ReplaceOneAsync(filter, product);
        }
    }
}
