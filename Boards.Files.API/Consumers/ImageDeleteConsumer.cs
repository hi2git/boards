using Boards.Commons.Application.Consumers;
using Boards.Domain.Contracts.Images;
using Boards.Files.Application.Commands;

using MediatR;

namespace Boards.Files.API.Consumers {
	public class ImageDeleteConsumer : AbstractConsumer<ImageDeleteMsg, ImageDeleteResponse> {
		public ImageDeleteConsumer(IMediator mediator) : base(mediator) { }

		protected override Task Handle(ImageDeleteMsg item, CancellationToken token) => this.Mediator.Send(new ImageDeleteCommand(item), token);
	}
}
