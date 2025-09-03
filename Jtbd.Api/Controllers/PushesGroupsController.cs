using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushesGroupsController: ControllerBase
    {
        private readonly ILogger<PushesGroupsController> _logger;
        private readonly IPushesGroups _repository;

        public PushesGroupsController(IPushesGroups repository, ILogger<PushesGroupsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/PushesGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PushesGroups>>> GetPushesGroups()
        {
            try
            {
                var PushesGroups = await _repository.GetAllAsync();
                return Ok(PushesGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/PushesGroups/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PushesGroups>> GetPushesGroups(int id)
        {
            try
            {
                var PushesGroups = await _repository.GetByIdAsync(id);
                return Ok(PushesGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/PushesGroupsProject/{id}
        [HttpGet("PushesGroupsProject/{id}")]
        public async Task<ActionResult<PushesGroups>> GetPushesGroupsProject(int id)
        {
            try
            {
                var PushesGroups = await _repository.GetByProjectIdAsync(id);
                return Ok(PushesGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/PushesGroups
        [HttpPost]
        public async Task<ActionResult> CrearPushesGroups([FromBody] CreatePushes nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetPushesGroups), new { id = nuevo.IdPush }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/PushesGroups/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarPushesGroups(int id, [FromBody] CreatePushes PushesGroups)
        {
            if (id != PushesGroups.IdPush)
            {
                return BadRequest("El ID del Push no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(PushesGroups);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Push no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/PushesGroups/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarPushesGroups(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Push no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
