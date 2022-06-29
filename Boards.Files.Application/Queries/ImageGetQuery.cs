using System;
using System.Linq;

using Boards.Domain.Contracts.Images;

using FluentValidation;

using MediatR;

namespace Boards.Files.Application.Commands {
	public record ImageGetQuery : ImageGetMsg, IRequest<string> {

		public ImageGetQuery(ImageGetMsg msg) : base(msg) { }

	}

	public class ImageGetQueryValidator : AbstractValidator<ImageGetQuery> {

		public ImageGetQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}
	}

	internal class ImageGetQueryHandler : IRequestHandler<ImageGetQuery, string> {
		private readonly IFileStorage _storage;

		public ImageGetQueryHandler(IFileStorage storage) => _storage = storage;

		public Task<string> Handle(ImageGetQuery request, CancellationToken token) => _storage.Get(request.Id, token);
	}


}
