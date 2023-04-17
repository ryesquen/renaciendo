using System.Data.SqlTypes;

namespace TiendaAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
