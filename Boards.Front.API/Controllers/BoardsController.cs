using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO;

using Boards.Application.Queries.Boards;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class BoardsController : AbstractApiController {
		private readonly IMediator _mediator;

		public BoardsController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<IdNameDTO>> GetAll(CancellationToken token) => _mediator.Send(new BoardGetAllQuery(), token);

	}
}
