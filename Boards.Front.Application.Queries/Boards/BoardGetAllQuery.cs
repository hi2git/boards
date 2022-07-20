using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Queries.Boards {
	public class BoardGetAllQuery : IRequest<IEnumerable<IdNameDTO>> { }

	public class BoardGetAllQueryValidator : AbstractValidator<BoardGetAllQuery> { }

	internal class BoardGetAllQueryHandler : IRequestHandler<BoardGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IClient<BoardGetAllMsg, BoardGetAllResponse> _client;
		private readonly IUserManager _userMgr;

		public BoardGetAllQueryHandler(IClient<BoardGetAllMsg, BoardGetAllResponse> client, IUserManager userMgr) {
			_client = client;
			_userMgr = userMgr;
		}

		public async Task<IEnumerable<IdNameDTO>> Handle(BoardGetAllQuery request, CancellationToken token) {
			var msg = new BoardGetAllMsg(_userMgr.CurrentUserId);
			var response = await _client.Send(msg, token);
			return response.Items;
		}

	}
}
