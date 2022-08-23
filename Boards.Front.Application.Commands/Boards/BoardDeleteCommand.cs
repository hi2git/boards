using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts;
using Boards.Domain.Contracts.Boards;

using FluentValidation;


using MediatR;

namespace Boards.Front.Application.Commands.Boards {

	public record BoardDeleteCommand : AbstractMsg, IRequest {

		public BoardDeleteCommand(Guid id) : base(id) { }

	}

	public class BoardDeleteCommandValidator : AbstractValidator<BoardDeleteCommand> {

		public BoardDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteCommandHandler : AbstractBoardCommandHandler<BoardDeleteCommand, BoardDeleteMsg, BoardDeleteResponse> {

		public BoardDeleteCommandHandler(IClient<BoardDeleteMsg, BoardDeleteResponse> client, IUserManager usrMgr) : base(client, usrMgr) { }

		protected override BoardDeleteMsg GetMsg(BoardDeleteCommand request) => new(request.Id, this.UserId);
	}
}
