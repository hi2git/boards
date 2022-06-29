using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostContentUpdateConsumer : AbstractConsumer<PostContentUpdateMsg, PostContentUpdateResponse> {

		public PostContentUpdateConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(PostContentUpdateMsg item, CancellationToken token) => this.Mediator.Send(new PostContentUpdateCommand(item), token);    //TODO: drop cache

		
	}
}
