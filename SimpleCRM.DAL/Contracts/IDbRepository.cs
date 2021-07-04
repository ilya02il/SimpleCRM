using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SimpleCRM.DAL.Entities;

namespace SimpleCRM.DAL.Contracts
{
	public interface IDbRepository
	{
		IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
		IQueryable<T> Get<T>() where T : class, IEntity;
		IQueryable<T> GetAll<T>() where T : class, IEntity;

		int Add<T>(T newEntity) where T : class, IEntity;
		Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity;

		void Attach<T>(T entity) where T : class, IEntity;

		Task Remove<T>(T entity) where T : class, IEntity;
		Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity;

		Task Update<T>(T entity) where T : class, IEntity;
		Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity;

		Task<int> SaveChangesAsync();
    }
}
