using SanStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Domain.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task Update(Category category);

    }
}
