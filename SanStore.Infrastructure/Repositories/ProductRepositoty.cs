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
    public class ProductRepositoty : GenericRepository<Product>, IProductRespositoty
    {

        public ProductRepositoty(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task UpdateAsync(Product product)
        {
             _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
            
        }
    }
}
