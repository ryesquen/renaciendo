using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Models;

namespace RenaciendoWebAPI.Repositories.IRepository
{
    public class RenacerRepository : Repository<Renacer>, IRenacerRepository
    {
        private readonly RenacerContext _renacerContext;

        public RenacerRepository(RenacerContext renacerContext) : base(renacerContext)
        {
            _renacerContext = renacerContext;
        }
        public async Task<Renacer> UpdateAsync(Renacer renacer)
        {
            renacer.FechaActualizacion = DateTime.Now;
            _renacerContext.Update(renacer);
            await _renacerContext.SaveChangesAsync();
            return renacer;
        }
    }
}