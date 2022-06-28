using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Constants;
using Board.Domain.DTO;

using Boards.Application.Queries.Users;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {

	[Authorize(Roles = RoleNames.Admin)]
	public class UsersController : AbstractApiController {
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<IdNameDTO>> GetAll(CancellationToken token) => _mediator.Send(new UserGetAllQuery(), token);

	}
}
