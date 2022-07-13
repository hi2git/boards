using Boards.Boards.Application.Queries;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardGetAllConsumer : AbstractGetConsumer<BoardGetAllMsg, BoardGetAllResponse> {

		public BoardGetAllConsumer(IMediator mediator) : base(mediator) { }

		protected override async Task<BoardGetAllResponse> Handle(BoardGetAllMsg item, CancellationToken token) {
			var items = await this.Mediator.Send(new BoardGetAllQuery(item.Id), token);
			return new() { Items = items };
		}
	}
}
