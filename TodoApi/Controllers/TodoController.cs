using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DBContext;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
			
			/*if (_context.Items.Count() == 0)
			{
				Item[] items = { new Item() { ID =1, Name = "First" }, new Item() { ID = 2, Name = "Second" }, new Item() { ID = 3, Name = "Third" } };

				_context.Items.AddRange(items);
				_context.SaveChanges();
			}*/
        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Item>>> GetItems()
		{
			return await _context.Items.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Item>> GetItem(int id)
		{
			Item item = await _context.Items.FindAsync(id);

			if (item == null)
			{
				return NotFound();
			}

			return item;
		}

		[HttpPost]
		public async Task<ActionResult<Item>> AddItem(Item item)
		{
			_context.Items.Add(item);

			await _context.SaveChangesAsync();

			return CreatedAtAction("GetItem", new { ID = item.ID }, item);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem(int id, Item item)
		{
			if (id != item.ID)
			{
				return BadRequest();
			}

			_context.Entry(item).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Item>> DeleteItem(int id)
		{
			Item item = await _context.Items.FindAsync(id);

			if (item == null)
			{
				return NotFound();
			}
			_context.Items.Remove(item);

			await _context.SaveChangesAsync();

			return item;
		}
    }
}