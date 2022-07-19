using Board.Domain.Repos;

using Boards.Boards.Application.Commands;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardCreateConsumer : AbstractCommandConsumer<BoardCreateMsg, BoardCreateResponse> {
		public BoardCreateConsumer(IMediator mediator, IEventRepo repo, ILogger<BoardCreateConsumer> log) : base(mediator, repo, log) { }

		protected override Task Handle(BoardCreateMsg item, CancellationToken token) => this.Mediator.Send(new BoardCreateCommand(item), token);
	}
}
