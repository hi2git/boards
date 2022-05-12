using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Application.Services;
using Board.Domain.DTO.BoardItems;

using Boards.Application.Queries.BoardItems;
using Boards.Infrastructure.Queues;

using Microsoft.Extensions.Logging;

using RabbitMQ.Client.Events;

namespace BoardItemGetAllQueryService {
	internal class Consumer : AbstractConsumer<BoardItemGetAllQuery> {
		private readonly IProducer _producer;

		public Consumer(ILogger<Consumer> logger, IJsonService jsonSvc, IProducer producer) : base(logger, jsonSvc) {
			_producer = producer;
		}

		protected override Task OnConsumed(BasicDeliverEventArgs context, BoardItemGetAllQuery dto) {
			var items = new List<BoardItemDTO> {
				new(){ Id = Guid.NewGuid() },
				new(){ Id = Guid.NewGuid() },
			}; // IEnumerable<BoardItemDTO>
			var id = context.BasicProperties.CorrelationId;
			var queue = context.BasicProperties.ReplyTo;
			return _producer.Publish(items, id, queue);
		}
	}
}
