using System;
using System.Linq;

using Board.Domain.DTO;

using Boards.Boards.Domain.Repos;
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

		public BoardGetAllQueryHandler(IBoardRepo repo) => _repo = repo;

		public async Task<IEnumerable<IdNameDTO>> Handle(BoardGetAllQuery request, CancellationToken token) {
			var boards = await _repo.GetAll(request.Id, token);
			return boards.Select(this.Map);
		}

		private IdNameDTO Map(Domain.Models.Board entity) => new() { Id = entity.Id, Name = entity.Name };
	}
}
