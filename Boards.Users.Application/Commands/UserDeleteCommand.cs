using System;

using Board.Domain.Repos;

using Boards.Domain.Contracts.Users;
using Boards.Users.Domain.Repos;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Users.Application.Commands {
	public record UserDeleteCommand : UserDeleteMsg, IRequest {

		public UserDeleteCommand(UserDeleteMsg msg) : base(msg.Id) { }
	}

	public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand> {
		public UserDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}
	}

	internal class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand> {
		//private readonly IAuthService _authService;
		//private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IPublishEndpoint _publish;

		//private readonly IBoardRepo _boardRepo;
		//private readonly IBoardItemRepo _itemRepo;

		public UserDeleteCommandHandler(/*IAuthService authService, IUserManager userMgr,*/ IUnitOfWork unitOfWork, IUserRepo userRepo, IPublishEndpoint publish) {
			//_authService = authService;
			//_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_publish = publish;
			//_boardRepo = boardRepo;
			//_itemRepo = itemRepo;
		}

		public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken token) {
			var userId = request.Id;
			var user = await _userRepo.Get(userId, token) ?? throw new ArgumentException($"Отсутствует пользователь {userId}");

			//await _authService.Logout(); // TODO: logout

			await _userRepo.Delete(user);
			await _unitOfWork.Commit(() => _publish.Publish<UserDeletedEvent>(new(user.Id)));

			return Unit.Value;
		}
	}
}
