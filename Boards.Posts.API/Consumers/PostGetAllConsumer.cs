using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Queries;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostGetAllConsumer : AbstractGetConsumer<PostGetAllMsg, PostGetAllResponse> {

		public PostGetAllConsumer(IMediator mediator) : base(mediator) { }

		protected override async Task<PostGetAllResponse> Handle(PostGetAllMsg item, CancellationToken token) {
			var items = await this.Mediator.Send(new PostGetAllQuery(item.Id), token);   //TODO: use cache
			return new() { Items = items };
		}

	}
}
