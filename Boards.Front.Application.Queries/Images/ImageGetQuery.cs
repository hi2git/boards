using System;
using System.Threading;
using System.Threading.Tasks;

using Boards.Domain.Contracts.Images;

using FluentValidation;

using MassTransit;

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
		private readonly IRequestClient<ImageGetMsg> _client;

		public ImageGetQueryHandler(IRequestClient<ImageGetMsg> client) => _client = client;

		public async Task<string> Handle(ImageGetQuery request, CancellationToken token) {
			var response = await _client.GetResponse<ImageGetResponse>(new(request.Id), token);
			return response.Message.Content;
		}
	}
}
