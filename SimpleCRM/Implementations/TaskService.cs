using System;
using AutoMapper;
using SimpleCRM.Contracts;
using SimpleCRM.DAL.Contracts;
using SimpleCRM.DAL.Entities;
using SimpleCRM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRM.Implementations
{
	public class TaskService : ITaskService
	{
		private readonly IDbRepository _dbRepository;
		private readonly IMapper _mapper;

		public TaskService(IDbRepository dbRepository, IMapper mapper)
		{
			_dbRepository = dbRepository;
			_mapper = mapper;
		}

		public async Task<int> Create(TaskModel task)
		{
			if (task.ParentTaskId == 0)
			{
				task.ParentTaskId = null;
			}

			var taskEntity = _mapper.Map<TaskEntity>(task);
			//_dbRepository.Attach(taskEntity.State);
			var result = _dbRepository.Add(taskEntity);
			await _dbRepository.SaveChangesAsync();

			return result;
		}

		public List<TaskModel> GetAll()
		{
			var taskEntitiesCollection = _dbRepository.GetAllInclude<TaskEntity>(task => task.Subtasks, task => task.State).ToList();
			var taskModelsCollection = new List<TaskModel>();

			if (taskEntitiesCollection.Count == 0)
				throw new Exception("Task list is empty.");

			foreach (var taskEntity in taskEntitiesCollection)
			{
				var taskModel = _mapper.Map<TaskModel>(taskEntity);

				if (taskModel.ParentTask != null)
				{
					continue;
				}

				taskModelsCollection.Add(taskModel);
			}

			return taskModelsCollection;
		}

		public TaskModel Get(int id)
		{
			var taskEntity = _dbRepository.GetAllInclude<TaskEntity>(task => task.State, task => task.Subtasks)
				.FirstOrDefault(entity => entity.Id == id);

			if (taskEntity == null)
				throw new NullReferenceException("Task entity from database is null.");

			var taskModel = _mapper.Map<TaskModel>(taskEntity);

			return taskModel;
		}

		public async Task Update(TaskModel task)
		{
			var innerTaskEntity = _dbRepository.GetAllInclude<TaskEntity>(t => t.State, t=> t.Subtasks).FirstOrDefault(t => t.Id == task.Id);

			if (innerTaskEntity == null)
				throw new NullReferenceException("Task entity from database is null.");

			switch (task.StateId)
			{
				case 3 when innerTaskEntity.State.Status != "InProgress":
				{
					throw new ArgumentException("You cannot change the status value to \"Paused\" if the task doesn't have the status \"In Progress\"");
				}

				case 4:
				{
					if (innerTaskEntity.State.Status != "InProgress")
					{
						throw new ArgumentException("You cannot change the status value to \"Finished\" if the task doesn't have the status \"In Progress\"");
					}

					foreach (var subtask in innerTaskEntity.Subtasks)
					{
						//subtask.State.Status = "Finished";
					}
					break;
				}
			}

			var taskEntity = _mapper.Map<TaskEntity>(task);

			await _dbRepository.Update(taskEntity);
			await _dbRepository.SaveChangesAsync();
		}

		public async Task Delete(int taskId)
		{
			var innerTaskEntity = _dbRepository.GetAll<TaskEntity>().FirstOrDefault(task => task.Id == taskId);

			if (innerTaskEntity == null)
				throw new NullReferenceException("Task entity from database is null.");

			await _dbRepository.Remove(innerTaskEntity);
			await _dbRepository.SaveChangesAsync();
		}
	}
}
