using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.Models;
using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	internal class BoardItemRepo : AbstractRepo<BoardItem>, IBoardItemRepo {
		public BoardItemRepo(BoardContext context) : base(context) { }

		public Task<List<BoardItem>> GetAll(Guid userId) => this.Query			.Where(n => n.UserId == userId)
			.ToListAsync();
	}
}
