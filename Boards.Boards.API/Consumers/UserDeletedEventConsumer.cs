using Boards.Boards.Application.Commands;
using Boards.Domain.Contracts.Users;

using MassTransit;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class UserDeletedEventConsumer : IConsumer<UserDeletedEvent> {
		private readonly IMediator _mediator;

		public UserDeletedEventConsumer(IMediator mediator) => _mediator = mediator;

		public Task Consume(ConsumeContext<UserDeletedEvent> context) => _mediator.Send(new BoardDeleteAllCommand(context.Message.Id), context.CancellationToken);
	}
}
