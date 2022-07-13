using System;
using System.Threading.Tasks;

using Board.Domain.DTO.Users;

using Boards.Application.Commands.Auths;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class AccountController : AbstractApiController {
		private readonly IMediator _mediator;

		public AccountController(IMediator mediator) => _mediator = mediator;

		[HttpPost, AllowAnonymous]
		public Task Login([FromBody] LoginDTO user) => _mediator.Send(new LoginCommand(user));

		[HttpDelete]
		public Task Logout() => _mediator.Send(new LogoutCommand());

	}
}
