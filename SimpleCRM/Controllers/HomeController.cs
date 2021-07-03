using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleCRM.Contracts;

namespace SimpleCRM.Controllers
{
	public class HomeController : Controller
	{
		List<TaskModel> testTaskList = new List<TaskModel>
		{
			new TaskModel
			{
				Id = 1,
				Name = "Task1",
				State = new StateModel {Id = 1},
				ExecutionTime = new TimeSpan(23, 15, 48, 0),
				PlannedIntensity = 0.57,
				Subtasks = new List<SubtaskModel>()
				{
					new SubtaskModel
					{
						Id = 1,
						Name = "Subtask1",
						State = new StateModel {Id = 1},
						ExecutionTime = new TimeSpan(23, 15, 48, 0),
						PlannedIntensity = 0.57,
					},
					new SubtaskModel
					{
						Id = 2,
						Name = "Subtask2",
						State = new StateModel {Id = 1},
						ExecutionTime = new TimeSpan(23, 15, 48, 0),
						PlannedIntensity = 0.57,
						Subtasks = new List<SubtaskModel>
						{
							new SubtaskModel()
							{
								Id = 3,
								Name = "Subtask1",
								State = new StateModel {Id = 1},
								ExecutionTime = new TimeSpan(23, 15, 48, 0),
								PlannedIntensity = 0.57,
							},
							new SubtaskModel()
							{
								Id = 4,
								Name = "Subtask2",
								State = new StateModel {Id = 1},
								ExecutionTime = new TimeSpan(23, 15, 48, 0),
								PlannedIntensity = 0.57,
							}
						}
					}
				}
			},
			new TaskModel
			{
				Id = 2,
				Name = "Task2",
				State = new StateModel {Id = 3},
				ExecutionTime = new TimeSpan(5, 27, 4, 0),
				PlannedIntensity = 2.578,
			},
			new TaskModel
			{
				Id = 3,
				Name = "Task3",
				State = new StateModel {Id = 2},
				ExecutionTime = new TimeSpan(7, 6, 14, 0),
				PlannedIntensity = 758.458,
			},
			new TaskModel
			{
				Id = 4,
				Name = "Task4",
				State = new StateModel {Id = 4},
				ExecutionTime = new TimeSpan(32, 7, 5, 0),
				PlannedIntensity = 7.2485,
			}
		};


		private readonly ITaskService _taskService;

		public HomeController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		public ViewResult Index()
		{
			var taskList = _taskService.GetAll();

			return View(taskList);
		}

		public async Task<ActionResult> GetTaskInfo(int id, string type)
		{
			//var result = testTaskList.FirstOrDefault(task => task.Id == id);
			object result;

			if (type == typeof(TaskModel).ToString())
			{
				result = await _taskService.GetTask(id);
				//Debug.WriteLine("TaskModel");
			}
			else
			{
				result = await _taskService.GetSubtask(id);
				//Debug.WriteLine("SubtaskModel");
			}

			return PartialView("_TaskInfoContent", result);
		}

		[HttpPost]
		public async Task<ActionResult> CreateTask(TaskModel task)
		{
			await _taskService.Create(task);

			return Ok();
		}
	}
}
