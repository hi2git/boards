using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Board.Application.Services;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Boards.Infrastructure.Queues {
	internal class AbstractRpcClient<TResponse> : IRpcClient<TResponse> {

		private readonly BlockingCollection<TResponse> _respQueue = new();
		private readonly IJsonService _jsonSvc;

		public AbstractRpcClient(IJsonService jsonSvc) => _jsonSvc = jsonSvc;

		/// <inheritdoc/>/>
		public TResponse Call<T>(T dto) {
			var factory = new ConnectionFactory() { HostName = "192.168.1.127", UserName = "rabbitmq", Password = "rabbitmq" }; // TODO: use IConnectionFactory
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			var queue = $"queue-{typeof(T).FullName}";
			channel.QueueDeclare(queue: queue, exclusive: false, autoDelete: false);

			// --

			var replyQueueName = channel.QueueDeclare().QueueName;
			var props = channel.CreateBasicProperties();
			var correlationId = Guid.NewGuid().ToString();
			props.CorrelationId = correlationId;
			props.ReplyTo = replyQueueName;

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (_, ea) => {
				if (ea.BasicProperties.CorrelationId == correlationId)
					this.Consume(ea);
			};

			channel.BasicConsume(
				consumer: consumer,
				queue: replyQueueName,
				autoAck: true
			);

			var body = this.GetBody(dto);
			channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: props, body: body);

			return _respQueue.Take();
		}


		private void Consume(BasicDeliverEventArgs ea) {
			var text = Encoding.UTF8.GetString(ea.Body.ToArray()); // TODO: use EncodingSvc
			var dto = _jsonSvc.Deserialize<TResponse>(text);
			_respQueue.Add(dto);
		}

		private byte[] GetBody(object dto) {
			var text = _jsonSvc.Serialize(dto);
			return Encoding.UTF8.GetBytes(text); // TODO: use EncodingSvc
		}
	}
}
