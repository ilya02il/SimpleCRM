using System;
using System.Diagnostics;
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
		public ActionResult Index()
		{
			try
			{
				var taskList = _taskService.GetAll();

				return View(taskList);
			}
			catch (Exception exp)
			{
				Debug.WriteLine(exp);
				return BadRequest("Tasks was not found");
			}
		}

		[HttpGet]
		public ActionResult GetTaskTree()
		{
			try
			{
				var taskList = _taskService.GetAll();
				return PartialView("_TaskTree", taskList);
			}
			catch (Exception exp)
			{
				Debug.WriteLine(exp);
				return BadRequest("Tasks was not found");
			}
		}

		[HttpGet]
		public ActionResult GetTaskInfo(int id)
		{
			try
			{
				var task = _taskService.Get(id);
				return PartialView("_TaskInfoContent", task);
			}
			catch (Exception exp)
			{
				Debug.WriteLine(exp);
				return BadRequest("Task was not found");
			}
		}

		[HttpPost]
		public async Task<ActionResult> CreateTask([FromBody]TaskModel task)
		{
			try
			{
				await _taskService.Create(task);
				return Ok();
			}
			catch (Exception exp)
			{
				Debug.WriteLine(exp);
				return BadRequest("Tasks was not created");
			}
		}

		[HttpPut]
		public async Task<ActionResult> EditTask([FromBody]TaskModel task)
		{
			try
			{
				await _taskService.Update(task);
				return Ok();
			}
			catch (Exception exp)
			{
				Debug.WriteLine(exp);
				return BadRequest("Tasks was not edited");
			}
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteTask(int id)
		{
			try
			{
				await _taskService.Delete(id);
				return Ok();
			}
			catch (Exception exp)
			{
				Debug.WriteLine(exp);
				return BadRequest("Tasks was not deleted");
			}
		}

	}
}
