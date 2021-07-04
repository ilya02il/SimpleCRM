using System;
using System.Collections.Generic;

namespace SimpleCRM.Models
{
	public class TaskModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Executors { get; set; }
		public string Description { get; set; }
		public DateTime RegistrationDate { get; set; }
		public double PlannedIntensity { get; set; }
		public TimeSpan ExecutionTime { get; set; }
		public DateTime CompletionDate { get; set; }
		public bool IsActive { get; set; }

		public int StateId { get; set; }
		public StateModel State { get; set; }

		public int? ParentTaskId { get; set; }
		public TaskModel ParentTask { get; set; }

		public List<SubtaskModel> Subtasks { get; set; }
	}
}
