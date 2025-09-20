using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanStore.Domain.Contracts;
using SanStore.Domain.Models;
using SanStore.Infrastructure.DbContexts;

namespace SanStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }
      

        [HttpGet]
        public async Task<ActionResult>  Get()
        {
            var catgeries = await _categoryRepository.GetAllAsync();
            return Ok(catgeries);
        }

        [HttpGet]
        [Route("Details")] 
        public async Task<ActionResult>  Get(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(x=>x.Id == id);
            if (category == null) { 
                return NotFound("Category ot found");            
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult>  Create(Category category)
        {
         if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
         var entity = await _categoryRepository.CreateAsync(category);

            return Ok();

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update([FromBody] Category category) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryRepository.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id) {
            if (id == 0) {

                return BadRequest();

            }
            var result = await _categoryRepository.GetByIdAsync(x=> x.Id == id);
            if (result == null)
            {
                return NotFound();

            }

            await _categoryRepository.DeleteAsync(result);
            return NoContent();

         
        }
    }
}
