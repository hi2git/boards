using System;
using System.Linq;

using Board.Infrastructure.Repository;

using Boards.Posts.Infrastructure;
using Boards.Users.Domain.Models;
using Boards.Users.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Boards.Users.Infrastructure.Repos {
	internal class UserRepo : AbstractRepo<User>, IUserRepo {
		public UserRepo(UsersContext context) : base(context) { }

		public Task<User?> Get(string login, CancellationToken token) => this.Query.FirstOrDefaultAsync(n => n.Name == login, token);

		public Task<bool> HasName(string name, CancellationToken token) => this.Query.AnyAsync(n => n.Name == name, token);
	}
}
