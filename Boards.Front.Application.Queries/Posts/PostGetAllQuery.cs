using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
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

		public PostGetAllQueryHandler(IClient<PostGetAllMsg, PostGetAllResponse> client) => _client = client;

		public async Task<IEnumerable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) {
			var response = await _client.Send(request, token);
			return response.Items;
		}
	}
}
