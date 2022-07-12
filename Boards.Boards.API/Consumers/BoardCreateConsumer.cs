using Board.Domain.Repos;

using Boards.Boards.Application.Commands;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardCreateConsumer : AbstractConsumer<BoardCreateMsg, BoardCreateResponse> {
		public BoardCreateConsumer(IMediator mediator, IEventRepo repo) : base(mediator, repo) { }

		protected override Task Handle(BoardCreateMsg item, CancellationToken token) => this.Mediator.Send(new BoardCreateCommand(item), token);
	}
}
