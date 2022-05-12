using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Board.Application.Services;

using RabbitMQ.Client;

namespace Boards.Infrastructure.Queues {
	internal class Producer : IProducer {
		private readonly IJsonService _jsonSvc;

		public Producer(IJsonService jsonSvc) => _jsonSvc = jsonSvc;

		/// <inheritdoc/>/>
		public Task Publish<T>(T dto) where T : class {
			var factory = new ConnectionFactory() { HostName = "192.168.1.127", UserName = "rabbitmq", Password = "rabbitmq" }; // TODO: use IConnectionFactory
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			var queue = $"queue-{nameof(T)}";
			channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);

			var body = this.GetBody(dto);
			channel.BasicPublish(exchange: "", routingKey: queue, body: body);

			return Task.CompletedTask;
		}

		public Task Publish<T>(T dto, string id, string queue) where T : class {
			var factory = new ConnectionFactory() { HostName = "192.168.1.127", UserName = "rabbitmq", Password = "rabbitmq" }; // TODO: use IConnectionFactory
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			var props = channel.CreateBasicProperties();
			props.CorrelationId = id;
			var body = this.GetBody(dto);
			channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: props, body: body);

			return Task.CompletedTask;

		}

		private byte[] GetBody(object dto) {
			var text = _jsonSvc.Serialize(dto);
			return Encoding.UTF8.GetBytes(text); // TODO: use EncodingSvc
		}


	}
}
