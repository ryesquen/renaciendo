using MongoDBNetCore.Models;

namespace MongoDBNetCore.Repositories
{
    public interface IProductCollection
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(string id);
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(string id);
    }
}
