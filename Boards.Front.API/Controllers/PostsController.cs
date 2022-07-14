using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Boards.Front.Application.Commands.Posts;
using Boards.Front.Application.Queries.Posts;
using Boards.Commons.Domain.DTOs.Posts;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Boards.Front.API.Controllers {
	public class PostsController : AbstractApiController {
		private readonly IMediator _mediator;

		public PostsController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<PostDTO>> GetAll([FromQuery] Guid id, CancellationToken token) => _mediator.Send(new PostGetAllQuery(id), token);

		[HttpPut]
		public Task Sort([FromBody] SortDTO dto) => _mediator.Send(new PostSortAllCommand(dto.Id, dto.Items));

		public class SortDTO {
			public Guid Id { get; set; }

			public IEnumerable<PostDTO> Items { get; set; }
		}

	}


}
