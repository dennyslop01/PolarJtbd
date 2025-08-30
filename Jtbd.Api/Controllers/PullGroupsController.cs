using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PullGroupsController: ControllerBase
    {
        private readonly ILogger<PullGroupsController> _logger;
        private readonly IPullGroups _repository;

        public PullGroupsController(IPullGroups repository, ILogger<PullGroupsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/PullGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PullGroups>>> GetPullGroups()
        {
            try
            {
                var PullGroups = await _repository.GetAllAsync();
                return Ok(PullGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/PullGroups/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PullGroups>> GetPullGroups(int id)
        {
            try
            {
                var PullGroups = await _repository.GetByIdAsync(id);
                return Ok(PullGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/PullGroupsProject/{id}
        [HttpGet("PullGroupsProject/{id}")]
        public async Task<ActionResult<PullGroups>> GetPullGroupsProject(int id)
        {
            try
            {
                var PullGroups = await _repository.GetByProjectIdAsync(id);
                return Ok(PullGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/PullGroups
        [HttpPost]
        public async Task<ActionResult> CrearPullGroups([FromBody] PullGroups nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetPullGroups), new { id = nuevo.IdPull }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/PullGroups/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarPullGroups(int id, [FromBody] PullGroups PullGroups)
        {
            if (id != PullGroups.IdPull)
            {
                return BadRequest("El ID del Pull no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(PullGroups);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pull no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/PullGroups/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarPullGroups(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pull no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
