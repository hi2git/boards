using Board.Domain.DTO.BoardItems;
using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Domain.Contracts.BoardItems;

using MassTransit;

using MediatR;

namespace Boards.Posts.API.Consumers {
	public class PostGetAllQueryConsumer : IConsumer<BoardItemGetAllMsg> {
		private readonly ILogger _logger;
		private readonly IBoardItemRepo _repo;

		public PostGetAllQueryConsumer(ILogger<PostGetAllQueryConsumer> logger, IBoardItemRepo repo) {
			_logger = logger;
			_repo = repo;
		}

		public async Task Consume(ConsumeContext<BoardItemGetAllMsg> context) {
			var items = await this.Handle(context.Message, context.CancellationToken);
			await context.RespondAsync(new BoardItemGetAllResponse { Items = items });
		}

		public async Task<IEnumerable<BoardItemDTO>> Handle(BoardItemGetAllMsg request, CancellationToken token) {// TODO add user check
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			_logger.LogInformation($"Received: {id}");
			var items = await _repo.GetAll(id, token);
			return items.Select(this.Map);
		}

		private BoardItemDTO Map(BoardItem n) => new() {
			Id = n.Id,
			OrderNumber = n.OrderNumber,
			Description = n.Description,
			IsDone = n.IsDone
		};

	}
}
