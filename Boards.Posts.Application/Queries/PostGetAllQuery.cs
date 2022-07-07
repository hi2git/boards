using System;
using System.Linq;

using Board.Domain.DTO.Posts;

using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Queries {
	public record PostGetAllQuery : PostGetAllMsg, IRequest<IEnumerable<PostDTO>> {

		public PostGetAllQuery(Guid id) => this.Id = id;

	}


	public class PostGetAllQueryValidator : AbstractValidator<PostGetAllQuery> {

		public PostGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}
	}

	internal class PostGetAllQueryHandler : IRequestHandler<PostGetAllQuery, IEnumerable<PostDTO>> {
		private readonly IPostRepo _repo;

		public PostGetAllQueryHandler(IPostRepo repo) => _repo = repo;

		public async Task<IEnumerable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) {// TODO add user check
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var items = await _repo.GetAll(id, token);
			return items.Select(this.Map);
		}

		private PostDTO Map(Post n) => new() {
			Id = n.Id,
			OrderNumber = n.OrderNumber,
			Description = n.Description,
			IsDone = n.IsDone
		};

	}

}
