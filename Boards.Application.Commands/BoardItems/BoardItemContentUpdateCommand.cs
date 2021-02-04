using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.BoardItems {
	public class BoardItemContentUpdateCommand : IRequest {

		public BoardItemContentUpdateCommand(BoardItemDTO item) => this.Item = item;

		public BoardItemDTO Item { get; }
	}

	public class BoardContentUpdateCommandValidator : AbstractValidator<BoardItemContentUpdateCommand> {

		public BoardContentUpdateCommandValidator() {
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).NotEmpty();
			RuleFor(n => n.Item.Content).NotEmpty();
		}
	}

	internal class BoardContentUpdateCommandGandler : IRequestHandler<BoardItemContentUpdateCommand> {
		private readonly IFileStorage _fileStorage;

		public BoardContentUpdateCommandGandler(IFileStorage fileStorage) {
			_fileStorage = fileStorage;
		}

		public async Task<Unit> Handle(BoardItemContentUpdateCommand request, CancellationToken cancellationToken) {
			var item = request?.Item ?? throw new ArgumentNullException(nameof(request));
			await _fileStorage.Write(item.Id.Value, item.Content); // TODO: check user before modify
			return Unit.Value;
		}
	}
}
