using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Application.Services;
using Board.Domain.Repos;
using Board.Domain.Services;

using Boards.Domain.Contracts.Users;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Users {
	public class UserDeleteCommand : IRequest {

		public UserDeleteCommand() { }
	}

	public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand> { }

	internal class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand> {
		private readonly IAuthService _authService;
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IPublishEndpoint _publish;

		//private readonly IBoardRepo _boardRepo;
		//private readonly IBoardItemRepo _itemRepo;

		public UserDeleteCommandHandler(IAuthService authService, IUserManager userMgr, IUnitOfWork unitOfWork, IUserRepo userRepo, IPublishEndpoint publish ) {
			_authService = authService;
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_publish = publish;
			//_boardRepo = boardRepo;
			//_itemRepo = itemRepo;
		}

		public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken token) {
			var user = await _userRepo.Get(_userMgr.CurrentUserId, token) ?? throw new ArgumentException($"Отсутствует пользователь {_userMgr.CurrentUserId}");

			await _authService.Logout();

			//foreach (var board in user.Boards) {		
			//	foreach (var item in board.BoardItems) {
			//		await _itemRepo.Delete(item);
			//	}
			//	await _boardRepo.Delete(board);
			//}

			await _userRepo.Delete(user);
			await _unitOfWork.Commit(() => _publish.Publish<UserDeletedEvent>(new(user.Id)));

			return Unit.Value;
		}
	}
}
