using Microsoft.EntityFrameworkCore;
using SanStore.Domain.Contracts;
using SanStore.Domain.Models;
using SanStore.Infrastructure.DbContexts;
using SanStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task UpdateAsync(Brand brand)
        {
            _dbContext.Update<Brand>(brand);
            await _dbContext.SaveChangesAsync();
        }
    }
}

