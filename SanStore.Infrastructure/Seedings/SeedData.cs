using SanStore.Domain.Models;
using SanStore.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Infrastructure.Seedings
{
    public class SeedData
    {
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext)
        {
            if (!_dbContext.Brands.Any())
            {
                await _dbContext.AddRangeAsync(
                    new Brand
                    {
                        Name = "Apple",
                        EstablishYear = 1956
                    },
                    new Brand
                    {
                         Name = "Samsung",
                         EstablishYear = 1956
                    },
                    new Brand
                    {
                         Name = "Sony",
                         EstablishYear = 1956
                    });

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
