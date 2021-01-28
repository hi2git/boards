using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	internal class UnitOfWork : IUnitOfWork {

		private readonly BoardContext _context;
		//private readonly ILogger<UnitOfWorkService> _logger;

		public UnitOfWork(BoardContext context) { // , ILogger<UnitOfWorkService> logger
			_context = context;
			//_logger = logger;
		}

		public async Task Commit() {
			var strategy = _context.Database.CreateExecutionStrategy();
			await strategy.ExecuteAsync(async () => {
				using (var transaction = _context.Database.BeginTransaction()) {
					try {
						await _context.SaveChangesAsync();
						transaction.Commit();
					}
					catch (DbUpdateConcurrencyException ex) {
						//_logger.LogError(ex.ToString());
						transaction.Rollback();
						throw;
					}
					catch (Exception ex) {
						//_logger.LogError(ex.ToString());
						transaction.Rollback();
						throw;
					}
				}
			});
		}
	}
}
