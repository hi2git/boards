using Board.Domain.Repos;

using Boards.Boards.Application.Commands;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardDeleteConsumer : AbstractCommandConsumer<BoardDeleteMsg, BoardDeleteResponse> {
		public BoardDeleteConsumer(IMediator mediator, IEventRepo repo, ILogger<BoardDeleteConsumer> log) : base(mediator, repo, log) { }

		protected override Task Handle(BoardDeleteMsg item, CancellationToken token) => this.Mediator.Send(new BoardDeleteCommand(item), token);
	}
}
