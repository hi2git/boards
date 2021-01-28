using System;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Models;

namespace Board.Infrastructure.Jwt.Interfaces {
	public interface IAuthService {

		Task Login(LoginDTO dto);

		Task Login(User user);

		Task Logout();

	}
}
