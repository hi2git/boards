using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Boards.Front.Application.Commands.Posts;
using Boards.Front.Application.Queries.Posts;
using Boards.Commons.Domain.DTOs.Posts;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Boards.Commons.Domain.DTOs;

namespace Boards.Front.API.Controllers {
	public class PostsController : AbstractApiController {
		private readonly IMediator _mediator;

		public PostsController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<Pageable<PostDTO>> GetAll([FromQuery] PostFilter filter, CancellationToken token) => _mediator.Send(new PostGetAllQuery(filter), token);

		[HttpPut]
		public Task Sort([FromBody] SortDTO dto) => _mediator.Send(new PostSortAllCommand(dto.Id, dto.Items));

		public class SortDTO {
			public Guid Id { get; set; }

			public IEnumerable<PostDTO> Items { get; set; }
		}

	}


}
