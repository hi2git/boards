using System;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	public abstract class AbstractUnitOfWork : IUnitOfWork {
		private readonly DbContext _context;

		protected AbstractUnitOfWork(DbContext context) => _context = context;

		public Task Commit() => _context.Database.CreateExecutionStrategy().ExecuteAsync(this.RunTransaction);

		private async Task RunTransaction() {
			var transaction = _context.Database.BeginTransaction();
			try {
				await _context.SaveChangesAsync();
				transaction.Commit();
			}

			catch (Exception) {
				transaction.Rollback();
				throw;
			}
		}



	}
}
