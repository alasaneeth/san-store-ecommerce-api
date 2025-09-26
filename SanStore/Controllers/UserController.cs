using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanStore.Application.ApplicationConstant;
using SanStore.Application.InputModels;
using SanStore.Application.Services;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Common;
using System.Net;

namespace SanStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponse _response;

        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new APIResponse();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<APIResponse> Register(Register register)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommenMessage.RegistrationFalid);
                    return _response;
                }

                var result = await _authService.Register(register);
    
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.DisplayMessage = CommenMessage.RegistrationSuccess;
                _response.Result = result;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<APIResponse> Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommenMessage.RegistrationFalid);
                    return _response;
                }

                var result = await _authService.Login(login);

                if(result is string)
                {
                    _response.IsSuccess= true;
                    _response.StatusCode= HttpStatusCode.BadRequest;
                    _response.DisplayMessage= CommenMessage.LoginFalid;
                     return _response;
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.DisplayMessage = CommenMessage.LoginSuccess;
                _response.Result = result;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }
    }
}
