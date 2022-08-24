using System;

using Board.Domain.Repos;

namespace Boards.Boards.Domain.Repos {
	public interface IBoardRepo : IRepo<Models.Board> {

		Task<List<Models.Board>> GetAll(Guid userId, CancellationToken token);

		Task<bool> HasName(string name, Guid userId, Guid exceptId, CancellationToken token);

	}
}
