using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Queries.Posts {
	public record PostGetAllQuery : PostGetAllMsg, IRequest<Pageable<PostDTO>> {
		public PostGetAllQuery(PostFilter filter) : base(filter) { }

	}

	public class BoardItemGetAllQueryValidator : AbstractValidator<PostGetAllQuery> {

		public BoardItemGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Filter).NotEmpty();
			RuleFor(n => n.Filter.BoardId).NotEmpty();
			RuleFor(n => n.Filter.Index).GreaterThan(0);
			RuleFor(n => n.Filter.Size).NotEmpty();
		}

	}

	internal class PostGetAllQueryHandler : IRequestHandler<PostGetAllQuery, Pageable<PostDTO>> {
		private readonly IClient<PostGetAllMsg, PostGetAllResponse> _client;
		//private readonly ICacheService _cache;

		public PostGetAllQueryHandler(IClient<PostGetAllMsg, PostGetAllResponse> client/*, ICacheService cache*/) {
			_client = client;
			//_cache = cache;
		}

		//public Task<Pageable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) =>
			//_cache.GetOrRequest($"board_{request.GetHashCode()}", () => this.Request(request, token), token);
			//_cache.GetOrRequestBoard(request.Id, request.GetHashCode(), () => this.Request(request, token), token);

		//private async Task<Pageable<PostDTO>> Request(PostGetAllQuery request, CancellationToken token) {
		public async Task<Pageable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) { 
			var response = await _client.Send(request, token);
			return response.Page;
		}
	}
}
