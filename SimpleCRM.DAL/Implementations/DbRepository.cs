using SimpleCRM.DAL.Contracts;
using SimpleCRM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleCRM.DAL.Implementations
{
	public class DbRepository : IDbRepository
	{
        private readonly DataContext _context;

        public DbRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get<T>() where T : class, IEntity
        {
            return _context.Set<T>().Where(x => x.IsActive).AsQueryable();
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return _context.Set<T>().Where(selector).Where(x => x.IsActive).AsQueryable();
        }

        public int Add<T>(T newEntity) where T : class, IEntity
        {
            var entity = _context.Set<T>().Add(newEntity);
            return entity.Entity.Id;
        }

        public async Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity
        {
            await _context.Set<T>().AddRangeAsync(newEntities);
        }

        public void Attach<T>(T entity) where T : class, IEntity
        {
	        _context.Set<T>().Attach(entity);
        }

        public async Task Remove<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }

        public async Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().RemoveRange(entities));
        }

        public async Task Update<T>(T entity) where T : class, IEntity
        {
	        var innerEntity = await _context.Set<T>().FindAsync(entity.Id);

	        if (innerEntity == null) return;

	        foreach (var property in entity.GetType().GetProperties())
	        {
		        if (property.GetValue(entity) == null)
			        property.SetValue(entity, property.GetValue(innerEntity));
	        }

	        await Task.Run(() => _context.Entry(innerEntity).CurrentValues.SetValues(entity));
        }

        public async Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().UpdateRange(entities));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
