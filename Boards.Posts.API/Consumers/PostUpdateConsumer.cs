using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostUpdateConsumer : AbstractConsumer<PostUpdateMsg, PostUpdateResponse> {

		public PostUpdateConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(PostUpdateMsg item, CancellationToken token) => this.Mediator.Send(new PostUpdateCommand(item), token);    //TODO: drop cache

		
	}
}
