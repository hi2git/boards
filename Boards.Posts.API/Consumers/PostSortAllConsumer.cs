using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MassTransit;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostSortAllConsumer : IConsumer<PostSortAllMsg> {
		private readonly IMediator _mediator;

		public PostSortAllConsumer(IMediator mediator) => _mediator = mediator;

		public async Task Consume(ConsumeContext<PostSortAllMsg> context) {
			try {
				await _mediator.Send(new PostSortAllCommand(context.Message));  //TODO: drop cache
				await context.RespondAsync(new PostSortedResponse());
			}
			catch (Exception e) {
				await context.RespondAsync(new PostSortedResponse(e.Message));
			}
		}
	}

	public class PostSortAllConsumerDefinition : ConsumerDefinition<PostSortAllConsumer> {

		public PostSortAllConsumerDefinition() {
			this.EndpointName = typeof(PostSortAllMsg).FullName
				?? throw new ConfigurationException($"Couldn't get name of type {typeof(PostUpdateMsg)}");
		}

	}

}
