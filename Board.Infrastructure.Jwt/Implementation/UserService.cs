using System;

using Board.Domain.Services;

using Microsoft.AspNetCore.Identity;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class UserService : IUserService {
		private PasswordHasher<object> _hasher;

		public UserService() => _hasher = new PasswordHasher<object>();

		public string Hash(string password) => _hasher.HashPassword(null, password);

		public bool IsPasswordEqual(string password, string hashed) => _hasher.VerifyHashedPassword(null, hashed, password) == PasswordVerificationResult.Success;
	}
}
