using System;

using Board.Domain.Repos;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Images;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostDeleteCommand : PostDeleteMsg, IRequest {

		public PostDeleteCommand(PostDeleteMsg msg) {
			this.BoardId = msg.BoardId;
			this.Id = msg.Id;
		}
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
		private readonly IPostRepo _repo;
		private readonly IPublishEndpoint _publish;
		//private readonly IClient<ImageDeleteMsg, ImageDeleteResponse> _client;

		public PostDeleteCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IPostRepo repo, /*IClient<ImageDeleteMsg, ImageDeleteResponse> client*/ IPublishEndpoint publish) {
			_mediator = mediator;
			_unitOfWork = unitOfWork;
			_repo = repo;
			_publish = publish;
			//_client = client;
		}

		public async Task<Unit> Handle(PostDeleteCommand request, CancellationToken token) {// TODO: check user before modify
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var entity = await _repo.Get(id, token);
			await _repo.Delete(entity);

			await _unitOfWork.Commit();

			await _mediator.Send(new PostSortAllCommand(request.BoardId));

			//await _client.Send(new (id), token);
			await _publish.Publish<PostDeletedEvent>(new(id)); // TODO: put in transaction
			return Unit.Value;
		}
	}
}
