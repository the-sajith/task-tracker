using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_tracker.Data;
using Task = task_tracker.Models.Task;

namespace task_tracker.Controllers
{
	[Route("tasks")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly TaskDbContext _dbContext;
		public TasksController(TaskDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IEnumerable<Task> GetTasks()
		{
			var tasks = _dbContext.Tasks.ToList();

			return tasks;
		}

		[HttpGet("{id}")]
		public ActionResult<Task> Get(int id)
		{
			var task = _dbContext.Tasks
				.Where(t => t.Id == id)
				.FirstOrDefault();

			if (task == null)
			{
				return NotFound();
			}

			return Ok(task);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Task task)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_dbContext.Tasks.Add(task);
			_dbContext.SaveChanges();

			return Created(string.Empty, task);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Task model)
		{
			var task = _dbContext.Tasks
				.Find(id);

			if (task == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			task.Text = model.Text;
			task.Day = model.Day;
			task.Reminder = model.Reminder;

			_dbContext.Tasks.Update(task);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var task = _dbContext.Tasks
				.Where(t => t.Id == id)
				.FirstOrDefault();

			if (task == null)
			{
				return NotFound();
			}

			_dbContext.Tasks.Remove(task);
			_dbContext.SaveChanges();

			return Ok();
		}

	}
}
