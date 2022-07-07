using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	internal class EntityRepo<T> : IEntityRepo<T> where T : class {

		private readonly DbContext _context;

		public EntityRepo(DbContext context) => _context = context;

		#region Public

		public Task Create(T entity) => _context.Set<T>().AddAsync(entity).AsTask();

		public Task<T> Get(Guid id, CancellationToken token) => _context.Set<T>().FindAsync(new object[] { id }, token).AsTask();

		/// <inheritdoc />
		public Task Delete(T entity) => Task.Run(() => _context.Set<T>().Remove(entity));

		/// <inheritdoc />
		public IQueryable<T> Query(Expression<Func<T, bool>> predicate = null) =>
			 predicate != null ?
				_context.Set<T>().Where(predicate) :
				_context.Set<T>();

		/// <inheritdoc />
		public Task Update(T entity) => Task.Run(() => _context.Update(entity));

		/// <inheritdoc />
		public Task<List<T>> GetAll(CancellationToken token) => _context.Set<T>().ToListAsync(token);

	}

	#endregion
}
