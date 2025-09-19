using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanStore.Domain.Models;
using SanStore.Infrastructure.DbContexts;

namespace SanStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();
            return Ok();

        }

        [HttpGet]
        public ActionResult Get()
        {
            var catgeries = _dbContext.Category.ToList();
            return Ok(catgeries);
        }

        [HttpGet]
        [Route("Details")] 
        public ActionResult Get(int id)
        {
            var category = _dbContext.Category.FirstOrDefault(x => x.Id == id);
            if (category == null) { 
                return NotFound("Category ot found");            
            }
            return Ok(category);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult Update([FromBody] Category category) {
            var result = _dbContext.Category.Update(category);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id) {
            if (id == 0) {

                return BadRequest();

            }
            var result = _dbContext.Category.FirstOrDefault( x => x.Id == id );
            if (result == null)
            {
                return NotFound();

            }

            _dbContext.Category.Remove(result);
            _dbContext.SaveChanges();
            return NoContent();

         
        }
    }
}
