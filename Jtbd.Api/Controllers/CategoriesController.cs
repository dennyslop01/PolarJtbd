using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategories _repository;

        public CategoriesController(ICategories repository, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            try
            {
                var Categories = await _repository.GetAllAsync();
                return Ok(Categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            try
            {
                var Categories = await _repository.GetByIdAsync(id);
                return Ok(Categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult> CrearCategories([FromBody] Categories nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetCategories), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCategories(int id, [FromBody] Categories category)
        {
            if (id != category.Id)
            {
                return BadRequest("El ID de la categoria no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(category);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoria no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCategories(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoria no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
