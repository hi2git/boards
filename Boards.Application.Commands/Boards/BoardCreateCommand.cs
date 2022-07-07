using System;
using System.Linq;

using Board.Domain.Services;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Boards {
	public class BoardCreateCommand : IRequest {

		public BoardCreateCommand(string name) => this.Name = name;

		public string Name { get; }
	}

	public class BoardCreateCommandValidator : AbstractValidator<BoardCreateCommand> {

		public BoardCreateCommandValidator() {
			RuleFor(n => n.Name).NotEmpty().MaximumLength(50);
				//.CustomAsync(async (n, context, _) => {
				//	if (await repo.HasName(n, userMgr.CurrentUserId))
				//		context.AddFailure($"Название доски {n} уже существует");
				//});
		}
	}

	internal class BoardCreateCommandHandler : AbstractHandler<BoardCreateCommand, BoardCreateMsg, BoardCreateResponse> {
		private readonly IUserManager _userMgr;
		//private readonly IUnitOfWork _unitOfWork;
		//private readonly IUserRepo _userRepo;
		//private readonly IBoardRepo _boardRepo;

		public BoardCreateCommandHandler(IClient<BoardCreateMsg, BoardCreateResponse> client, IUserManager userMgr) : base(client) => _userMgr = userMgr;

		protected override BoardCreateMsg GetMsg(BoardCreateCommand request) => new(_userMgr.CurrentUserId, request.Name);

		//public async Task<Guid> Handle(BoardCreateCommand request, CancellationToken token) {
		//var user = await _userRepo.Get(_userMgr.CurrentUserId, token);
		//var board = new Board.Domain.Models.Board(Guid.NewGuid(), request.Name, user);

		//await _boardRepo.Create(board);
		//await _unitOfWork.Commit();

		//return board.Id;
	}
}
