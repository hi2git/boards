using System;
using System.Linq;

using Boards.Boards.Domain.Repos;
using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Domain.Contracts.Boards;

using FluentValidation;

using MediatR;

namespace Boards.Boards.Application.Queries {
	public record BoardGetAllQuery : BoardGetAllMsg, IRequest<IEnumerable<IdNameDTO>> {
		public BoardGetAllQuery(Guid userId) : base(userId) {}
	}

	public class BoardGetAllQueryValidator : AbstractValidator<BoardGetAllQuery> {

		public BoardGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardGetAllQueryHandler : IRequestHandler<BoardGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IBoardRepo _repo;
		private readonly ICacheService _cache;

		public BoardGetAllQueryHandler(IBoardRepo repo, ICacheService cache) {
			_repo = repo;
			_cache = cache;
		}

		public Task<IEnumerable<IdNameDTO>> Handle(BoardGetAllQuery request, CancellationToken token) => _cache.GetOrRequest($"user_{request.Id}", () => this.GetFromDb(request, token), token);
		
		public async Task<IEnumerable<IdNameDTO>> GetFromDb(BoardGetAllQuery request, CancellationToken token) {
			var boards = await _repo.GetAll(request.Id, token);
			return boards.Select(this.Map);
		}

		private IdNameDTO Map(Domain.Models.Board entity) => new() { Id = entity.Id, Name = entity.Name };
	}
}
