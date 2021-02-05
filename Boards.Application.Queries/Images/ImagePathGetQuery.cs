using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Queries.Images {
	public class ImagePathGetQuery : IRequest<string> {

		public ImagePathGetQuery(Guid id) => this.Id = id;

		public Guid Id { get; }
	}

	public class ImagePathGetQueryValidator : AbstractValidator<ImagePathGetQuery> {

		public ImagePathGetQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class ImagePathGetQueryHandler : IRequestHandler<ImagePathGetQuery, string> {
		private readonly IFileStorage _fileStorage;

		public ImagePathGetQueryHandler(IFileStorage fileStorage) => _fileStorage = fileStorage;

		public Task<string> Handle(ImagePathGetQuery request, CancellationToken cancellationToken) => Task.Run(() => _fileStorage.PathOf(request.Id));
	}
}
