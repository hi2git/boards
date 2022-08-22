using System;
using System.Linq;

using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Queries {
	public record PostGetAllQuery : PostGetAllMsg, IRequest<Pageable<PostDTO>> {

		public PostGetAllQuery(PostGetAllMsg msg) : base(msg.Filter) { }

	}


	public class PostGetAllQueryValidator : AbstractValidator<PostGetAllQuery> {

		public PostGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Filter).NotEmpty();
			RuleFor(n => n.Filter.Index).GreaterThan(0);
			RuleFor(n => n.Filter.BoardId).NotEmpty();
			RuleFor(n => n.Filter.Size).NotEmpty();
		}
	}

	internal class PostGetAllQueryHandler : IRequestHandler<PostGetAllQuery, Pageable<PostDTO>> {
		private readonly IPostRepo _repo;
		private readonly ICacheService _cache;

		public PostGetAllQueryHandler(IPostRepo repo, ICacheService cache) {
			_repo = repo;
			_cache = cache;
		}

		public Task<Pageable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) {// TODO add user check
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			//var filter = request.Filter;
			//var items = await _repo.GetAll(id, token);

			return _cache.GetOrRequestBoard(id, request.Filter.GetHashCode(), () => this.GetFromDb(request, token), token);

			//var query = _repo.Query(id);
			//var total = query.Count(); // TODO: replace to async call

			//var items = total > 0 ? query.Skip(filter.RealIndex * filter.SizeInt).Take(filter.SizeInt).ToList() : Enumerable.Empty<Post>();	// TODO: replace to async call
			//return new Pageable<PostDTO>(items.Select(Map), total);
		}

		private async Task<Pageable<PostDTO>> GetFromDb(PostGetAllQuery request, CancellationToken token) {
			var filter = request.Filter;
			var query = _repo.Query(request.Id);
			var total = query.Count(); // TODO: replace to async call

			var items = total > 0 ? query.Skip(filter.RealIndex * filter.SizeInt).Take(filter.SizeInt).ToList() : Enumerable.Empty<Post>(); // TODO: replace to async call
			return new Pageable<PostDTO>(items.Select(Map), total);
		}

		private static PostDTO Map(Post n) => new() {
			Id = n.Id,
			OrderNumber = n.OrderNumber,
			Description = n.Description,
			IsDone = n.IsDone
		};

	}

}
