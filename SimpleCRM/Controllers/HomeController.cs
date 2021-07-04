using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Contracts;
using SimpleCRM.Models;
using System.Threading.Tasks;

namespace SimpleCRM.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITaskService _taskService;

		public HomeController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		[HttpGet]
		public ViewResult Index()
		{
			var taskList = _taskService.GetAll();

			return View(taskList);
		}

		[HttpGet]
		public ActionResult GetTaskTree()
		{
			var taskList = _taskService.GetAll();

			return PartialView("_TaskTree", taskList);
		}

		[HttpGet]
		public ActionResult GetTaskInfo(int id)
		{
			var task = _taskService.Get(id);

			return PartialView("_TaskInfoContent", task);
		}

		[HttpPost]
		public async Task<ActionResult> CreateTask([FromBody]TaskModel task)
		{
			await _taskService.Create(task);

			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> EditTask([FromBody]TaskModel task)
		{
			await _taskService.Update(task);

			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteTask(int id)
		{
			await _taskService.Delete(id);

			return Ok();
		}

	}
}
