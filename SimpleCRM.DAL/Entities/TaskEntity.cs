using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCRM.DAL.Entities
{
	public class TaskEntity : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Executors { get; set; }
		public string Description { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public double PlannedIntensity { get; set; }
		public TimeSpan ExecutionTime { get; set; }
		public DateTime CompletionDate { get; set; }
		public bool IsActive { get; set; }
		
		public int? ParentTaskId { get; set; }
		public TaskEntity ParentTask { get; set; }

		[Required]
		[ForeignKey("StateId")]
		public int StateId { get; set; }
		public StateEntity State { get; set; }

		public List<TaskEntity> Subtasks { get; set; }
	}
}
