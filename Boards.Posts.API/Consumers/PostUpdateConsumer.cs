using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MassTransit;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostUpdateConsumer : IConsumer<PostUpdateMsg> {
		private readonly IMediator _mediator;

		public PostUpdateConsumer(IMediator mediator) => _mediator = mediator;

		public Task Consume(ConsumeContext<PostUpdateMsg> context) => _mediator.Send(new PostUpdateCommand(context.Message), context.CancellationToken);    //TODO: drop cache
	}

	public class PostUpdateConsumerDefinition : ConsumerDefinition<PostUpdateConsumer> {

		public PostUpdateConsumerDefinition()  => this.EndpointName = typeof(PostUpdateMsg).FullName 
			?? throw new ConfigurationException($"Couldn't get name of type {typeof(PostUpdateMsg)}");
	}
}
