using Microsoft.AspNetCore.Mvc;
using PageRightsApi.Data;
using PageRightsApi.Models;
using Microsoft.EntityFrameworkCore;


namespace PageRightsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageRightsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PageRightsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateRights([FromBody] List<PageRight> pageRights)
        {
            if (pageRights == null || !pageRights.Any())
            {
                return BadRequest("No data provided.");
            }

            foreach (var right in pageRights)
            {
                var existing = await _context.PageRights
                    .FirstOrDefaultAsync(r => r.Username == right.Username && r.Name == right.Name);

                if (existing != null)
                {
                    existing.View = right.View;
                    existing.Edit = right.Edit;
                    existing.Add = right.Add;
                    existing.Delete = right.Delete;
                }
                else
                {
                    _context.PageRights.Add(right);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Page rights saved/updated successfully." });
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetRightsForUser(string username)
        {
            var rights = await _context.PageRights
                .Where(r => r.Username == username)
                .ToListAsync();

            return Ok(rights);
        }

        [HttpGet("access-pages")]
        public IActionResult GetPages(string username)
        {
            var pages = _context.PageRights
                .Where(p => p.Username == username && p.View == true)
                .Select(p => p.Name)
                .Distinct()
                .ToList();

            return Ok(pages);
        }

        [HttpGet("rights")]
        public IActionResult GetRights(string username)
        {
            var rights = _context.PageRights
                .Where(p => p.Username == username)
                .ToList();

            return Ok(rights);
        }


    }
}
