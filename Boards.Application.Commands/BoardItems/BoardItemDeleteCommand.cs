using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.BoardItems {
	public class BoardItemDeleteCommand : IRequest {

		public BoardItemDeleteCommand(Guid id) => this.Id = id;

		public Guid Id { get; }
	}

	public class BoardItemDeleteCommandValidator : AbstractValidator<BoardItemDeleteCommand> {

		public BoardItemDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardItemDeleteCommandHandler : IRequestHandler<BoardItemDeleteCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public BoardItemDeleteCommandHandler(IUnitOfWork unitOfWork, IBoardItemRepo repo, IFileStorage fileStorage) {
			_unitOfWork = unitOfWork;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		public async Task<Unit> Handle(BoardItemDeleteCommand request, CancellationToken cancellationToken) {// TODO: check user before modify
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(id);
			await _repo.Delete(entity);
			await _unitOfWork.Commit();

			await _fileStorage.Delete(id);
			return Unit.Value;
		}
	}
}
