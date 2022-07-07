using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Files.Application.Commands {
	public record ImageDeleteCommand : PostDeletedEvent, IRequest {
		public ImageDeleteCommand(Guid id) : base(id) { }
	}

	public class ImageDeleteCommandValidator : AbstractValidator<ImageDeleteCommand> {

		public ImageDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}
	}

	internal class ImageDeleteCommandHandler : IRequestHandler<ImageDeleteCommand> {
		private readonly IFileStorage _storage;

		public ImageDeleteCommandHandler(IFileStorage storage) => _storage = storage;

		public async Task<Unit> Handle(ImageDeleteCommand request, CancellationToken token) {
			await _storage.Delete(request.Id);
			return Unit.Value;
		}
	}


}
