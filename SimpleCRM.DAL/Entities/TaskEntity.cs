using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCRM.DAL.Entities
{
	public class TaskEntity : IEntity
	{
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
		public virtual TaskEntity ParentTask { get; set; }

		[Required]
		[ForeignKey("StateId")]
		public int StateId { get; set; }
		public virtual StateEntity State { get; set; }

		public virtual List<TaskEntity> Subtasks { get; set; }
	}

	public class TaskNode : IEntity
	{
		public int Id { get; set; }
		public bool IsActive { get; set; }

		public int AncestorId { get; set; }
		public virtual TaskEntity Ancestor { get; set; }

		public int OffspringId { get; set; }
		public virtual TaskEntity Offspring { get; set; }

		public int Separation { get; set; }
	}
}
