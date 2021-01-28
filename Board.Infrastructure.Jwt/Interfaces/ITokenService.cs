using System;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Infrastructure.Jwt.Models;

namespace Board.Infrastructure.Jwt.Interfaces {
	internal interface ITokenService {
		Task<JwtTokenResult> Generate(UserLoginDTO user);

	}
}
