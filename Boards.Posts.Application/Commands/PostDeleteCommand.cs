using System;

using Board.Domain.Repos;

using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostDeleteCommand : PostDeleteMsg, IRequest {

		public PostDeleteCommand(PostDeleteMsg msg) {
			this.Id = msg.Id;
			this.PostId = msg.PostId;
		}
	}

	public class PostDeleteCommandValidator : AbstractValidator<PostDeleteCommand> {

		public PostDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.PostId).NotEmpty();
		}

	}

	internal class PostDeleteCommandHandler : IRequestHandler<PostDeleteCommand> {
		private readonly IMediator _mediator;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPostRepo _repo;
		private readonly IPublishEndpoint _publish;

		public PostDeleteCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IPostRepo repo, IPublishEndpoint publish) {
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repo = repo;
			_publish = publish;
		}

		public async Task<Unit> Handle(PostDeleteCommand request, CancellationToken token) {// TODO: check user before modify
			var id = request?.PostId ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(id, token);
			await _repo.Delete(entity);

			await _unitOfWork.Commit(() => _publish.Publish<PostDeletedEvent>(new(id)));

			await _mediator.Send(new PostSortAllCommand(request.Id));	// TODO: replace by request

			return Unit.Value;
		}
	}
}
