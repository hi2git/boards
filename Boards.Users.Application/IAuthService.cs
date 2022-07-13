using System;

using Boards.Commons.Domain.DTOs.Jwt;
using Boards.Commons.Domain.DTOs.Users;
using Boards.Users.Domain.Models;

namespace Boards.Users.Application {
	public interface IAuthService {

		Task<JwtTokenDTO> Login(LoginDTO dto, AuthSettings auth, CancellationToken token);

		Task<JwtTokenDTO> Login(User user, AuthSettings auth);

	}
}
