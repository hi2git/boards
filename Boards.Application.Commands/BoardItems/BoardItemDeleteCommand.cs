using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.BoardItems {
	public class BoardItemDeleteCommand : IRequest {

		public BoardItemDeleteCommand(Guid boardId, Guid id) {
			this.BoardId = boardId;
			this.Id = id;
		}

		public Guid BoardId { get; }

		public Guid Id { get; }
	}

	public class BoardItemDeleteCommandValidator : AbstractValidator<BoardItemDeleteCommand> {

		public BoardItemDeleteCommandValidator() {
			RuleFor(n => n.BoardId).NotEmpty();
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardItemDeleteCommandHandler : IRequestHandler<BoardItemDeleteCommand> {
		private readonly IMediator _mediator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public BoardItemDeleteCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IBoardItemRepo repo, IFileStorage fileStorage) {
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		public async Task<Unit> Handle(BoardItemDeleteCommand request, CancellationToken token) {// TODO: check user before modify
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(id, token);
			await _repo.Delete(entity);

			await _unitOfWork.Commit();

			await _mediator.Send(new BoardSortAllCommand(request.BoardId));

			await _fileStorage.Delete(id);
			return Unit.Value;
		}
	}
}
