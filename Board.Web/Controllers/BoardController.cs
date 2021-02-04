using System;
using System.Threading.Tasks;

using Board.Domain.DTO;

using Boards.Application.Commands.Boards;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardController : AbstractApiController {
		private readonly IMediator _mediator;

		public BoardController(IMediator mediator) => _mediator = mediator;

		[HttpPost]
		public Task<Guid> Create([FromBody] Valueable<string> dto) => _mediator.Send(new BoardCreateCommand(dto.Value));

		[HttpPut]
		public Task Update([FromBody] IdNameDTO dto) => _mediator.Send(new BoardUpdateCommand(dto));

		[HttpDelete]
		public Task Delete([FromQuery] Guid id) => _mediator.Send(new BoardDeleteCommand(id));

	}
}
