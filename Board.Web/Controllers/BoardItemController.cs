using System;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;

using Boards.Application.Commands.BoardItems;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardItemController : AbstractApiController {
		private readonly IMediator _mediator;

		public BoardItemController(IMediator mediator) => _mediator = mediator;

		[HttpPost]
		public Task Create([FromBody] IdItemDTO dto) => _mediator.Send(new BoardItemCreateCommand(dto.Id, dto.Item));

		[HttpPut("content")]
		public Task UpdateContent([FromBody] IdItemDTO dto) => _mediator.Send(new BoardItemContentUpdateCommand(dto.Item));

		[HttpPut]
		public Task Update([FromBody] IdItemDTO dto) => _mediator.Send(new BoardItemUpdateCommand(dto.Item));

		[HttpDelete]
		public Task Delete([FromQuery] IdItemDTO<Guid> dto) => _mediator.Send(new BoardItemDeleteCommand(dto.Item));

	}
}
