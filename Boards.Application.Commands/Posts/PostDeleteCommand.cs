using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public record PostDeleteCommand : PostDeleteMsg, IRequest {

		public PostDeleteCommand(Guid boardId, Guid id) {
			this.BoardId = boardId;
			this.Id = id;
		}
	}

	public class PostDeleteCommandValidator : AbstractValidator<PostDeleteCommand> {

		public PostDeleteCommandValidator() {
			RuleFor(n => n.BoardId).NotEmpty();
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class PostDeleteCommandHandler : AbstractHandler, IRequestHandler<PostDeleteCommand> {
		private readonly IRequestClient<PostDeleteMsg> _client;

		public PostDeleteCommandHandler(IRequestClient<PostDeleteMsg> client) => _client = client;

		public async Task<Unit> Handle(PostDeleteCommand request, CancellationToken token) {
			var response = await _client.GetResponse<PostDeleteResponse>(request, token);
			return ThrowIfError(response.Message);
		}
	}
}
