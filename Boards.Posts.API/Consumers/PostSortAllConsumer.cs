using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Application.Commands;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostSortAllConsumer : AbstractConsumer<PostSortAllMsg, PostSortedResponse> {

		public PostSortAllConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(PostSortAllMsg item, CancellationToken token) => this.Mediator.Send(new PostSortAllCommand(item), token);  //TODO: drop cache // TODO: skip doubles

	}

}
