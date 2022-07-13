using System;

namespace Boards.Users.Application {
	public interface IPasswordService {

		bool IsEqual(string password, string hashed);

		string Hash(string password);

	}
}
