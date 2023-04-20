using Microsoft.EntityFrameworkCore;
using OracleWebAPI.Data.Models;

namespace OracleWebAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ModelContext _modelContext;

        public CategoriaService(ModelContext modelContext)
        {
            _modelContext = modelContext;
        }
        public async Task<ResponseService<List<Categoria>>> GetAllCategories()
        {
            var response = new ResponseService<List<Categoria>>();
            var list = await _modelContext.Categoria.ToListAsync();
            if (list is not null) response.Object = list;
            else response.AddInternalServerError("Se encontró un error.");
            return response;
        }
        public async Task<ResponseService<Categoria>> GetCategoryById(decimal id)
        {
            var response = new ResponseService<Categoria>();
            var category = await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == id);
            if (category is not null) response.Object = category;
            else response.AddNotFound("No se encontró la Categoria.");
            return response;
        }
        public async Task<ResponseService<Categoria>> InsertCategory(Categoria categoria)
        {
            var response = new ResponseService<Categoria>();
            try
            {
                await _modelContext.AddAsync(categoria);
                await _modelContext.SaveChangesAsync();
                decimal _id = await _modelContext.Categoria.MaxAsync(c => c.Id);
                response.Object = await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == _id);
            }
            catch (DbUpdateException ex)
            {
                response.AddInternalServerError(ex.Message);
            }
            return response;
        }
        public async Task<ResponseService<Categoria>> UpdateCategory(decimal id, Categoria categoria)
        {
            var response = new ResponseService<Categoria>();
            try
            {
                if (categoria is null) { response.AddBadRequest("No se mandó la categoría"); }
                if (id != categoria?.Id) { response.AddBadRequest("Los Id´s no corresponden"); }
                _modelContext.Update(categoria);
                await _modelContext.SaveChangesAsync();
                decimal _id = await _modelContext.Categoria.MaxAsync(c => categoria!.Id);
                response.Object = await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == _id);
            }
            catch (DbUpdateException ex)
            {
                response.AddInternalServerError(ex.Message);
            }
            return response;
        }
        public async Task<ResponseService<bool>> DeleteCategory(decimal id)
        {
            var response = new ResponseService<bool>();
            try
            {
                var category = await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == id);
                if (category is not null) _modelContext.Remove(category);
                else response.AddBadRequest("No se encontró la categoria a eliminar.");
                await _modelContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                response.AddInternalServerError(ex.Message);
            }
            return response;
        }
    }
}