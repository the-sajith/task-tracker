using System.ComponentModel.DataAnnotations;

namespace task_tracker.Models
{
	public class Task
	{
		[Key]
		public int Id { get; set; }

		public string Text { get; set; } = string.Empty;

		public int Day { get; set; }

		public bool Reminder { get; set; }
	}
}
