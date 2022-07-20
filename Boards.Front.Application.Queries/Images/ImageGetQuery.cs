using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Images;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Queries.Images {
	public class ImageGetQuery : IRequest<string> {

		public ImageGetQuery(Guid id) => this.Id = id;

		public Guid Id { get; }
	}

	public class ImagePathGetQueryValidator : AbstractValidator<ImageGetQuery> {

		public ImagePathGetQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class ImageGetQueryHandler : IRequestHandler<ImageGetQuery, string> {
		private readonly IClient<ImageGetMsg, ImageGetResponse> _client;

		public ImageGetQueryHandler(IClient<ImageGetMsg, ImageGetResponse> client) => _client = client;

		public async Task<string> Handle(ImageGetQuery request, CancellationToken token) {
			var response = await _client.Send(new(request.Id), token);
			return response.Content;
		}
	}
}
