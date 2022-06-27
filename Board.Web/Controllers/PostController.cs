using System;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

using Boards.Application.Commands.Posts;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class PostController : AbstractApiController {
		private readonly IMediator _mediator;

		public PostController(IMediator mediator) => _mediator = mediator;

		[HttpPost]
		public Task Create([FromBody] IdItemDTO dto) => _mediator.Send(new PostCreateCommand(dto.Id, dto.Item));

		[HttpPut("content")]
		public Task UpdateContent([FromBody] IdItemDTO dto) => _mediator.Send(new PostContentUpdateCommand(dto.Item));

		[HttpPut]
		public Task Update([FromBody] IdItemDTO dto) => _mediator.Send(new PostUpdateCommand(dto.Item));

		[HttpDelete]
		public Task Delete([FromQuery] IdItemDTO<Guid> dto) => _mediator.Send(new PostDeleteCommand(dto.Id, dto.Item));

	}
}
