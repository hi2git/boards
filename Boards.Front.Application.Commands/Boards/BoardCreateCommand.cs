using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Boards {
	public class BoardCreateCommand : IRequest {

		public BoardCreateCommand(string name) => this.Name = name;

		public string Name { get; }
	}

	public class BoardCreateCommandValidator : AbstractValidator<BoardCreateCommand> {

		public BoardCreateCommandValidator() {
			RuleFor(n => n.Name).NotEmpty().MaximumLength(50);
		}
	}

	internal class BoardCreateCommandHandler : AbstractHandler<BoardCreateCommand, BoardCreateMsg, BoardCreateResponse> {
		private readonly IUserManager _userMgr;

		public BoardCreateCommandHandler(IClient<BoardCreateMsg, BoardCreateResponse> client, IUserManager userMgr) : base(client) => _userMgr = userMgr;

		protected override BoardCreateMsg GetMsg(BoardCreateCommand request) => new(_userMgr.CurrentUserId, request.Name);

	}
}
