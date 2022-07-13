using System;

using Board.Domain.Repos;

using Boards.Users.Domain.Models;

namespace Boards.Users.Domain.Repos {
	public interface IUserRepo : IRepo<User> {

		Task<User?> Get(string login, CancellationToken token);

		Task<bool> HasName(string name, CancellationToken token);

	}
}
