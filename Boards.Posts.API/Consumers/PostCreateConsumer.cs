using Board.Domain.Repos;

using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostCreateConsumer : AbstractCommandConsumer<PostCreateMsg, PostCreateResponse> {

		public PostCreateConsumer(IMediator mediator, IEventRepo repo, ILogger<PostCreateConsumer> log) : base(mediator, repo, log) { }

		protected override Task Handle(PostCreateMsg item, CancellationToken token) => this.Mediator.Send(new PostCreateCommand(item), token);    //TODO: drop cache
		
	}
}
