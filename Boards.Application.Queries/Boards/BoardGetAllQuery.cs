using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Repos;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Queries.Boards {
	public class BoardGetAllQuery : IRequest<IEnumerable<IdNameDTO>> { }

	public class BoardGetAllQueryValidator : AbstractValidator<BoardGetAllQuery> { }

	internal class BoardGetAllQueryHandler : IRequestHandler<BoardGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IUserManager _userMgr;
		private readonly IBoardRepo _repo;

		public BoardGetAllQueryHandler(IUserManager userMgr, IBoardRepo repo) {
			_userMgr = userMgr;
			_repo = repo;
		}

		public async Task<IEnumerable<IdNameDTO>> Handle(BoardGetAllQuery request, CancellationToken cancellationToken) {
			var boards = await _repo.GetAll(_userMgr.CurrentUserId);
			return boards.Select(this.Map);
		}

		private IdNameDTO Map(Board.Domain.Models.Board entity) => new IdNameDTO { Id = entity.Id, Name = entity.Name };
	}
}
