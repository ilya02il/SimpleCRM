using System.Collections.Generic;
using SimpleCRM.Models;
using System.Threading.Tasks;

namespace SimpleCRM.Contracts
{
	public interface ITaskService
	{
		Task<int> Create(TaskModel task);
		List<TaskModel> GetAll();
		Task<TaskModel> GetTask(int id);
		Task<SubtaskModel> GetSubtask(int id);
		Task Update(TaskModel task);
		Task Delete(TaskModel task);
	}
}
