using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;

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

	internal class PostUpdateCommandHandler : IRequestHandler<PostUpdateCommand> {
		private readonly ISendEndpointProvider _sendProvider;

		public PostUpdateCommandHandler(ISendEndpointProvider sendProvider) => _sendProvider = sendProvider;

		public async Task<Unit> Handle(PostUpdateCommand request, CancellationToken token) {// TODO: check user before modify
			var endpoint = await _sendProvider.GetSendEndpoint(new Uri($"queue:{typeof(PostUpdateMsg).FullName}"));
			await endpoint.Send(request, token);

			return Unit.Value;
		}

	}
}
