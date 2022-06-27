using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public class PostContentUpdateCommand : IRequest {

		public PostContentUpdateCommand(PostDTO item) => this.Item = item;

		public PostDTO Item { get; }
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
