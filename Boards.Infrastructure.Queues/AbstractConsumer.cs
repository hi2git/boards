using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Board.Application.Services;

using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Boards.Infrastructure.Queues {
	public abstract class AbstractConsumer<T> : IConsumer<T> where T : class {
		private readonly ILogger<AbstractConsumer<T>> _logger;
		private readonly IJsonService _jsonSvc;
		private readonly IModel _channel;

		public AbstractConsumer(ILogger<AbstractConsumer<T>> logger, IJsonService jsonSvc) {
			_logger = logger;
			_jsonSvc = jsonSvc;

			var factory = new ConnectionFactory { HostName = "192.168.1.127", UserName = "rabbitmq", Password = "rabbitmq", DispatchConsumersAsync = true };
			var connection = factory.CreateConnection();
			_channel = connection.CreateModel();
		}

		public Task Consume() {
			var queue = $"queue-{typeof(T)}";
			_channel.QueueDeclare(queue: queue, exclusive: false, autoDelete: false);

			var consumer = new AsyncEventingBasicConsumer(_channel);
			consumer.Received += this.Consume1;

			_channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
			return Task.CompletedTask;
		}

		protected abstract Task OnConsumed(BasicDeliverEventArgs context, T dto);

		private Task Consume1(object sender, BasicDeliverEventArgs ea) {
			var text = Encoding.UTF8.GetString(ea.Body.ToArray()); // TODO: use EncodingSvc
			_logger.LogInformation($"Received: {text}");
			var dto = _jsonSvc.Deserialize<T>(text);
			return this.OnConsumed(ea, dto);
		}

	}
}
