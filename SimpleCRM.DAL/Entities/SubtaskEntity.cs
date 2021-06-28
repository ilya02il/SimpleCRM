using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCRM.DAL.Entities
{
	public class SubtaskEntity : TaskEntity
	{
		[ForeignKey("ParentTaskId")]
		public TaskEntity ParentTask { get; set; }
	}
}
