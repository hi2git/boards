using System;
using System.Linq;

using Board.Domain.Repos;

using Boards.Boards.Domain.Repos;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Boards.Application.Commands {
	public record BoardCreateCommand : BoardCreateMsg, IRequest<Guid> {

		public BoardCreateCommand(BoardCreateMsg msg) : base(msg.Id, msg.Name) { }
	}

	public class BoardCreateCommandValidator : AbstractValidator<BoardCreateCommand> {

		public BoardCreateCommandValidator(IBoardRepo repo) {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Name).NotEmpty().MaximumLength(50);
			RuleFor(n => n).CustomAsync(async (n, context, token) => {
				if (await repo.HasName(n.Name, n.Id, token))                    // except current id ?
					context.AddFailure($"Название доски \"{n.Name}\" уже существует");
			});
		}
	}

	internal class BoardCreateCommandHandler : IRequestHandler<BoardCreateCommand, Guid> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _repo;

		public BoardCreateCommandHandler(IUnitOfWork unitOfWork,  IBoardRepo repo) {
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		public async Task<Guid> Handle(BoardCreateCommand request, CancellationToken token) {
			var board = new Domain.Models.Board(Guid.NewGuid(), request.Name, request.Id);

			await _repo.Create(board);
			await _unitOfWork.Commit();

			return board.Id;
		}
	}
}
