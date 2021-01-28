using System;

namespace Board.Domain.Services {
	public interface IPasswordService {

		bool IsEqual(string password, string hashed);

		string Hash(string password);

	}
}
