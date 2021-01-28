using System;

namespace Board.Domain.Services {
	public interface IUserService {

		bool IsPasswordEqual(string password, string hashed);

		string Hash(string password);

	}
}
