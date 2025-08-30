using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController: ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjects _repository;

        public ProjectsController(IProjects repository, ILogger<ProjectsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
        {
            try
            {
                var Projects = await _repository.GetAllAsync();
                return Ok(Projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Projects>> GetProjects(int id)
        {
            try
            {
                var Projects = await _repository.GetByIdAsync(id);
                return Ok(Projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/ProjectsDeparment/{id}
        [HttpGet("ProjectsDeparment/{id}")]
        public async Task<ActionResult<Projects>> GetProjectsDeparment(int id)
        {
            try
            {
                var Projects = await _repository.GetByDeparmentIdAsync(id);
                return Ok(Projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult> CrearProjects([FromBody] Projects nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetProjects), new { id = nuevo.IdProject }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarProjects(int id, [FromBody] Projects Projects)
        {
            if (id != Projects.IdProject)
            {
                return BadRequest("El ID del Proyecto no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(Projects);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proyecto no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Projects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProjects(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proyecto no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
