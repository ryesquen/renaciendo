using OracleWebAPI.Data.Models;
using OracleWebAPI.DTO;

namespace OracleWebAPI.Mappings
{
    public static partial class Mappings
    {
        public static CategoriaDTO DataBaseObjectToDTO(Categoria categoria)
        {
            return new CategoriaDTO()
            {
                Id = categoria.Id,
                Descripcion = categoria.Descripcion
            };
        }
    }
    public static partial class Mappings
    {
        public static Categoria DTOToDataBaseObject(CategoriaDTO categoriaDTO)
        {
            return new Categoria()
            {
                Id = categoriaDTO.Id,
                Descripcion = categoriaDTO.Descripcion
            };
        }
    }
}
