using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TiendaAPI.Connection;
using TiendaAPI.Models;

namespace TiendaAPI.Data
{
    public class ProductosData : IProductsData
    {
        private readonly ConnectionDB _connectionDB;

        public ProductosData(ConnectionDB connectionDB)
        {
            _connectionDB = connectionDB;
        }
        public async Task<List<Producto>> GetProducts()
        {
            var listProducts = new List<Producto>();
            using var sql = new SqlConnection(_connectionDB._connectionString);
            using var cmd = new SqlCommand("mostrarProductos", sql);
            await sql.OpenAsync();
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = cmd.ExecuteReader();
            while (await reader.ReadAsync())
            {
                var producto = new Producto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Descripcion = reader["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(reader["Precio"])
                };
                listProducts.Add(producto);
            }
            return listProducts;
        }
        public async Task<bool> InsertProduct(Producto producto)
        {
            using var sql = new SqlConnection(_connectionDB._connectionString);
            using var cmd = new SqlCommand("InsertarProducto", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            cmd.Parameters.AddWithValue("@precio", producto.Precio);
            await sql.OpenAsync();
            var result = await cmd.ExecuteNonQueryAsync();
            return result > 0;
        }
        public async Task<bool> EditProduct(Producto producto)
        {
            using var sql = new SqlConnection(_connectionDB._connectionString);
            using var cmd = new SqlCommand("EditarProducto", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", producto.Id);
            cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            cmd.Parameters.AddWithValue("@precio", producto.Precio);
            await sql.OpenAsync();
            var result = await cmd.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}
