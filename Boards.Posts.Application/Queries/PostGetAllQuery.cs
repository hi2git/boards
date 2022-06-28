using System;
using System.Collections.Generic;
using System.Linq;

using Board.Domain.DTO.Posts;
using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Domain.Contracts.Posts;

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
		private readonly IBoardItemRepo _repo;

		public PostGetAllQueryHandler(IBoardItemRepo repo) => _repo = repo;

		public async Task<IEnumerable<PostDTO>> Handle(PostGetAllQuery request, CancellationToken token) {// TODO add user check
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var items = await _repo.GetAll(id, token);
			return items.Select(this.Map);
		}

		private PostDTO Map(BoardItem n) => new() {
			Id = n.Id,
			OrderNumber = n.OrderNumber,
			Description = n.Description,
			IsDone = n.IsDone
		};

	}

}
