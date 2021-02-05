using System;
using System.Threading.Tasks;

using Board.Domain.DTO.Users;
using Board.Domain.Models;

namespace Board.Application.Services {
	public interface IAuthService {

		Task Login(LoginDTO dto);

		Task Login(User user);

		Task Logout();

	}
}
