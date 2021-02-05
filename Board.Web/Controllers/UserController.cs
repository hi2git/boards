using System;
using System.Threading.Tasks;

using Board.Domain.DTO.Users;

using Boards.Application.Commands.Users;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class UserController : AbstractApiController {
		private readonly IMediator _mediator;

		public UserController(IMediator mediator) => _mediator = mediator;

		[HttpPost, AllowAnonymous]
		public Task Create([FromBody] LoginDTO dto) => _mediator.Send(new UserCreateCommand(dto));


		[HttpPut]
		public Task Update([FromBody] UserSettingsDTO dto) => _mediator.Send(new UserUpdateCommand(dto));

	}


}
