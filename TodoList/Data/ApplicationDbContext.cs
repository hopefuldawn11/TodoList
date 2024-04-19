using Microsoft.EntityFrameworkCore;
using TodoList.Models.Entities;

namespace TodoList.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
