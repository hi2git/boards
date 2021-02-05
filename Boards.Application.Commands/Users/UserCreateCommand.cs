using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Application.Services;
using Board.Domain.DTO.Users;
using Board.Domain.Enums;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;

using BotDetect.Web;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Users {
	public class UserCreateCommand : IRequest {

		public UserCreateCommand(LoginDTO item) => this.Item = item;

		public LoginDTO Item { get; }
	}

	public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand> {

		public UserCreateCommandValidator(IUserRepo repo) {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Password).NotEmpty().MaximumLength(50);
			RuleFor(n => n.Item.CaptchaId).NotEmpty();
			RuleFor(n => n.Item.CaptchaCode).NotEmpty().MaximumLength(6);
			RuleFor(n => n.Item.Email).NotEmpty().MaximumLength(50).EmailAddress();
			RuleFor(n => n.Item.Login)
				.NotEmpty()
				.MaximumLength(50)
				.CustomAsync(async (n, context, _) => {
					if (await repo.HasName(n))
						context.AddFailure($"Имя пользователя {n} уже существует");
				});
			RuleFor(n => n.Item).Custom((n, context) => {
				if (!new SimpleCaptcha().Validate(n.CaptchaCode, n.CaptchaId)) {
					context.AddFailure("Введенное значение не соответствует изображению");
				}
			});
		}

	}

	internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommand> {
		private readonly IAuthService _authService;
		private readonly IPasswordService _pwdService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IRoleRepo _roleRepo;
		private readonly IBoardRepo _boardRepo;

		public UserCreateCommandHandler(IAuthService authService, IPasswordService pwdService, IUnitOfWork unitOfWork, IUserRepo userRepo, IRoleRepo roleRepo, IBoardRepo boardRepo) {
			_authService = authService;
			_pwdService = pwdService;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_roleRepo = roleRepo;
			_boardRepo = boardRepo;
		}

		public async Task<Unit> Handle(UserCreateCommand request, CancellationToken cancellationToken) {// Check Captcha
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));

			var passwordHash = _pwdService.Hash(item.Password);
			var role = await _roleRepo.Get(RoleEnum.User);

			var user = new User(Guid.NewGuid(), item.Login, passwordHash, role);
			await _userRepo.Create(user);

			var board = new Board.Domain.Models.Board(Guid.NewGuid(), "MyFirstDesk", user);

			await _boardRepo.Create(board);
			await _unitOfWork.Commit();

			await _authService.Login(user);

			return Unit.Value;
		}
	}
}
