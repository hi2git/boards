using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Queries;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostGetAllConsumer : AbstractQueryConsumer<PostGetAllMsg, PostGetAllResponse> {

		public PostGetAllConsumer(IMediator mediator, ILogger<PostGetAllConsumer> log) : base(mediator, log) { }

		protected override async Task<PostGetAllResponse> Handle(PostGetAllMsg item, CancellationToken token) {
			var items = await this.Mediator.Send(new PostGetAllQuery(item.Id), token);   //TODO: use cache
			return new() { Items = items };
		}

	}
}
