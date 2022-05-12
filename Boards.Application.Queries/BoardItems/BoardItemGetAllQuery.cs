using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;

using Boards.Domain.Contracts.BoardItems;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Queries.BoardItems {
	public record BoardItemGetAllQuery : BoardItemGetAllMsg, IRequest<IEnumerable<BoardItemDTO>> {
		public BoardItemGetAllQuery(Guid id) => this.Id = id;
	}

	public class BoardItemGetAllQueryValidator : AbstractValidator<BoardItemGetAllQuery> {

		public BoardItemGetAllQueryValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardItemGetAllQueryHandler : IRequestHandler<BoardItemGetAllQuery, IEnumerable<BoardItemDTO>> {
		private readonly IRequestClient<BoardItemGetAllMsg> _client;

		public BoardItemGetAllQueryHandler(IRequestClient<BoardItemGetAllMsg> client) => _client = client;

		public async Task<IEnumerable<BoardItemDTO>> Handle(BoardItemGetAllQuery request, CancellationToken token) {
			var response = await _client.GetResponse<BoardItemGetAllResponse>(request, token);
			return response.Message.Items;
		}
	}
}
