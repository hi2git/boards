using Boards.Domain.Contracts.Posts;
using Boards.Files.Application.Commands;

using MassTransit;

using MediatR;

namespace Boards.Files.API.Consumers {
	public class PostDeletedEventConsumer : IConsumer<PostDeletedEvent> {	// TODO: use AbstractConsumer
		private readonly IMediator _mediator;

		public PostDeletedEventConsumer(IMediator mediator) => _mediator = mediator;

		public Task Consume(ConsumeContext<PostDeletedEvent> context) => _mediator.Send(new ImageDeleteCommand(context.Message.Id));

	}
}
