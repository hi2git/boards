using Boards.Boards.Application.Queries;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardGetAllConsumer : AbstractQueryConsumer<BoardGetAllMsg, BoardGetAllResponse> {

		public BoardGetAllConsumer(IMediator mediator, ILogger<BoardGetAllConsumer> log) : base(mediator, log) { }

		protected override async Task<BoardGetAllResponse> Handle(BoardGetAllMsg item, CancellationToken token) {
			var items = await this.Mediator.Send(new BoardGetAllQuery(item.Id), token);
			return new() { Items = items };
		}
	}
}
