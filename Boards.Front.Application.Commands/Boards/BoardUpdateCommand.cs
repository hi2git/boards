using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Boards {
	public class BoardUpdateCommand : IRequest {

		public BoardUpdateCommand(IdNameDTO item) => this.Item = item;

		public IdNameDTO Item { get; }
	}

	public class BoardUpdateCommandValidator : AbstractValidator<BoardUpdateCommand> {

		public BoardUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			RuleFor(n => n.Item.Name).NotEmpty().MaximumLength(50);
		}
	}

	internal class BoardUpdateCommandHandler : AbstractHandler<BoardUpdateCommand, BoardUpdateMsg, BoardUpdateResponse> {
		private readonly IUserManager _userMgr;

		public BoardUpdateCommandHandler(IClient<BoardUpdateMsg, BoardUpdateResponse> client, ICacheService cache, IUserManager usrMgr) : base(client, cache) => _userMgr = usrMgr;

		protected override BoardUpdateMsg GetMsg(BoardUpdateCommand request) => new(request.Item, _userMgr.CurrentUserId);

		protected override string CacheKey(BoardUpdateCommand _) => _userMgr.UserKey;
	}
}
