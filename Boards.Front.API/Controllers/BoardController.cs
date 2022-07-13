using System;
using System.Threading.Tasks;

using Boards.Application.Commands.Boards;
using Boards.Commons.Domain.DTOs;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class BoardController : AbstractApiController {
		private readonly IMediator _mediator;

		public BoardController(IMediator mediator) => _mediator = mediator;

		[HttpPost]
		public Task Create([FromBody] Valueable<string> dto) => _mediator.Send(new BoardCreateCommand(dto.Value));

		[HttpPut]
		public Task Update([FromBody] IdNameDTO dto) => _mediator.Send(new BoardUpdateCommand(dto));

		[HttpDelete]
		public Task Delete([FromQuery] Guid id) => _mediator.Send(new BoardDeleteCommand(id));

	}
}
