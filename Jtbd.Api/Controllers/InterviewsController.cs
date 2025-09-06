using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController: ControllerBase
    {
        private readonly ILogger<InterviewsController> _logger;
        private readonly IInterviews _repository;

        public InterviewsController(IInterviews repository, ILogger<InterviewsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Interviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interviews>>> GetInterviews()
        {
            try
            {
                var Interviews = await _repository.GetAllAsync();
                return Ok(Interviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Interviews/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Interviews>> GetInterviews(int id)
        {
            try
            {
                var Interviews = await _repository.GetByIdAsync(id);
                return Ok(Interviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Interviews
        [HttpPost]
        public async Task<ActionResult> CrearInterviews([FromBody] CreateInterview nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetInterviews), new { id = nuevo.IdInter }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Interviews/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarInterviews(int id, [FromBody] CreateInterview interviews)
        {
            if (id != interviews.IdInter)
            {
                return BadRequest("El ID del Entrevistado no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(interviews);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Entrevistado no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Interviews/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarInterviews(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Entrevistado no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
