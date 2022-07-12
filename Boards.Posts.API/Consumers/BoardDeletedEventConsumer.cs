using Boards.Domain.Contracts.Boards;
using Boards.Posts.Application.Commands;

using MassTransit;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class BoardDeletedEventConsumer : IConsumer<BoardDeletedEvent> {
		private readonly IMediator _mediator;

		public BoardDeletedEventConsumer(IMediator mediator) => _mediator = mediator;

		public Task Consume(ConsumeContext<BoardDeletedEvent> context) => _mediator.Send(new PostDeleteAllCommand(context.Message.Id));  
	}
}
