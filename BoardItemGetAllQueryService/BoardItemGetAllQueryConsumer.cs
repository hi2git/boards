using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;

using Boards.Domain.Contracts.BoardItems;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace BoardItemGetAllQueryService {
	internal class BoardItemGetAllQueryConsumer : IConsumer<BoardItemGetAllMsg> {
		private readonly ILogger<BoardItemGetAllQueryConsumer> _logger;

		public BoardItemGetAllQueryConsumer(ILogger<BoardItemGetAllQueryConsumer> logger) => _logger = logger;

		public async Task Consume(ConsumeContext<BoardItemGetAllMsg> context) {
			_logger.LogInformation($"Received: {context.Message.Id}");

			var items = new List<BoardItemDTO> {
				new(){ Id = Guid.NewGuid() },
				new(){ Id = Guid.NewGuid() },
			};
			await context.RespondAsync(new BoardItemGetAllResponse { Items = items });
		}
	}
}
