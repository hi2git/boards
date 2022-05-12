using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;
using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Domain.Contracts.BoardItems;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace BoardItemGetAllQueryService {
	internal class BoardItemGetAllQueryConsumer : IConsumer<BoardItemGetAllMsg> {
		private readonly ILogger<BoardItemGetAllQueryConsumer> _logger;
		private readonly IBoardItemRepo _repo;

		public BoardItemGetAllQueryConsumer(ILogger<BoardItemGetAllQueryConsumer> logger, IBoardItemRepo repo) {
			_logger = logger;
			_repo = repo;
		}

		public async Task Consume(ConsumeContext<BoardItemGetAllMsg> context) {
			var items = await this.Handle(context.Message);
			await context.RespondAsync(new BoardItemGetAllResponse { Items = items });
		}

		public async Task<IEnumerable<BoardItemDTO>> Handle(BoardItemGetAllMsg request, CancellationToken token = default) {// TODO add user check
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			_logger.LogInformation($"Received: {id}");
			var items = await _repo.GetAll(id);//_userMgr.CurrentUserId);
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
