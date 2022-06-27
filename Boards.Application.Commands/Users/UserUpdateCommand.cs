using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Users;
using Board.Domain.Repos;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Users {
	public class UserUpdateCommand : IRequest {

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

	internal class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand> {
		private readonly IUserManager _userMgr;
		private readonly IPasswordService _pwdService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;

		public UserUpdateCommandHandler(IUserManager userMgr, IPasswordService pwdService, IUnitOfWork unitOfWork, IUserRepo userRepo) {
			_userMgr = userMgr;
			_pwdService = pwdService;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
		}

		public async Task<Unit> Handle(UserUpdateCommand request, CancellationToken token) {
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			//if (!item.IsPasswordChanged)
			//	return;

			var user = await _userRepo.Get(_userMgr.CurrentUserId, token);
			var isOldRight = _pwdService.IsEqual(item.OldPassword, user.Password);
			if (!isOldRight)
				throw new ArgumentException("Старый пароль введен неверно");

			user.Password = _pwdService.Hash(item.NewPassword);
			await _userRepo.Update(user);
			await _unitOfWork.Commit();

			return Unit.Value;
		}
	}
}
