using System;

using Board.Domain.Services;

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
		private readonly IFileStorage _fileStorage;

		public PostContentUpdateCommandGandler(IFileStorage fileStorage) => _fileStorage = fileStorage;

		public async Task<Unit> Handle(PostContentUpdateCommand request, CancellationToken token) {
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			await _fileStorage.Write(item.Id.Value, item.Content); // TODO: check user before modify
			return Unit.Value;
		}
	}
}
