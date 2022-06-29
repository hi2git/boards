using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

using Boards.Domain.Contracts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public record PostUpdateCommand : PostUpdateMsg, IRequest {

		public PostUpdateCommand(PostDTO item) => this.Item = item;

	}

	public class PostUpdateCommandValidator : AbstractValidator<PostUpdateCommand> {

		public PostUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			//RuleFor(n => n.Item.OrderNumber).NotEmpty();
		}
	}

	internal class PostUpdateCommandHandler : AbstractHandler, IRequestHandler<PostUpdateCommand> {
		private readonly IRequestClient<PostUpdateMsg> _client;

		public PostUpdateCommandHandler(IRequestClient<PostUpdateMsg> client) => _client = client;

		public async Task<Unit> Handle(PostUpdateCommand request, CancellationToken token) {
			var response = await _client.GetResponse<PostUpdateResponse>(request, token);
			return ThrowIfError(response.Message);
		}



	}
}
