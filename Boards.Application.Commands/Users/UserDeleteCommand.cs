using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Application.Commands.Auths;
using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts.Users;

using FluentValidation;


using MediatR;

namespace Boards.Application.Commands.Users {
	public record UserDeleteCommand :  IRequest { }

	public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand> { }

	internal class UserDeleteCommandHandler : AbstractHandler<UserDeleteCommand, UserDeleteMsg, UserDeleteResponse> {
		private readonly IUserManager _userMgr;
		private readonly IMediator _mediator;

		//private readonly IBoardRepo _boardRepo;
		//private readonly IBoardItemRepo _itemRepo;

		public UserDeleteCommandHandler(IClient<UserDeleteMsg, UserDeleteResponse> client,  IUserManager userMgr, IMediator mediator) : base(client) {
			_userMgr = userMgr;
			_mediator = mediator;
		}

		protected override UserDeleteMsg GetMsg(UserDeleteCommand request) => new(_userMgr.CurrentUserId);  // TODO: logout

		protected override Task HandleResponse(UserDeleteResponse response, UserDeleteCommand request, CancellationToken token) => _mediator.Send(new LogoutCommand(), token);
	}
}
