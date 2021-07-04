using Microsoft.EntityFrameworkCore;
using SimpleCRM.DAL.Entities;

namespace SimpleCRM.DAL.Implementations
{
	public class DataContext : DbContext
	{

		public DbSet<TaskEntity> Tasks { get; set; }
		//public DbSet<SubtaskEntity> Subtasks { get; set; }
		public DbSet<StateEntity> States { get; set; }
		
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TaskEntity>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.HasOne(x => x.ParentTask)
					.WithMany(x => x.Subtasks)
					.HasForeignKey(x => x.ParentTaskId)
					.IsRequired(false)
					.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<StateEntity>().HasData(
				new StateEntity { Id = 1, Status = "Assigned" },
				new StateEntity { Id = 2, Status = "InProgress" },
				new StateEntity { Id = 3, Status = "Paused" },
				new StateEntity { Id = 4, Status = "Finished" }
				);
		}
    }
}
