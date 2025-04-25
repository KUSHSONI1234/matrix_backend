using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires JWT authentication
    public class FormDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FormDataController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/formdata
        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.FormData.ToList();
            return Ok(data);
        }

        // POST: api/formdata
        [HttpPost]
        public IActionResult Post([FromBody] FormData formData)
        {
            if (formData == null)
                return BadRequest(new { message = "Invalid form data." });

            _context.FormData.Add(formData);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = formData.Id }, formData);
        }

        // PUT: api/formdata/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FormData formData)
        {
            if (formData == null || id != formData.Id)
                return BadRequest(new { message = "ID mismatch or invalid data." });

            var existing = _context.FormData.Find(id);
            if (existing == null)
                return NotFound(new { message = "Record not found." });

            _context.Entry(existing).CurrentValues.SetValues(formData);
            _context.SaveChanges();

            return Ok(new { message = "Updated successfully." });
        }

        // DELETE: api/formdata/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = _context.FormData.Find(id);
            if (record == null)
                return NotFound(new { message = "Record not found." });

            _context.FormData.Remove(record);
            _context.SaveChanges();

            return Ok(new { message = "Deleted successfully." });
        }
    }
}
