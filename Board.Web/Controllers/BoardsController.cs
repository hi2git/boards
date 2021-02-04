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
		//private readonly IUserManager _userMgr;
		//private readonly IBoardRepo _repo;

		public BoardsController(IMediator mediator/*, IUserManager userMgr, IBoardRepo repo*/) {
			_mediator = mediator;
			//_userMgr = userMgr;
			//_repo = repo;
		}

		[HttpGet]
		public Task<IEnumerable<IdNameDTO>> GetAll() => _mediator.Send(new BoardGetAllQuery());

		//private IdNameDTO Map(Domain.Models.Board entity) => new IdNameDTO { Id = entity.Id, Name = entity.Name };

	}
}
