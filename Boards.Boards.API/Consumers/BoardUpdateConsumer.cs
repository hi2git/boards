using Boards.Boards.Application.Commands;
using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Boards;

using MediatR;

namespace Boards.Boards.API.Consumers {
	public class BoardUpdateConsumer : AbstractConsumer<BoardUpdateMsg, BoardUpdateResponse> {
		public BoardUpdateConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(BoardUpdateMsg item, CancellationToken token) => this.Mediator.Send(new BoardUpdateCommand(item), token);
	}
}
