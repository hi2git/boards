using System;

using Board.Domain.DTO.Jwt;
using Board.Domain.DTO.Users;

using Boards.Users.Domain.Models;

namespace Boards.Users.Application {
	public interface IAuthService {

		Task<JwtTokenResult> Login(LoginDTO dto, AuthSettings auth, CancellationToken token);

		Task<JwtTokenResult> Login(User user, AuthSettings auth);

	}
}
