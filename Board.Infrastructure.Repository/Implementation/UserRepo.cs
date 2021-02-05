using System;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.Models;
using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	internal class UserRepo : AbstractRepo<User>, IUserRepo {
		public UserRepo(BoardContext context) : base(context) { }

		public Task<User> Get(string login) => this.Query
			.Include(n => n.Role)
			.FirstOrDefaultAsync(n => n.Name == login);

		public Task<User> GetWithItems(Guid id) => this.Query
			.Include(n => n.Boards)
			.ThenInclude(n => n.BoardItems)
			.FirstOrDefaultAsync(n => n.Id == id);

	}
}
