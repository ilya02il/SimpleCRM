using System.Collections.Generic;
using SimpleCRM.Models;
using System.Threading.Tasks;

namespace SimpleCRM.Contracts
{
	public interface ITaskService
	{
		Task<int> Create(TaskModel task);
		List<TaskModel> GetAll();
		TaskModel Get(int id);
		Task Update(TaskModel task);
		Task Delete(int taskId);
	}
}
