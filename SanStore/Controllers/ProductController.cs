using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanStore.Application.ApplicationConstant;
using SanStore.Application.DTO.CategoryDtos;
using SanStore.Application.DTO.ProductDto;
using SanStore.Application.InputModels;
using SanStore.Application.Services;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Common;
using System.Net;

namespace SanStore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        protected APIResponse _response;

        public ProductController(IProductService productService)
        {
            _productService = productService; 
            _response = new APIResponse();  
        }

        [HttpGet]
        public async Task<APIResponse> Get()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = products;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }

        [HttpPost]
        [Route("GetPagination")]
        public async Task<APIResponse> Get(PaginationInputModel pagination)
        {
            try
            {
                var products = await _productService.GetPagination(pagination);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = products;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }

        [HttpGet]
        [Route("Filter")]
        public async Task<APIResponse> GetFilter( int? categoryId, int? brandId)
        {
            try
            {
                var products = await _productService.GetAllbyFilterAsync(categoryId, brandId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = products;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommenMessage.SystemError);
            }

            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateProductDto product)
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

                var entity = await _productService.CreateAsync(product);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommenMessage.CreateOperationSuccess;
                _response.Result = entity; 
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommenMessage.CreateOperationFailed;
                _response.AddError(ex.Message); // Better to include actual exception message
                return Ok(_response);
            }
        }
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                var category = await _productService.GetByIdAsync(id);
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
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateProductDto dto)
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

                var product = _productService.GetByIdAsync(dto.Id);

                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.UpdateOperationFailed;
                }
                await _productService.UpdateAsync(dto);

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
                var result = await _productService.GetByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommenMessage.DeleteOperationFailed;
                    return Ok(_response);





                }

                await _productService.DeleteAsync(id);

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