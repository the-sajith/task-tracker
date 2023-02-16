using Microsoft.EntityFrameworkCore;
using Task = task_tracker.Models.Task;

namespace task_tracker.Data
{
	public class TaskDbContext : DbContext
	{
		public TaskDbContext(DbContextOptions<TaskDbContext> options)
			: base(options)
		{

		}

		public DbSet<Task> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>().HasKey(t => t.Id);
		}
	}
}
