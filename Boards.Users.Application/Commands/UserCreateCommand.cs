using System;
using System.Linq;

using Board.Domain.Enums;
using Board.Domain.Repos;

using Boards.Domain.Contracts.Users;
using Boards.Users.Domain.Models;
using Boards.Users.Domain.Repos;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Users.Application.Commands {
	public record UserCreateCommand : UserCreateMsg, IRequest {

		public UserCreateCommand(UserCreateMsg item) : base(item.Item) { }

	}

	public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand> {

		public UserCreateCommandValidator(IUserRepo repo) {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Password).NotEmpty().MaximumLength(50);
			//RuleFor(n => n.Item.CaptchaId).NotEmpty();
			//RuleFor(n => n.Item.CaptchaCode).NotEmpty().MaximumLength(6);
			RuleFor(n => n.Item.Email).NotEmpty().MaximumLength(50).EmailAddress();
			RuleFor(n => n.Item.Login)
				.NotEmpty()
				.MaximumLength(50)
				.CustomAsync(async (n, context, token) => {
					if (await repo.HasName(n, token))
						context.AddFailure($"Имя пользователя {n} уже существует");
				});
			//RuleFor(n => n.Item).Custom((n, context) => {
			//	if (!new SimpleCaptcha().Validate(n.CaptchaCode, n.CaptchaId)) {
			//		context.AddFailure("Введенное значение не соответствует изображению");
			//	}
			//});
		}

	}

	internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommand> {
		//private readonly IAuthService _authService;
		private readonly IPasswordService _pwdService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IPublishEndpoint _publish;

		public UserCreateCommandHandler(/*IAuthService authService,*/ IPasswordService pwdService, IUnitOfWork unitOfWork, IUserRepo userRepo, IPublishEndpoint publish) {
			//_authService = authService;
			_pwdService = pwdService;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_publish = publish;
		}

		public async Task<Unit> Handle(UserCreateCommand request, CancellationToken token) {
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));

			var passwordHash = _pwdService.Hash(item.Password);

			var user = new User(Guid.NewGuid(), item.Login, passwordHash, RoleEnum.User, item.Email);
			await _userRepo.Create(user);

			await _unitOfWork.Commit(() => _publish.Publish<UserCreatedEvent>(new(user.Id)));

			//await _authService.Login(user); // TODO: return auth-token

			return Unit.Value;
		}
	}
}
