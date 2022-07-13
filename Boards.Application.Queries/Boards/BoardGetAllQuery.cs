using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Queries.Boards {
	public class BoardGetAllQuery : IRequest<IEnumerable<IdNameDTO>> { }

	public class BoardGetAllQueryValidator : AbstractValidator<BoardGetAllQuery> { }

	internal class BoardGetAllQueryHandler : IRequestHandler<BoardGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IRequestClient<BoardGetAllMsg> _client;
		private readonly IUserManager _userMgr;
		//private readonly IBoardRepo _repo;

		public BoardGetAllQueryHandler(IRequestClient<BoardGetAllMsg> client, IUserManager userMgr) {
			_client = client;
			_userMgr = userMgr;
			//_repo = repo;
		}

		public async Task<IEnumerable<IdNameDTO>> Handle(BoardGetAllQuery request, CancellationToken token) {
			//var boards = await _repo.GetAll(_userMgr.CurrentUserId);
			//return boards.Select(this.Map);
			var msg = new BoardGetAllMsg(_userMgr.CurrentUserId);
			var response = await _client.GetResponse<BoardGetAllResponse>(msg, token);
			return response.Message.Items;
		}

		//private IdNameDTO Map(Board.Domain.Models.Board entity) => new() { Id = entity.Id, Name = entity.Name };
	}
}
