using RenaciendoWebAPI.Models;

namespace RenaciendoWebAPI.Repositories.IRepository
{
    public interface IRenacerRepository : IRepository<Renacer>
    {
        Task<Renacer> UpdateAsync(Renacer renacer);
    }
}
