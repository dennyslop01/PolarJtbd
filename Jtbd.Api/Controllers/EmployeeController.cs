using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jtbd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployee _repository;

        public EmployeeController(IEmployee repository, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            try
            {
                var Employee = await _repository.GetAllAsync();
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var Employee = await _repository.GetByIdAsync(id);
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult> CrearEmployee([FromBody] CreateEmployee nuevo)
        {
            try
            {
                if (nuevo == null)
                {
                    return BadRequest();
                }

                await _repository.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetEmployee), new { id = nuevo.Id }, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEmployee(int id, [FromBody] CreateEmployee Employee)
        {
            if (id != Employee.Id)
            {
                return BadRequest("El ID del Empleado no coincide.");
            }

            try
            {
                await _repository.UpdateAsync(Employee);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Empleado no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarEmployee(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Empleado no encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
