using Microsoft.AspNetCore.Identity;
using SanStore.Application.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> Register(Register register);

        Task<object> Login(Login login);
    }
}
