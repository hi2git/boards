﻿using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts.Boards;

using FluentValidation;


using MediatR;

namespace Boards.Front.Application.Commands.Boards {

	public record BoardDeleteCommand : BoardDeleteMsg, IRequest {

		public BoardDeleteCommand(Guid id) : base(id) { }

	}

	public class BoardDeleteCommandValidator : AbstractValidator<BoardDeleteCommand> {

		public BoardDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteCommandHandler : AbstractHandler<BoardDeleteCommand, BoardDeleteMsg, BoardDeleteResponse> {
		private readonly IUserManager _userMgr;

		public BoardDeleteCommandHandler(IClient<BoardDeleteMsg, BoardDeleteResponse> client, ICacheService cache, IUserManager usrMgr) : base(client, cache) => _userMgr = usrMgr;

		protected override string CacheKey => _userMgr.UserKey;
	}
}
