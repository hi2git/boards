using Board.Domain.Repos;

using Boards.Boards.Application.Commands;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardDeleteConsumer : AbstractConsumer<BoardDeleteMsg, BoardDeleteResponse> {
		public BoardDeleteConsumer(IMediator mediator, IEventRepo repo) : base(mediator, repo) { }

		protected override Task Handle(BoardDeleteMsg item, CancellationToken token) => this.Mediator.Send(new BoardDeleteCommand(item.Id), token);
	}
}
