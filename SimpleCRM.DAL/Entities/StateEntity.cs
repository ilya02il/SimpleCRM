using System.Collections.Generic;

namespace SimpleCRM.DAL.Entities
{
	public class StateEntity : IEntity
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public bool IsActive { get; set; }

		public virtual List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
	}
}
