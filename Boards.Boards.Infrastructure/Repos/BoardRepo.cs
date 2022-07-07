using System;
using System.Linq;

using Board.Infrastructure.Repository.Implementation;

using Boards.Boards.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Boards.Boards.Infrastructure.Repos {
	internal class BoardRepo : AbstractRepo<Domain.Models.Board>, IBoardRepo {
		public BoardRepo(BoardsContext context) : base(context) { }

		/// <inheritdoc/>
		public Task<List<Domain.Models.Board>> GetAll(Guid userId, CancellationToken token) => this.Query
			.Where(n => n.UserId == userId)
			.ToListAsync(token);

		/// <inheritdoc/>
		public Task<bool> HasName(string name, Guid userId, CancellationToken token) => this.Query.AnyAsync(n => n.UserId == userId && n.Name == name, token);

	}
}
