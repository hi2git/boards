using System;
using System.Linq;

using Board.Domain.Repos;

using Boards.Boards.Domain.Repos;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Boards.Application.Commands {
	public record BoardUpdateCommand : BoardUpdateMsg, IRequest {

		public BoardUpdateCommand(BoardUpdateMsg item) : base(item.Id, item.Name, item.UserId) { }

	}

	public class BoardUpdateCommandValidator : AbstractValidator<BoardUpdateCommand> {

		public BoardUpdateCommandValidator(IBoardRepo repo) {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Name).NotEmpty().MaximumLength(50);
			RuleFor(n => n).CustomAsync(async (n, context, token) => {
					if (await repo.HasName(n.Name, n.UserId, token))	// except current id ?
						context.AddFailure($"Название доски \"{n.Name}\" уже существует");
				});
		}
	}

	internal class BoardUpdateCommandHandler : IRequestHandler<BoardUpdateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _repo;

		public BoardUpdateCommandHandler(IUnitOfWork unitOfWork, IBoardRepo repo) {
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		public async Task<Unit> Handle(BoardUpdateCommand request, CancellationToken token) {
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			
			var board = await _repo.Get(id, token) ?? throw new ArgumentException($"Отсутствует доска {id}");
			board.Name = request.Name;
			
			await _repo.Update(board);
			await _unitOfWork.Commit();

			return Unit.Value;
		}
	}
}
