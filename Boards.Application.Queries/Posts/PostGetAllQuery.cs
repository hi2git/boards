using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Queries.Posts {
	public record PostGetAllQuery : PostGetAllMsg, IRequest<IEnumerable<PostDTO>> {
		public PostGetAllQuery(Guid id) => this.Id = id;
	}

	public class BoardItemGetAllQueryValidator : AbstractValidator<PostGetAllQuery> {

		public BoardItemGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class PostGetAllQueryHandler : IRequestHandler<PostGetAllQuery, IEnumerable<PostDTO>> {
		private readonly IRequestClient<PostGetAllMsg> _client;

		public PostGetAllQueryHandler(IRequestClient<PostGetAllMsg> client) => _client = client;

		public async Task<IEnumerable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) {
			var response = await _client.GetResponse<PostGetAllResponse>(request, token);
			return response.Message.Items;
		}
	}
}
