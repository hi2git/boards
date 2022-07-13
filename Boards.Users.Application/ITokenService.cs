using System;

using Boards.Commons.Domain.DTOs.Jwt;
using Boards.Commons.Domain.DTOs.Users;

namespace Boards.Users.Application {
	public interface ITokenService {
		Task<JwtTokenDTO> Generate(UserLoginDTO user, AuthSettings auth);

	}
}
