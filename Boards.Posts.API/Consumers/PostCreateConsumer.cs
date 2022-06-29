using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostCreateConsumer : AbstractConsumer<PostCreateMsg, PostCreateResponse> {

		public PostCreateConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(PostCreateMsg item, CancellationToken token) => this.Mediator.Send(new PostCreateCommand(item), token);    //TODO: drop cache
		
	}
}
