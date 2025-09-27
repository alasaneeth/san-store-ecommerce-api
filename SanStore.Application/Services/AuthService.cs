using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SanStore.Application.InputModels;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SanStore.Application.ViewModels;

namespace SanStore.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private ApplicationUser ApplicationUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            ApplicationUser = new();
            _config = config;
        }



        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            ApplicationUser.FirstName = register.FirstnaName;
            ApplicationUser.LastName = register.LastnaName;
            ApplicationUser.Email = register.Email;
            ApplicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(ApplicationUser, register.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(ApplicationUser, "CUSTOMER");
            }

            return result.Errors;
        }

        public async Task<object> Login(Login login)
        {
            ApplicationUser = await _userManager.FindByEmailAsync(login.Email);
            if (ApplicationUser == null)
            {
                return "Invalid Email Address";
            }

            var result = await _signInManager.PasswordSignInAsync(ApplicationUser, login.Password, isPersistent: true, lockoutOnFailure: true);

            var isValidCredential = await _userManager.CheckPasswordAsync(ApplicationUser, login.Password);
            if (result.Succeeded)
            {
                var token = await GenerateToken();
                LoginResponse loginResponse = new LoginResponse
                {
                    UserId = ApplicationUser.Id,
                    Token = token
                };
                return loginResponse;
            }
            else
            {
                if(result.IsLockedOut)
                {
                    return "Your Accoutn is locked, Contact Syaem Admin";
                }
                if (result.IsNotAllowed)
                {
                    return "Please Verify your E-mail Address";
                }

                if (isValidCredential == false)
                {
                    return "Invalid password";
                }else
                {
                    return "Login Faild";
                }
            }


        }

        public async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JwtSettings:Key")));
            var signinCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(ApplicationUser);
            var roleClaims = roles.Select(x=> new Claim(ClaimTypes.Role,x)).ToList();
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, ApplicationUser.Email)
            }.Union(roleClaims).ToList();

            var token = new JwtSecurityToken
                (
                    issuer: _config["JwtSettings:Issuer"],
                    audience: _config["JwtSettings:Audience"],
                    claims: claims,
                    signingCredentials: signinCredentials,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinute"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
