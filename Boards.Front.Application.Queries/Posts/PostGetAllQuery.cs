using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Queries.Posts {
	public record PostGetAllQuery : PostGetAllMsg, IRequest<IEnumerable<PostDTO>> {
		public PostGetAllQuery(Guid id) => this.Id = id;
	}

	public class BoardItemGetAllQueryValidator : AbstractValidator<PostGetAllQuery> {

		public BoardItemGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class PostGetAllQueryHandler : IRequestHandler<PostGetAllQuery, IEnumerable<PostDTO>> {
		private readonly IClient<PostGetAllMsg, PostGetAllResponse> _client;
		private readonly ICacheService _cache;

		public PostGetAllQueryHandler(IClient<PostGetAllMsg, PostGetAllResponse> client, ICacheService cache) {
			_client = client;
			_cache = cache;
		}

		public Task<IEnumerable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) => _cache.GetOrRequest($"board_{request.Id}", () => this.Request(request, token), token);

		private async Task<IEnumerable<PostDTO>> Request(PostGetAllQuery request, CancellationToken token) {
			var response = await _client.Send(request, token);
			return response.Items;
		}
	}
}
