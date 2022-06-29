using System;

using Board.Domain.Services;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Images;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostContentUpdateCommand : PostContentUpdateMsg, IRequest {

		public PostContentUpdateCommand(PostContentUpdateMsg msg) => this.Item = msg.Item;

	}

	public class PostContentUpdateCommandValidator : AbstractValidator<PostContentUpdateCommand> {

		public PostContentUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			RuleFor(n => n.Item.Content).NotEmpty();
		}
	}

	internal class PostContentUpdateCommandGandler : IRequestHandler<PostContentUpdateCommand> {
		private readonly IClient<ImageUpdateMsg, ImageUpdateResponse> _client;

		public PostContentUpdateCommandGandler(IClient<ImageUpdateMsg, ImageUpdateResponse> client) => _client = client;

		public async Task<Unit> Handle(PostContentUpdateCommand request, CancellationToken token) {
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			await _client.Send(new(item.Id.Value, item.Content), token); // TODO: check user before modify
			return Unit.Value;
		}
	}
}
