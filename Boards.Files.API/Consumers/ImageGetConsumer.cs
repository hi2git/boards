using Boards.Domain.Contracts.Images;
using Boards.Files.Application.Commands;

using MassTransit;

using MediatR;

namespace Boards.Files.API.Consumers {
	public class ImageGetConsumer : IConsumer<ImageGetMsg> {
		private readonly IMediator _mediator;

		public ImageGetConsumer(IMediator mediator) => _mediator = mediator;

		public async Task Consume(ConsumeContext<ImageGetMsg> context) {
			var content = await _mediator.Send(new ImageGetQuery(context.Message), context.CancellationToken);
			await context.RespondAsync(new ImageGetResponse { Content = content });
		}

	}
}
