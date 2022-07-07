using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Board.Domain.Repos {
	public interface IBoardRepo : IRepo<Models.Board> {

		//Task<Models.Board> GetWithItems(Guid id);

		Task<List<Models.Board>> GetAll(Guid userId);

		Task<bool> HasName(string name, Guid userId);

	}
}
