using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanStore.Application.DTO;
using SanStore.Application.Services.Interface;
using SanStore.Domain.Contracts;
using SanStore.Domain.Models;
using SanStore.Infrastructure.DbContexts;

namespace SanStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
      

        [HttpGet]
        public async Task<ActionResult>  Get()
        {
            var catgeries = await _categoryService.GetAllAsync();
            return Ok(catgeries);
        }

        [HttpGet]
        [Route("Details")] 
        public async Task<ActionResult>  Get(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) { 
                return NotFound("Category ot found");            
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult>  Create([FromBody] CreateCategoryDTO category)
        {
         if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
         var entity = await _categoryService.CreateAsync(category);

            return Ok();

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update([FromBody] UpdateCategoryDto category) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id) {
            if (id == 0) {

                return BadRequest();

            }
            var result = await _categoryService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();

            }

            await _categoryService.DeleteAsync(id);
            return NoContent();

         
        }
    }
}
