using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostDeleteConsumer : AbstractConsumer<PostDeleteMsg, PostDeleteResponse> {

		public PostDeleteConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(PostDeleteMsg item, CancellationToken token) => this.Mediator.Send(new PostDeleteCommand(item), token);    //TODO: drop cache

		
	}
}
