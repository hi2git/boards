using System;
using System.Linq;

using Board.Domain.Repos;

using Boards.Domain.Contracts.Users;
using Boards.Users.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Users.Application.Commands {
	public record UserUpdateCommand : UserUpdateMsg, IRequest {

		public UserUpdateCommand(UserUpdateMsg msg) : base(msg.Id, msg.Item) { }
	}

	public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand> {

		public UserUpdateCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.IsPasswordChanged).NotEmpty();
			RuleFor(n => n.Item.OldPassword).NotEmpty().MaximumLength(50);
			RuleFor(n => n.Item.NewPassword).NotEmpty().MaximumLength(50);
		}

	}

	internal class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand> {
		private readonly IPasswordService _pwdService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;

		public UserUpdateCommandHandler(IPasswordService pwdService, IUnitOfWork unitOfWork, IUserRepo userRepo) {
			_pwdService = pwdService;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
		}

		public async Task<Unit> Handle(UserUpdateCommand request, CancellationToken token) {
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			//if (!item.IsPasswordChanged)
			//	return;

			var user = await _userRepo.Get(request.Id, token);
			var isOldRight = _pwdService.IsEqual(item.OldPassword, user.Password);
			if (!isOldRight)
				throw new ArgumentException("Старый пароль введен неверно");

			user.Password = _pwdService.Hash(item.NewPassword);
			await _userRepo.Update(user);
			await _unitOfWork.Commit();	// Publish UserUpdatedEvent

			return Unit.Value;
		}
	}
}
