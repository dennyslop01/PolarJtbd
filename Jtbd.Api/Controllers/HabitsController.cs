using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController: ControllerBase
    {
        private readonly ILogger<HabitsController> _logger;
        private readonly IHabits _repository;

        public HabitsController(IHabits repository, ILogger<HabitsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Habits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habits>>> GetHabits()
        {
            try
            {
                var Habits = await _repository.GetAllAsync();
                return Ok(Habits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Habits/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Habits>> GetHabits(int id)
        {
            try
            {
                var Habits = await _repository.GetByIdAsync(id);
                return Ok(Habits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/HabitsProject/{id}
        [HttpGet("HabitsProject/{id}")]
        public async Task<ActionResult<Habits>> GetHabitsProject(int id)
        {
            try
            {
                var Habits = await _repository.GetByProjectIdAsync(id);
                return Ok(Habits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Habits
        [HttpPost]
        public async Task<ActionResult> CrearHabits([FromBody] Habits nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetHabits), new { id = nuevo.IdHabit }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Habits/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarHabits(int id, [FromBody] Habits Habits)
        {
            if (id != Habits.IdHabit)
            {
                return BadRequest("El ID del Habito no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(Habits);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Habito no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Habits/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarHabits(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Habito no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
