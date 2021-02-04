using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Board.Domain.DTO;

using Boards.Application.Queries.Boards;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardsController : AbstractApiController {
		private readonly IMediator _mediator;

		public BoardsController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<IdNameDTO>> GetAll() => _mediator.Send(new BoardGetAllQuery());

	}
}
