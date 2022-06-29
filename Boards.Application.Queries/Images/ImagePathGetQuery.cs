using System;
using System.Threading;
using System.Threading.Tasks;


using Boards.Domain.Contracts.Images;

using FluentValidation;

using MassTransit;

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
		private readonly IRequestClient<ImageGetMsg> _client;

		public ImagePathGetQueryHandler(IRequestClient<ImageGetMsg> client) => _client = client;

		public async Task<string> Handle(ImagePathGetQuery request, CancellationToken token) {
			var response  = await _client.GetResponse<ImageGetResponse>(new(request.Id), token);
			return response.Message.Content;
		}
	}
}
