using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TodoApi.Models;

namespace TodoApi.DBContext
{
    public class TodoContext : DbContext
    {
		Item[] _items = new Item[]
		{
			new Item() { Name = "One", IsComplete = false },
			new Item() { Name = "Two", IsComplete = false },
			new Item() { Name = "Three", IsComplete = false }
		};

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
			if (this.Items.Count() == 0)
			{
				this.Items.AddRange(_items);
				this.SaveChanges();
			}
        }

        public DbSet<Item> Items
        {
            get;
            set;
        }
    }
}
