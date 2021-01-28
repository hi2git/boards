using System;
using System.Threading.Tasks;

using Board.Domain.DTO;

namespace Board.Infrastructure.Jwt.Interfaces {
	public interface IAuthService {

		Task Login(LoginDTO dto);

		Task Logout();

	}
}
