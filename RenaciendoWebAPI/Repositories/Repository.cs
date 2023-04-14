using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Repositories.IRepository;
using System.Diagnostics;
using System.Linq.Expressions;

namespace RenaciendoWebAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RenacerContext _renacerContext;
        internal DbSet<T> _dbSet;

        public Repository(RenacerContext renacerContext)
        {
            _renacerContext = renacerContext;
            _dbSet = _renacerContext.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = _dbSet;
            if (filtro is not null)
            {
                query = query.Where(filtro);
            }
            return query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro is not null)
            {
                query = query.Where(filtro);
            }
            return (await query.FirstOrDefaultAsync())!;
        }

        public async Task SaveChangesAsync()
        {
            await _renacerContext.SaveChangesAsync();
        }
    }
}
