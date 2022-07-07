using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	internal class BoardRepo : AbstractRepo<Domain.Models.Board>, IBoardRepo {
		public BoardRepo(BoardContext context) : base(context) { }

		//public Task<Domain.Models.Board> GetWithItems(Guid id) => this.Query
		//	.Include(n => n.BoardItems)
		//	.FirstOrDefaultAsync(n => n.Id == id);

		public Task<List<Domain.Models.Board>> GetAll(Guid userId) => this.Query
			.Where(n => n.UserId == userId)
			.ToListAsync();

		public Task<bool> HasName(string name, Guid userId) => this.Query
			.AnyAsync(n => n.UserId == userId && n.Name == name);

	}
}
