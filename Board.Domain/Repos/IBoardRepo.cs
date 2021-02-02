using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Board.Domain.Repos {
	public interface IBoardRepo : IRepo<Models.Board> {

		Task<List<Models.Board>> GetAll(Guid userId);

	}
}
