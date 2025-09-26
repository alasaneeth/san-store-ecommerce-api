using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name="ADMIN", NormalizedName="ADMIN"},
                new IdentityRole {Name = "CUSTOMER", NormalizedName= "CUSTOMER"}
            };

            foreach (var role in roles) 
            {
                if (!await roleManager.RoleExistsAsync(role.Name)) 
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
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
