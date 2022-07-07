using System;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	public abstract class AbstractUnitOfWork : IUnitOfWork {
		private readonly DbContext _context;

		protected AbstractUnitOfWork(DbContext context) => _context = context;

		public Task Commit(Func<Task>? action = null) => _context.Database
			.CreateExecutionStrategy()
			.ExecuteAsync(() => this.RunTransaction(action));

		private async Task RunTransaction(Func<Task>? action) {
			var transaction = _context.Database.BeginTransaction();
			try {
				await _context.SaveChangesAsync();
				transaction.Commit();
				await (action?.Invoke() ?? Task.CompletedTask);
			}

			catch (Exception) {
				transaction.Rollback();
				throw;
			}
		}



	}
}
