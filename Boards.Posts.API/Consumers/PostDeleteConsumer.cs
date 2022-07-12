using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostDeleteConsumer : AbstractConsumer<PostDeleteMsg, PostDeleteResponse> {

		public PostDeleteConsumer(IMediator mediator, IEventRepo repo) : base(mediator, repo) { }

		protected override Task Handle(PostDeleteMsg item, CancellationToken token) => this.Mediator.Send(new PostDeleteCommand(item), token);    //TODO: drop cache

		
	}
}
