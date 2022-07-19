using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostUpdateConsumer : AbstractCommandConsumer<PostUpdateMsg, PostUpdateResponse> {

		public PostUpdateConsumer(IMediator mediator, IEventRepo repo, ILogger<PostUpdateConsumer> log) : base(mediator, repo, log) { }

		protected override Task Handle(PostUpdateMsg item, CancellationToken token) => this.Mediator.Send(new PostUpdateCommand(item), token);    //TODO: drop cache

		
	}
}
