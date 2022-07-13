using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Models;
using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository {
	public abstract class AbstractRepo<T> : IRepo<T> where T : Entity<Guid> { 
		private readonly DbContext _context;

		#region Ctors

		public AbstractRepo(DbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

		#endregion

		#region Public

		/// <inheritdoc/>
		public virtual Task<List<T>> GetAll(CancellationToken token) => this.Items.ToListAsync(token);

		/// <inheritdoc/>
		public virtual Task<T> Get(Guid id, CancellationToken token) => this.Items.FindAsync(new object[] { id }, token).AsTask();

		/// <inheritdoc/>
		public Task Update(T entity) => Task.Run(() => this.Items.Update(entity));

		/// <inheritdoc/>
		public Task Create(T entity) => this.Items.AddAsync(entity).AsTask();

		/// <inheritdoc/>
		public virtual Task Delete(T entity) => Task.Run(() => this.Items.Remove(entity));

		#endregion

		#region Protected

		protected virtual IQueryable<T> Query => this.QueryAll;

		protected IQueryable<T> QueryAll => _context.Set<T>();

		#endregion

		#region Private

		private DbSet<T> Items => _context.Set<T>();

		#endregion


	}
}
