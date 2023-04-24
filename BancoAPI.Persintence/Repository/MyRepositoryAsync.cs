using Ardalis.Specification.EntityFrameworkCore;
using BancoAPI.Application.Interfaces;
using BancoAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoAPI.Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public MyRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
