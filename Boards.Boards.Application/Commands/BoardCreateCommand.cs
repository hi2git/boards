using System;
using System.Linq;

using Board.Domain.Repos;

using Boards.Boards.Domain.Repos;
using Boards.Commons.Application.Services;
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
				if (await repo.HasName(n.Name, n.Id, default, token))
					context.AddFailure($"Название доски \"{n.Name}\" уже существует");
			});
		}
	}

	internal class BoardCreateCommandHandler : IRequestHandler<BoardCreateCommand, Guid> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _repo;
		private readonly ICacheService _cache;

		public BoardCreateCommandHandler(IUnitOfWork unitOfWork,  IBoardRepo repo, ICacheService cache) {
			_unitOfWork = unitOfWork;
			_repo = repo;
			_cache = cache;
		}

		public async Task<Guid> Handle(BoardCreateCommand request, CancellationToken token) {
			var board = new Domain.Models.Board(Guid.NewGuid(), request.Name, request.Id);

			await _repo.Create(board);
			await _unitOfWork.Commit();

			await _cache.Remove($"user_{request.Id}");

			return board.Id;
		}
	}
}
