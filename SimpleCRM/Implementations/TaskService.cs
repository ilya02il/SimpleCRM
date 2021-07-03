using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
			var taskEntity = _mapper.Map<TaskEntity>(task);
			var result = await _dbRepository.Add(taskEntity);
			//await _dbRepository.SaveChangesAsync();

			return result;
		}

		public List<TaskModel> GetAll()
		{
			var taskEntitiesCollection = _dbRepository.GetAll<TaskEntity>().ToList();
			var taskModelsCollection = new List<TaskModel>();

			foreach (var taskModel in _mapper.Map<List<TaskModel>>(taskEntitiesCollection))
			{
				if (taskModel.Subtasks.Count == 0) continue;

				var summarizedPlannedIntensity = taskModel.PlannedIntensity;
				var summarizedExecutionTime = taskModel.ExecutionTime;

				summarizedPlannedIntensity = taskModel.Subtasks.Aggregate(summarizedPlannedIntensity, (current, subtask) => current + subtask.PlannedIntensity);
				summarizedExecutionTime = taskModel.Subtasks.Aggregate(summarizedExecutionTime, (current, subtask) => current + subtask.ExecutionTime);

				taskModel.PlannedIntensity = summarizedPlannedIntensity;
				taskModel.ExecutionTime = summarizedExecutionTime;

				taskModelsCollection.Add(taskModel);
			}

			return taskModelsCollection;
		}

		public async Task<TaskModel> GetTask(int id)
		{
			var taskEntity = await _dbRepository.Get<TaskEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
			var taskModel = _mapper.Map<TaskModel>(taskEntity);

			return taskModel;
		}

		public async Task<SubtaskModel> GetSubtask(int id)
		{
			var subtaskEntity = await _dbRepository.Get<SubtaskEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
			var subtaskModel = _mapper.Map<SubtaskModel>(subtaskEntity);

			return subtaskModel;
		}

		public async Task Update(TaskModel task)
		{
			var innerTaskEntity = await _dbRepository.Get<TaskEntity>(t => t.Id == task.Id).FirstOrDefaultAsync();

			if (innerTaskEntity == null)
				return;

			switch (task.State.Status)
			{
				case "Finished" when innerTaskEntity.State.Status != "InProgress":
				case "Paused" when innerTaskEntity.State.Status != "InProgress":
				{
					return;
				}

				case "Finished":
				{
					foreach (var subtask in task.Subtasks)
					{
						subtask.State.Status = "Finished";
					}

					break;
				}
			}

			var taskEntity = _mapper.Map<TaskEntity>(task);

			await _dbRepository.Update(taskEntity);
		}

		public async Task Delete(TaskModel task)
		{
			var taskEntity = _mapper.Map<TaskEntity>(task);
			await _dbRepository.Remove(taskEntity);
		}
	}
}
