using Board.Domain.Repos;

using Boards.Boards.Application.Commands;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardUpdateConsumer : AbstractCommandConsumer<BoardUpdateMsg, BoardUpdateResponse> {
		public BoardUpdateConsumer(IMediator mediator, IEventRepo repo, ILogger<BoardUpdateConsumer> log) : base(mediator, repo, log) { }

		protected override Task Handle(BoardUpdateMsg item, CancellationToken token) => this.Mediator.Send(new BoardUpdateCommand(item), token);
	}
}
