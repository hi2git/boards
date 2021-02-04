using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;

using Boards.Application.Commands.BoardItems;
using Boards.Application.Queries.BoardItems;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardItemsController : AbstractApiController {
		private readonly IMediator _mediator;

		public BoardItemsController(IMediator mediator) => _mediator = mediator;

		[HttpGet]
		public Task<IEnumerable<BoardItemDTO>> GetAll([FromQuery] Guid id) => _mediator.Send(new BoardItemGetAllQuery(id));

		[HttpPut]
		public Task Sort([FromBody] SortDTO dto) => _mediator.Send(new BoardSortAllCommand(dto.Id, dto.Items));

		public class SortDTO {
			public Guid Id { get; set; }

			public IEnumerable<BoardItemDTO> Items { get; set; }
		}

	}


}
