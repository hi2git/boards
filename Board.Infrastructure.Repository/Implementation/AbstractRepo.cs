using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;

namespace Board.Infrastructure.Repository.Implementation {
	internal class AbstractRepo<T> : IRepo<T> where T : class {

		#region Ctors

		public AbstractRepo(BoardContext context) {
			context = context ?? throw new ArgumentNullException(nameof(context));
			this.EntityRepo = new EntityRepo<T>(context);
		}

		#endregion

		#region Public

		public virtual IQueryable<T> Query => this.QueryAll;

		public IQueryable<T> QueryAll => EntityRepo.Query();

		/// <inheritdoc/>
		public virtual Task<List<T>> GetAll(CancellationToken token) => EntityRepo.GetAll(token);

		/// <inheritdoc/>
		public virtual Task<T> Get(Guid id, CancellationToken token) => EntityRepo.Get(id, token);

		/// <inheritdoc/>
		public Task Update(T entity) {
			onSave(entity);
			return EntityRepo.Update(entity);
		}

		/// <inheritdoc/>
		protected virtual void onSave(T entity) { }

		/// <inheritdoc/>
		protected virtual void onAdd(T entity) { }

		/// <inheritdoc/>
		public Task Create(T entity) {
			onAdd(entity);
			return EntityRepo.Create(entity);
		}

		/// <inheritdoc/>
		public virtual Task Delete(T entity) => EntityRepo.Delete(entity);

		//public Task UpdateManyToMany<TKey>(IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, TKey> getKey) =>
		//	EntityRepo.UpdateManyToMany(currentItems, newItems, getKey);

		#endregion

		#region Protected

		protected private IEntityRepo<T> EntityRepo { get; }

		#endregion


	}
}
