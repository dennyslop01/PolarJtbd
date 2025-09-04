using Microsoft.AspNetCore.Mvc;
using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnxietiesController : ControllerBase
    {
        private readonly ILogger<AnxietiesController> _logger;
        private readonly IAnxieties _repository;

        public AnxietiesController(IAnxieties repository, ILogger<AnxietiesController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Anxieties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anxieties>>> GetAnxieties()
        {
            try
            {
                var Anxieties = await _repository.GetAllAsync();
                return Ok(Anxieties);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Anxieties/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Anxieties>> GetAnxieties(int id)
        {
            try
            {
                var Anxieties = await _repository.GetByIdAsync(id);
                return Ok(Anxieties);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/AnxietiesProject/{id}
        [HttpGet("AnxietiesProject/{id}")]
        public async Task<ActionResult<Anxieties>> GetAnxietiesProject(int id)
        {
            try
            {
                var Anxieties = await _repository.GetByProjectIdAsync(id);
                return Ok(Anxieties);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Anxieties
        [HttpPost]
        public async Task<ActionResult> CrearAnxieties([FromBody] CreateAnxietie nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetAnxieties), new { id = nuevo.IdAnxie }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Anxieties/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarAnxieties(int id, [FromBody] CreateAnxietie anxieties)
        {
            if (id != anxieties.IdAnxie)
            {
                return BadRequest("El ID de la Ansiedad no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(anxieties);
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

        // DELETE: api/Anxieties/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarAnxieties(int id)
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
