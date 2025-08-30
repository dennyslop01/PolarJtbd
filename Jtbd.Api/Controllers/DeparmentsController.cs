using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparmentsController: ControllerBase
    {
        private readonly ILogger<DeparmentsController> _logger;
        private readonly IDeparments _repository;

        public DeparmentsController(IDeparments repository, ILogger<DeparmentsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Deparments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deparments>>> GetDeparments()
        {
            try
            {
                var Deparments = await _repository.GetAllAsync();
                return Ok(Deparments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Deparments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Deparments>> GetDeparments(int id)
        {
            try
            {
                var Deparments = await _repository.GetByIdAsync(id);
                return Ok(Deparments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Deparments
        [HttpPost]
        public async Task<ActionResult> CrearDeparments([FromBody] Deparments nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetDeparments), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        // PUT: api/Deparments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarDeparments(int id, [FromBody] Deparments deparment)
        {
            if (id != deparment.Id)
            {
                return BadRequest("El ID del Deparmento no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(deparment);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Deparmento no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Deparments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarDeparments(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Deparmento no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
