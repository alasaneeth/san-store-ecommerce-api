using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SanStore.Application.ApplicationConstant;
using SanStore.Application.DTO.BrandDtos;
using SanStore.Application.DTO.CategoryDtos;
using SanStore.Application.Services;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Common;
using SanStore.Domain.Models;
using System.Net;
using System.Runtime.CompilerServices;

namespace SanStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        protected APIResponse _response;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<APIResponse> Get()
        {
            try
            {
                var brands = await _brandService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brands;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }

        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                var brand = await _brandService.GetByIdAsync(id);

                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.RecordNotFound;
                    return Ok(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brand;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);



            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateBrandDto brandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommenMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);

                }

                var entity = await _brandService.CreateAsync(brandDto);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommenMessage.CreateOperationSuccess;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommenMessage.CreateOperationFailed;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }
       
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDto dto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommenMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);

                }

                var brand = _brandService.GetByIdAsync(dto.Id);

                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.UpdateOperationFailed;
                }
                await _brandService.UpdateAsync(dto);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommenMessage.UpdateOperationSuccess;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommenMessage.UpdateOperationFailed;
                _response.AddError(CommenMessage.SystemError);
            }


            return Ok();
        }

        [HttpDelete]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                    return Ok(_response);


                }
                var result = await _brandService.GetByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                    return Ok(_response);
                }

                await _brandService.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommenMessage.DeleteOperationSuccess;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                _response.AddError(CommenMessage.SystemError);
            }

            return Ok();


        }

    }
}
