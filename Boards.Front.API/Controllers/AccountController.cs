using System;
using System.Threading.Tasks;

using Board.Application.Services;
using Board.Domain.DTO.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class AccountController : AbstractApiController {
		private readonly IAuthService _authService;

		public AccountController(IAuthService authService) => _authService = authService;


		[HttpPost, AllowAnonymous]
		public Task Login([FromBody] LoginDTO user) => _authService.Login(user);

		[HttpDelete]
		public Task Logout() => _authService.Logout();

	}
}
