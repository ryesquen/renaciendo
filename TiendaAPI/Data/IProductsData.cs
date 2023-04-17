using TiendaAPI.Models;

namespace TiendaAPI.Data
{
    public interface IProductsData 
    {
        public Task<List<Producto>> GetProducts();
        public Task<bool> InsertProduct(Producto producto);
        public Task<bool> EditProduct(Producto producto);
    }
}
