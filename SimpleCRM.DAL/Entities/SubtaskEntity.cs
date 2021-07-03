using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCRM.DAL.Entities
{
	public class SubtaskEntity : IEntity
	{
		[ForeignKey(nameof(Task))]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Executors { get; set; }
		public string Description { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public double PlannedIntensity { get; set; }
		public TimeSpan ExecutionTime { get; set; }
		public DateTime CompletionDate { get; set; }
		public bool IsActive { get; set; }
		[NotMapped]
		public TaskEntity Task { get; set; }

		[Required]
		[ForeignKey("StateId")]
		public int StateId { get; set; }
		public StateEntity State { get; set; }

		public List<SubtaskEntity> Subtasks { get; set; }
		[Required]
		[ForeignKey("ParentTaskId")]
		public TaskEntity ParentTask { get; set; }
	}
}
