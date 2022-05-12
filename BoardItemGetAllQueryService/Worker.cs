using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Application.Services;

using Boards.Application.Queries.BoardItems;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BoardItemGetAllQueryService {
	internal class Worker : BackgroundService {
		private readonly ILogger<Worker> _logger;
		private readonly Consumer _consumer;

		public Worker(ILogger<Worker> logger, Consumer consumer) {
			_logger = logger;
			_consumer = consumer;
		}

		protected override async Task ExecuteAsync(CancellationToken token) {
			token.ThrowIfCancellationRequested();
			//while (!token.IsCancellationRequested) {
			//_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			await _consumer.Consume();
			//await Task.Delay(1000, token);
			//}
		}
	}
}
