using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Board.Domain.Constants;
using Board.Domain.DTO;

using Boards.Application.Queries.Users;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {

	[Authorize(Roles = RoleNames.Admin)]
	public class UsersController : AbstractApiController {
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<IdNameDTO>> GetAll() => _mediator.Send(new UserGetAllQuery());

	}
}
