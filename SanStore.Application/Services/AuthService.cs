using Microsoft.AspNetCore.Identity;
using SanStore.Application.InputModels;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser ApplicationUser;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            ApplicationUser = new();
        }
        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            ApplicationUser.FirstName = register.FirstnaName;
            ApplicationUser.LastName = register.LastnaName;
            ApplicationUser.Email = register.Email;
            ApplicationUser.UserName = register.Email;
            
            var result = await _userManager.CreateAsync(ApplicationUser,register.Password);

            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(ApplicationUser, "ADMIN");
            }

            return result.Errors;
        }
    }
}
