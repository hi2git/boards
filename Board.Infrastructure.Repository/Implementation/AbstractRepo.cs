using System;
using System.Collections.Generic;
using System.Linq;
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

		public virtual IQueryable<T> Query => EntityRepo.Query();

		public IQueryable<T> QueryAll => EntityRepo.Query();

		/// <inheritdoc/>
		public virtual Task<List<T>> GetAll() => EntityRepo.GetAll();

		/// <inheritdoc/>
		public virtual Task<T> Get(Guid id) => EntityRepo.Get(id);

		/// <inheritdoc/>
		public async Task Update(T entity) {
			onSave(entity);
			await EntityRepo.Update(entity);
		}

		protected virtual void onSave(T entity) { }

		protected virtual void onAdd(T entity) { }
		/// <inheritdoc/>
		public async Task Create(T entity) {
			onAdd(entity);
			await EntityRepo.Create(entity);
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
