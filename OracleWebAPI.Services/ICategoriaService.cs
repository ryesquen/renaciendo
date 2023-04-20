using OracleWebAPI.Data.Models;

namespace OracleWebAPI.Services
{
    public interface ICategoriaService
    {
        Task<ResponseService<List<Categoria>>> GetAllCategories();
        Task<ResponseService<Categoria>> GetCategoryById(decimal id);
        Task<ResponseService<Categoria>> InsertCategory(Categoria categoria);
        Task<ResponseService<Categoria>> UpdateCategory(decimal id, Categoria categoria);
        Task<ResponseService<bool>> DeleteCategory(decimal id);
    }
}