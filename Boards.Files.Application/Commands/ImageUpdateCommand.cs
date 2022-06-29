using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Domain.Contracts.Images;

using FluentValidation;

using MediatR;

namespace Boards.Files.Application.Commands {
	public record ImageUpdateCommand : ImageUpdateMsg, IRequest {

		public ImageUpdateCommand(ImageUpdateMsg msg) : base(msg) { }

	}

	public class ImageUpdateCommandValidator : AbstractValidator<ImageUpdateCommand> {

		public ImageUpdateCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Content).NotEmpty();
		}
	}

	internal class ImageUpdateCommandHandler : IRequestHandler<ImageUpdateCommand> {
		private readonly IFileStorage _storage;

		public ImageUpdateCommandHandler(IFileStorage storage) => _storage = storage;

		public async Task<Unit> Handle(ImageUpdateCommand request, CancellationToken token) {
			await _storage.Write(request.Id, request.Content);
			return Unit.Value;
		}
	}


}
