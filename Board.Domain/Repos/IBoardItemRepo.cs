using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Board.Domain.Models;

namespace Board.Domain.Repos {
	public interface IBoardItemRepo : IRepo<BoardItem> {

		Task<List<BoardItem>> GetAll(Guid userId);

	}
}
