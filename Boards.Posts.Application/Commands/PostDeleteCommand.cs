using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public class PostDeleteCommand : IRequest {

		public PostDeleteCommand(PostDeleteMsg msg) {
			this.BoardId = msg.BoardId;
			this.Id = msg.Id;
		}

		public Guid BoardId { get; }

		public Guid Id { get; }
	}

	public class PostDeleteCommandValidator : AbstractValidator<PostDeleteCommand> {

		public PostDeleteCommandValidator() {
			RuleFor(n => n.BoardId).NotEmpty();
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class PostDeleteCommandHandler : IRequestHandler<PostDeleteCommand> {
		private readonly IMediator _mediator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public PostDeleteCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IBoardItemRepo repo, IFileStorage fileStorage) {
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		public async Task<Unit> Handle(PostDeleteCommand request, CancellationToken token) {// TODO: check user before modify
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(id, token);
			await _repo.Delete(entity);

			await _unitOfWork.Commit();

			await _mediator.Send(new PostSortAllCommand(request.BoardId));

			await _fileStorage.Delete(id);
			return Unit.Value;
		}
	}
}
