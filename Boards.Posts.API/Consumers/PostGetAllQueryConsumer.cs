using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Queries;

using MassTransit;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostGetAllQueryConsumer : IConsumer<PostGetAllMsg> {
		private readonly IMediator _mediator;

		public PostGetAllQueryConsumer(IMediator mediator) => _mediator = mediator;

		public async Task Consume(ConsumeContext<PostGetAllMsg> context) {
			var items = await _mediator.Send(new PostGetAllQuery(context.Message.Id), context.CancellationToken);	//TODO: use cache
			await context.RespondAsync(new PostGetAllResponse { Items = items });
		}

	}
}
