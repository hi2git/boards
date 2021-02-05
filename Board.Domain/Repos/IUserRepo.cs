﻿using System;
using System.Threading.Tasks;

using Board.Domain.Models;

namespace Board.Domain.Repos {
	public interface IUserRepo : IRepo<User> {

		Task<User> Get(string login);

		Task<User> GetWithItems(Guid id);

		Task<bool> HasName(string name);

	}
}
