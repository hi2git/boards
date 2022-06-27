using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

using Boards.Application.Commands.Posts;
using Boards.Application.Queries.BoardItems;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class PostsController : AbstractApiController {
		private readonly IMediator _mediator;

		public PostsController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<PostDTO>> GetAll([FromQuery] Guid id) => _mediator.Send(new BoardItemGetAllQuery(id));

		[HttpPut]
		public Task Sort([FromBody] SortDTO dto) => _mediator.Send(new PostSortAllCommand(dto.Id, dto.Items));

		public class SortDTO {
			public Guid Id { get; set; }

			public IEnumerable<PostDTO> Items { get; set; }
		}

	}


}
