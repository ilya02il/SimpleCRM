using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCRM.DAL.Entities;

namespace SimpleCRM.DAL.Implementations
{
	public class DataContext : DbContext
	{

		public DbSet<TaskEntity> Tasks { get; set; }
		public DbSet<SubtaskEntity> Subtasks { get; set; }
		public DbSet<StateEntity> States { get; set; }
		
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

		public DbSet<T> DbSet<T>() where T : class
		{
			return Set<T>();
		}

		public new IQueryable<T> Query<T>() where T : class
		{
			return Set<T>();
		}
    }
}
