using System;
using System.Linq;

using Boards.Boards.Domain.Repos;
using Boards.Commons.Infrastructure.Repos;

using Microsoft.EntityFrameworkCore;

namespace Boards.Boards.Infrastructure.Repos {
	internal class BoardRepo : AbstractRepo<Domain.Models.Board>, IBoardRepo {
		public BoardRepo(BoardsContext context) : base(context) { }

		/// <inheritdoc/>
		public Task<List<Domain.Models.Board>> GetAll(Guid userId, CancellationToken token) => this.Query
			.Where(n => n.UserId == userId)
			.ToListAsync(token);

		/// <inheritdoc/>
		public Task<bool> HasName(string name, Guid userId, Guid exceptId, CancellationToken token) => 
			this.Query.AnyAsync(n => n.UserId == userId && n.Name == name && n.Id != exceptId, token);

	}
}
