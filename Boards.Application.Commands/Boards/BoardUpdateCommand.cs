using System;
using System.Linq;

using Board.Domain.DTO;
using Board.Domain.Services;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Boards {
	public class BoardUpdateCommand : IRequest {

		public BoardUpdateCommand(IdNameDTO item) => this.Item = item;

		public IdNameDTO Item { get; }
	}

	public class BoardUpdateCommandValidator : AbstractValidator<BoardUpdateCommand> {

		public BoardUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			RuleFor(n => n.Item.Name).NotEmpty().MaximumLength(50);
				//.CustomAsync(async (n, context, _) => {
				//	if (await repo.HasName(n, userMgr.CurrentUserId))
				//		context.AddFailure($"Название доски {n} уже существует");
				//});
		}
	}

	internal class BoardUpdateCommandHandler : AbstractHandler<BoardUpdateCommand, BoardUpdateMsg, BoardUpdateResponse> { //IRequestHandler<BoardUpdateCommand> {
		private readonly IUserManager _userMgr;

		//private readonly IUnitOfWork _unitOfWork;
		//private readonly IBoardRepo _boardRepo;

		public BoardUpdateCommandHandler(IClient<BoardUpdateMsg, BoardUpdateResponse> client, IUserManager userMgr) : base(client) => _userMgr = userMgr;

		protected override BoardUpdateMsg GetMsg(BoardUpdateCommand request) => new(request.Item, _userMgr.CurrentUserId);

		//public BoardUpdateCommandHandler(IUnitOfWork unitOfWork, IBoardRepo boardRepo) {
		//	_unitOfWork = unitOfWork;
		//	_boardRepo = boardRepo;
		//}

		//public async Task<Unit> Handle(BoardUpdateCommand request, CancellationToken token) {
		//	var id = request?.Item.Id ?? throw new ArgumentNullException(nameof(request));
		//	var board = await _boardRepo.Get(id, token) ?? throw new ArgumentException($"Отсутствует доска {id}");
		//	board.Name = request?.Item.Name;
		//	await _boardRepo.Update(board);
		//	await _unitOfWork.Commit();

		//	return Unit.Value;
		//}
	}
}
