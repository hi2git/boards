using System;

using Board.Domain.DTO.Jwt;
using Board.Domain.DTO.Users;

namespace Boards.Users.Application {
	public interface ITokenService {
		Task<JwtTokenResult> Generate(UserLoginDTO user, AuthSettings auth);

	}
}
