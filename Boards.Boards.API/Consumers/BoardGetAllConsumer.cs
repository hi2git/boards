using Boards.Boards.Application.Queries;
using Boards.Domain.Contracts.Boards;

using MassTransit;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardGetAllConsumer : IConsumer<BoardGetAllMsg> {
		private readonly IMediator _mediator;

		public BoardGetAllConsumer(IMediator mediator) => _mediator = mediator;

		public async Task Consume(ConsumeContext<BoardGetAllMsg> context) {
			var items =await _mediator.Send(new BoardGetAllQuery(context.Message.Id), context.CancellationToken);
			await context.RespondAsync(new BoardGetAllResponse { Items = items });
		}
	}
}
