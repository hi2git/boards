using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;
using Board.Domain.Models;
using Board.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Application.Queries.BoardItems {
	public class BoardItemGetAllQuery : IRequest<IEnumerable<BoardItemDTO>> {

		public BoardItemGetAllQuery(Guid id) => this.Id = id;

		public Guid Id { get; }
	}

	public class BoardItemGetAllQueryValidator : AbstractValidator<BoardItemGetAllQuery> {

		public BoardItemGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardItemGetAllQueryHandler : IRequestHandler<BoardItemGetAllQuery, IEnumerable<BoardItemDTO>> {
		private readonly IBoardItemRepo _repo;

		public BoardItemGetAllQueryHandler(IBoardItemRepo repo) => _repo = repo;

		public async Task<IEnumerable<BoardItemDTO>> Handle(BoardItemGetAllQuery request, CancellationToken cancellationToken) {// TODO add user check
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var items = await _repo.GetAll(id);//_userMgr.CurrentUserId);
			return items.Select(this.Map);
		}

		private BoardItemDTO Map(BoardItem n) => new BoardItemDTO {
			Id = n.Id,
			OrderNumber = n.OrderNumber,
			Description = n.Description,
			IsDone = n.IsDone
		};
	}
}
