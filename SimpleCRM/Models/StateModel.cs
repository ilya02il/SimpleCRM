using System.Collections.Generic;

namespace SimpleCRM.Models
{
	public class StateModel
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public bool IsActive { get; set; }

		public List<TaskModel> Tasks { get; set; }
	}
}
