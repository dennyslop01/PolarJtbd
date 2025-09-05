using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : Controller
    {
        private readonly ILogger<StoriesController> _logger;
        private readonly IStories _repository;

        public StoriesController(IStories repository, ILogger<StoriesController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Stories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stories>>> GetStories()
        {
            try
            {
                var Stories = await _repository.GetAllAsync();
                return Ok(Stories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Stories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Stories>> GetStories(int id)
        {
            try
            {
                var Stories = await _repository.GetByIdAsync(id);
                return Ok(Stories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/StoriesProject/{id}
        [HttpGet("StoriesProject/{id}")]
        public async Task<ActionResult<Stories>> GetStoriesProject(int id)
        {
            try
            {
                var Stories = await _repository.GetByProjectIdAsync(id);
                return Ok(Stories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Stories
        [HttpPost]
        public async Task<ActionResult> CrearStories([FromBody] CreateStorie nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetStories), new { id = nuevo.IdStorie }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Stories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarStories(int id, [FromBody] CreateStorie Stories)
        {
            if (id != Stories.IdStorie)
            {
                return BadRequest("El ID de la Ansiedad no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(Stories);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogError("Ansiedad no encontrado.");
                return NotFound("Ansiedad no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Stories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarStories(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogError("Ansiedad no encontrado.");
                return NotFound("Ansiedad no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}