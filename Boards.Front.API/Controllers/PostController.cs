using System;
using System.Threading.Tasks;

using Boards.Front.Application.Commands.Posts;
using Boards.Commons.Domain.DTOs.Posts;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class PostController : AbstractApiController {
		private readonly IMediator _mediator;

		public PostController(IMediator mediator) => _mediator = mediator;

		[HttpPost]
		public Task Create([FromBody] IdItemDTO dto) => _mediator.Send(new PostCreateCommand(dto.Id, dto.Item));

		//[HttpPut("content")]
		//public Task UpdateContent([FromBody] IdItemDTO dto) => _mediator.Send(new PostContentUpdateCommand(dto.Item));

		[HttpPut]
		public Task Update([FromBody] IdItemDTO dto) => _mediator.Send(new PostUpdateCommand(dto.Id, dto.Item));

		[HttpDelete]
		public Task Delete([FromQuery] IdItemDTO<Guid> dto) => _mediator.Send(new PostDeleteCommand(dto.Id, dto.Item));

	}
}
