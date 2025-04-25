using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftBackend.Data;
using ShiftBackend.Models;

namespace ShiftBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShiftController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/shift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShifts()
        {
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            return shift == null ? NotFound("Shift not found.") : Ok(shift);
        }

        // POST: api/shift
        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] ShiftModel shift)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                return BadRequest(new { message = "Validation failed", errors });
            }

            try
            {
                _context.Shifts.Add(shift);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, shift);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    message = "Database error during save.",
                    error = dbEx.InnerException?.Message ?? dbEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error saving shift.",
                    error = ex.Message
                });
            }
        }

        // PUT: api/shift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, [FromBody] ShiftModel shift)
        {
            if (id != shift.Id)
                return BadRequest("Shift ID mismatch.");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                return BadRequest(new { message = "Validation failed", errors });
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Shifts.Any(e => e.Id == id))
                    return NotFound("Shift not found.");

                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating shift.", error = ex.Message });
            }
        }

        // DELETE: api/shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                return NotFound("Shift not found.");

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
