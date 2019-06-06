using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizationApi.Models;

namespace OrganizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationContext _context;

        public OrganizationController(OrganizationContext context)
        {
            _context = context;

            if (_context.OrganizationItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.OrganizationItems.Add(new OrganizationItem { OrganizationName = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationItem>>> GetTodoItems()
        {
            return await _context.OrganizationItems.ToListAsync();
        }

        // GET: api/organization/1
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationItem>> GetOrganizationItem(long id)
        {
            var organizationItem = await _context.OrganizationItems.FindAsync(id);

            if (organizationItem == null)
            {
                return NotFound();
            }

            return organizationItem;
        }

        // POST: api/organization/ The method gets the value of the to-do item from the body of the HTTP request.
        [HttpPost]
        public async Task<ActionResult<OrganizationItem>> PostOrganizationItem(OrganizationItem item)
        {
            _context.OrganizationItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrganizationItem), new { id = item.Id }, item);
        }

        // DELETE: api/organization/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationItem(long id)
        {
            var organizationItem = await _context.OrganizationItems.FindAsync(id);

            if (organizationItem == null)
            {
                return NotFound();
            }

            _context.OrganizationItems.Remove(organizationItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}