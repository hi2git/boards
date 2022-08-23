using System;
using System.Linq;

using Board.Domain.Repos;

using Boards.Boards.Domain.Repos;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Boards.Application.Commands {
	public record BoardDeleteCommand : BoardDeleteMsg, IRequest {

		public BoardDeleteCommand(BoardDeleteMsg msg) : base(msg.Id, msg.UserId) { }
	}

	public class BoardDeleteCommandValidator : AbstractValidator<BoardDeleteCommand> {

		public BoardDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteCommandHandler : IRequestHandler<BoardDeleteCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _repo;
		private readonly IPublishEndpoint _publish;
		private readonly ICacheService _cache;

		public BoardDeleteCommandHandler(IUnitOfWork unitOfWork, IBoardRepo repo, IPublishEndpoint publish, ICacheService cache) {
			_unitOfWork = unitOfWork;
			_repo = repo;
			_publish = publish;
			_cache = cache;
		}

		public async Task<Unit> Handle(BoardDeleteCommand request, CancellationToken token) {
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var board = await _repo.Get(id, token) ?? throw new ArgumentException($"Отсутствует доска {id}");

			await _repo.Delete(board);
			await _unitOfWork.Commit(() => _publish.Publish<BoardDeletedEvent>(new(id)));

			await _cache.Remove($"user_{request.UserId}");

			return Unit.Value;
		}
	}
}
