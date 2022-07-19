using System;

using Boards.Commons.Domain.DTOs.Jwt;
using Boards.Commons.Domain.DTOs.Users;
using Boards.Users.Domain.Models;

namespace Boards.Users.Application {
	public interface IAuthService {

		Task<JwtTokenDTO> GetToken(LoginDTO dto, AuthSettings auth, CancellationToken token);

		Task<JwtTokenDTO> GetToken(User user, AuthSettings auth);

	}
}
