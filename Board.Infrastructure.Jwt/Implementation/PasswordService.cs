using System;

using Board.Domain.Services;

using Microsoft.AspNetCore.Identity;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class PasswordService : IPasswordService {
		private PasswordHasher<object> _hasher;

		public PasswordService() => _hasher = new PasswordHasher<object>();

		public string Hash(string password) => _hasher.HashPassword(null, password);

		public bool IsEqual(string password, string hashed) => _hasher.VerifyHashedPassword(null, hashed, password) == PasswordVerificationResult.Success;
	}
}
