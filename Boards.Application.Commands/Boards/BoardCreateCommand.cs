using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Boards {
	public class BoardCreateCommand : IRequest<Guid> {

		public BoardCreateCommand(string name) => this.Name = name;

		public string Name { get; }
	}

	public class BoardCreateCommandValidator : AbstractValidator<BoardCreateCommand> {

		public BoardCreateCommandValidator() {
			RuleFor(n => n.Name).NotEmpty().MaximumLength(50);
		}
	}

	internal class BoardCreateCommandHandler : IRequestHandler<BoardCreateCommand, Guid> {
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IBoardRepo _boardRepo;

		public BoardCreateCommandHandler(IUserManager userMgr, IUnitOfWork unitOfWork, IUserRepo userRepo, IBoardRepo boardRepo) {
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_boardRepo = boardRepo;
		}

		public async Task<Guid> Handle(BoardCreateCommand request, CancellationToken cancellationToken) {
			var user = await _userRepo.Get(_userMgr.CurrentUserId);
			var board = new Board.Domain.Models.Board(Guid.NewGuid(), request.Name, user);

			await _boardRepo.Create(board);
			await _unitOfWork.Commit();

			return board.Id;
		}
	}
}
