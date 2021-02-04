using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Repos;
using Board.Domain.Services;

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
		}
	}

	internal class BoardUpdateCommandHandler : IRequestHandler<BoardUpdateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _boardRepo;

		public BoardUpdateCommandHandler(IUnitOfWork unitOfWork, IBoardRepo boardRepo) {
			_unitOfWork = unitOfWork;
			_boardRepo = boardRepo;
		}

		public async Task<Unit> Handle(BoardUpdateCommand request, CancellationToken cancellationToken) {
			var id = request?.Item.Id ?? throw new ArgumentNullException(nameof(request));
			var board = await _boardRepo.Get(id) ?? throw new ArgumentException($"Отсутствует доска {id}");
			board.Name = request?.Item.Name;
			await _boardRepo.Update(board);
			await _unitOfWork.Commit();

			return Unit.Value;
		}
	}
}
