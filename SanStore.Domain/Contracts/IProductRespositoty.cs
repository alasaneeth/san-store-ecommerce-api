using SanStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Domain.Contracts
{
    public interface IProductRespositoty : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetDetailAsync(int id);
        Task UpdateAsync(Product product);

    }
}
