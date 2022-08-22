using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Users;
using Boards.Domain.Contracts.Users;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Commands.Users {
	public record UserUpdateCommand : IRequest {

		public UserUpdateCommand(UserSettingsDTO item) => this.Item = item;

		public UserSettingsDTO Item { get; }
	}

	public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand> {

		public UserUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.IsPasswordChanged).NotEmpty();
			RuleFor(n => n.Item.OldPassword).NotEmpty().MaximumLength(50);
			RuleFor(n => n.Item.NewPassword).NotEmpty().MaximumLength(50);
		}

	}

	internal class UserUpdateCommandHandler : AbstractHandler<UserUpdateCommand, UserUpdateMsg, UserUpdateResponse> {
		private readonly IUserManager _userMgr;

		public UserUpdateCommandHandler(IClient<UserUpdateMsg, UserUpdateResponse> client, IUserManager userMgr/*, ICacheService cache*/) : base(client/*, cache*/) => _userMgr = userMgr;

		protected override UserUpdateMsg GetMsg(UserUpdateCommand request) => new(_userMgr.CurrentUserId, request.Item);

		//protected override string CacheKey(UserUpdateCommand _) => "all_users";
	}
}
