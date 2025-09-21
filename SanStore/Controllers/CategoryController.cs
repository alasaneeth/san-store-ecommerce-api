using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanStore.Application.ApplicationConstant;
using SanStore.Application.DTO;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Common;
using SanStore.Domain.Contracts;
using SanStore.Domain.Models;
using SanStore.Infrastructure.DbContexts;
using System.Net;

namespace SanStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        protected APIResponse _response;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _response = new APIResponse();
        }
      

        [HttpGet]
        public async Task<APIResponse>  Get()
        {
            try
            {
                var catgeries = await _categoryService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = catgeries;
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
        public async Task<ActionResult<APIResponse>>  Get(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.RecordNotFound;

                    return Ok(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = category;
            }
            catch (Exception) 
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>>  Create([FromBody] CreateCategoryDTO category)
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

                var entity = await _categoryService.CreateAsync(category);
                _response.StatusCode=HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommenMessage.CreateOperationSuccess;
            }
            catch  (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommenMessage.CreateOperationFailed;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;

        }

        [HttpPut]

        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateCategoryDto dto) {

            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommenMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return Ok(_response);

                }

                var category = _categoryService.GetByIdAsync(dto.Id);

                if(category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.UpdateOperationFailed;
                }
                 await _categoryService.UpdateAsync(dto);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess= true;  
                _response.DisplayMessage= CommenMessage.UpdateOperationSuccess;

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
 
        public async Task<ActionResult> Delete(int id) {
            try
            {
                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                    return Ok(_response);


                }
                var result = await _categoryService.GetByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                    return Ok(_response);





                }

                await _categoryService.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommenMessage.DeleteOperationSuccess;
            } catch(Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                _response.AddError(CommenMessage.SystemError);
            }
            
            return Ok();

         
        }
    }
}
