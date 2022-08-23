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

	internal class BoardUpdateCommandHandler : AbstractBoardCommandHandler<BoardUpdateCommand, BoardUpdateMsg, BoardUpdateResponse> {

		public BoardUpdateCommandHandler(IClient<BoardUpdateMsg, BoardUpdateResponse> client, IUserManager usrMgr) : base(client, usrMgr) { }

		protected override BoardUpdateMsg GetMsg(BoardUpdateCommand request) => new(request.Item, this.UserId);

	}
}
