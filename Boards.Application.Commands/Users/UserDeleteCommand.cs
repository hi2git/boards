using System;

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

		//private readonly IBoardRepo _boardRepo;
		//private readonly IBoardItemRepo _itemRepo;

		public UserDeleteCommandHandler(IClient<UserDeleteMsg, UserDeleteResponse> client,  IUserManager userMgr) : base(client) {
			_userMgr = userMgr;
		}

		protected override UserDeleteMsg GetMsg(UserDeleteCommand request) => new(_userMgr.CurrentUserId);	// TODO: logout

		//public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken token) {

		//	await _authService.Logout();



		//	return Unit.Value;
		//}
	}
}
