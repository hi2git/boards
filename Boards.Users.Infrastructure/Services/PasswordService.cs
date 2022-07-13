using System;

using Boards.Users.Application;

using Microsoft.AspNetCore.Identity;

namespace Boards.Users.Infrastructure.Services {
	internal class PasswordService : IPasswordService {
		private PasswordHasher<object> _hasher;

		public PasswordService() => _hasher = new PasswordHasher<object>();

		public string Hash(string password) => _hasher.HashPassword(null, password);

		public bool IsEqual(string password, string hashed) => _hasher.VerifyHashedPassword(null, hashed, password) == PasswordVerificationResult.Success;
	}
}
